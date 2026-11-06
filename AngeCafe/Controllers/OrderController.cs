using AngeCafe.API.Data;
using AngeCafe.API.DTOs;
using AngeCafe.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngeCafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // Submit a customer order
        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder(
            OrderRequestDto request)
        {
            if (request.PaymentMethod != "Cash" &&
                request.PaymentMethod != "GCash")
            {
                return BadRequest("Invalid payment method.");
            }

            if (request.Items == null || !request.Items.Any())
                return BadRequest("Your cart is empty.");

            var orderItems = new List<OrderItem>();

            foreach (var requestedItem in request.Items)
            {
                if (requestedItem.Quantity <= 0)
                    return BadRequest("Invalid item quantity.");

                var variant = await _context.MenuItemVariants
                    .Include(v => v.MenuItem)
                    .FirstOrDefaultAsync(v =>
                        v.VariantId == requestedItem.VariantId);

                if (variant == null)
                    return BadRequest("A selected menu variant does not exist.");

                if (!variant.IsAvailable ||
                    variant.MenuItem == null ||
                    !variant.MenuItem.IsActive)
                {
                    return BadRequest(
                        $"The selected item {variant.VariantName} is unavailable.");
                }

                decimal subtotal = variant.Price * requestedItem.Quantity;

                orderItems.Add(new OrderItem
                {
                    VariantId = variant.VariantId,
                    ItemName = variant.MenuItem.ItemName,
                    VariantName = variant.VariantName,
                    Quantity = requestedItem.Quantity,
                    UnitPrice = variant.Price,
                    Subtotal = subtotal
                });
            }

            decimal totalAmount = orderItems.Sum(i => i.Subtotal);

            var order = new Order
            {
                CustomerName = request.CustomerName.Trim(),
                ContactNumber = request.ContactNumber.Trim(),
                PaymentMethod = request.PaymentMethod,
                TotalAmount = totalAmount,
                OrderDate = DateTime.Now,
                OrderItems = orderItems
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Generate unique order and queue numbers
            order.OrderNumber = $"AC-{order.OrderId:D6}";
            order.QueueNumber = $"Q-{order.OrderId:D6}";

            await _context.SaveChangesAsync();

            var response = MapToResponse(order);

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = order.OrderId },
                response);
        }

        // Retrieve an order by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponseDto>> GetOrder(int id)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound("Order not found.");

            return Ok(MapToResponse(order));
        }

        // Retrieve customer order history using contact number
        [HttpGet("history/{contactNumber}")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>>
            GetOrderHistory(string contactNumber)
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .Where(o => o.ContactNumber == contactNumber)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Ok(orders.Select(MapToResponse));
        }

        private static OrderResponseDto MapToResponse(Order order)
        {
            return new OrderResponseDto
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                QueueNumber = order.QueueNumber,
                CustomerName = order.CustomerName,
                ContactNumber = order.ContactNumber,
                PaymentMethod = order.PaymentMethod,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,

                Items = order.OrderItems.Select(i =>
                    new OrderItemResponseDto
                    {
                        ItemName = i.ItemName,
                        VariantName = i.VariantName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        Subtotal = i.Subtotal
                    }).ToList()
            };
        }
    }
}
