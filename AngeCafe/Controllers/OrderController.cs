
using AngeCafe.API.Data;
using AngeCafe.API.DTOs;
using AngeCafe.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngeCafe.Api.Controllers
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

        // ==========================================
        // CUSTOMER ORDERING
        // ==========================================

        // POST: api/Order
        // Create a new customer order
        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder(
            OrderRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerName) ||
                string.IsNullOrWhiteSpace(request.ContactNumber))
            {
                return BadRequest(
                    "Customer name and contact number are required.");
            }

            if (string.IsNullOrWhiteSpace(request.PaymentMethod))
            {
                return BadRequest("Please select a payment method.");
            }

            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest("Your cart is empty.");
            }

            string paymentMethod = request.PaymentMethod.Trim();

            if (!paymentMethod.Equals(
                    "Cash", StringComparison.OrdinalIgnoreCase) &&
                !paymentMethod.Equals(
                    "GCash", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(
                    "Payment method must be Cash or GCash.");
            }

            if (request.Items.Any(i =>
                    i.VariantId <= 0 || i.Quantity <= 0))
            {
                return BadRequest("Invalid item or quantity.");
            }

            var variantIds = request.Items
                .Select(i => i.VariantId)
                .ToList();

            if (variantIds.Distinct().Count() != variantIds.Count)
            {
                return BadRequest(
                    "Each variant should appear only once in the cart.");
            }

            var variants = await _context.MenuItemVariants
                .Include(v => v.MenuItem)
                .Where(v => variantIds.Contains(v.VariantId))
                .ToListAsync();

            if (variants.Count != variantIds.Count)
            {
                return BadRequest(
                    "One or more selected items do not exist.");
            }

            if (variants.Any(v =>
                    !v.IsAvailable ||
                    v.MenuItem == null ||
                    !v.MenuItem.IsActive))
            {
                return BadRequest(
                    "One or more selected items are unavailable.");
            }

            var variantDictionary = variants
                .ToDictionary(v => v.VariantId);

            var order = new Order
            {
                CustomerName = request.CustomerName.Trim(),
                ContactNumber = request.ContactNumber.Trim(),
                OrderDate = DateTime.Now,

                PaymentMethod = paymentMethod.Equals(
                    "Cash", StringComparison.OrdinalIgnoreCase)
                    ? "Cash"
                    : "GCash",

                PaymentStatus = "Unpaid",
                OrderStatus = "Pending"
            };

            foreach (var item in request.Items)
            {
                var variant = variantDictionary[item.VariantId];

                decimal subtotal = variant.Price * item.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    VariantId = variant.VariantId,
                    Quantity = item.Quantity,
                    UnitPrice = variant.Price,
                    Subtotal = subtotal
                });

                order.TotalAmount += subtotal;
            }

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            order.QueueNumber =
                $"AC-{order.OrderDate:yyyyMMdd}-{order.OrderId:D4}";

            await _context.SaveChangesAsync();

            var response = await GetOrderResponse(order.OrderId);

            return CreatedAtAction(
                nameof(GetOrderByQueueNumber),
                new { queueNumber = order.QueueNumber },
                response
            );
        }

        // ==========================================
        // CUSTOMER ORDER TRACKING
        // ==========================================

        // GET: api/Order/{queueNumber}
        // Get an order using its queue number
        [HttpGet("{queueNumber}")]
        public async Task<ActionResult<OrderResponseDto>>
            GetOrderByQueueNumber(string queueNumber)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItemVariant)
                        .ThenInclude(v => v!.MenuItem)
                .FirstOrDefaultAsync(
                    o => o.QueueNumber == queueNumber);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            return Ok(MapOrderResponse(order));
        }

        // GET: api/Order/history?contactNumber=09XXXXXXXXX
        // Get customer order history
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>>
            GetOrderHistory(
                [FromQuery] string contactNumber)
        {
            if (string.IsNullOrWhiteSpace(contactNumber))
            {
                return BadRequest(
                    "Contact number is required.");
            }

            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItemVariant)
                        .ThenInclude(v => v!.MenuItem)
                .Where(o =>
                    o.ContactNumber == contactNumber.Trim())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Ok(orders.Select(MapOrderResponse));
        }

        // ==========================================
        // STAFF ORDER MANAGEMENT
        // ==========================================

        // GET: api/Order/staff
        // View all customer orders
        [HttpGet("staff")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>>
            GetAllOrdersForStaff()
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItemVariant)
                        .ThenInclude(v => v!.MenuItem)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Ok(orders.Select(MapOrderResponse));
        }

        // GET: api/Order/staff/{orderId}
        // View a specific order
        [HttpGet("staff/{orderId:int}")]
        public async Task<ActionResult<OrderResponseDto>>
            GetStaffOrderById(int orderId)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItemVariant)
                        .ThenInclude(v => v!.MenuItem)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            return Ok(MapOrderResponse(order));
        }

        // PUT: api/Order/staff/{orderId}/status
        // Update order status
        [HttpPut("staff/{orderId:int}/status")]
        public async Task<IActionResult> UpdateOrderStatus(
            int orderId,
            [FromBody] string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
            {
                return BadRequest(
                    "Order status is required.");
            }

            string[] allowedStatuses =
            {
                "Pending",
                "Preparing",
                "Ready",
                "Completed",
                "Cancelled"
            };

            string normalizedStatus = allowedStatuses
                .FirstOrDefault(status =>
                    status.Equals(
                        newStatus.Trim(),
                        StringComparison.OrdinalIgnoreCase));

            if (normalizedStatus == null)
            {
                return BadRequest(
                    "Invalid order status. Allowed statuses: " +
                    string.Join(", ", allowedStatuses));
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            order.OrderStatus = normalizedStatus;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Order status updated successfully.",
                orderId = order.OrderId,
                queueNumber = order.QueueNumber,
                orderStatus = order.OrderStatus
            });
        }

        // PUT: api/Order/staff/{orderId}/payment
        // Update payment status
        [HttpPut("staff/{orderId:int}/payment")]
        public async Task<IActionResult> UpdatePaymentStatus(
            int orderId,
            [FromBody] string newPaymentStatus)
        {
            if (string.IsNullOrWhiteSpace(newPaymentStatus))
            {
                return BadRequest(
                    "Payment status is required.");
            }

            string[] allowedPaymentStatuses =
            {
                "Unpaid",
                "Paid"
            };

            string normalizedPaymentStatus =
                allowedPaymentStatuses.FirstOrDefault(status =>
                    status.Equals(
                        newPaymentStatus.Trim(),
                        StringComparison.OrdinalIgnoreCase));

            if (normalizedPaymentStatus == null)
            {
                return BadRequest(
                    "Invalid payment status. Allowed statuses: " +
                    string.Join(", ", allowedPaymentStatuses));
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            order.PaymentStatus = normalizedPaymentStatus;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Payment status updated successfully.",
                orderId = order.OrderId,
                queueNumber = order.QueueNumber,
                paymentStatus = order.PaymentStatus
            });
        }

        // ==========================================
        // HELPER METHODS
        // ==========================================

        // Get order response by order ID
        private async Task<OrderResponseDto>
            GetOrderResponse(int orderId)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItemVariant)
                        .ThenInclude(v => v!.MenuItem)
                .FirstAsync(o => o.OrderId == orderId);

            return MapOrderResponse(order);
        }

        // Convert Order model to OrderResponseDto
        private static OrderResponseDto MapOrderResponse(
            Order order)
        {
            return new OrderResponseDto
            {
                OrderId = order.OrderId,
                QueueNumber = order.QueueNumber,
                CustomerName = order.CustomerName,
                ContactNumber = order.ContactNumber,
                OrderDate = order.OrderDate,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                OrderStatus = order.OrderStatus,
                TotalAmount = order.TotalAmount,

                Items = order.OrderItems.Select(oi =>
                    new OrderItemResponseDto
                    {
                        ItemName =
                            oi.MenuItemVariant?.MenuItem?.ItemName
                            ?? "Unavailable item",

                        VariantName =
                            oi.MenuItemVariant?.VariantName
                            ?? "Unavailable variant",

                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        Subtotal = oi.Subtotal
                    }).ToList()
            };
        }
    }
}
