# ANGE CAFE
## Restaurant Ordering and Menu Management System
### UST Angelicum College

## 1. Project Overview

Ange Cafe is a web-based Restaurant Ordering and Menu Management System developed for UST Angelicum College. The system is designed to simplify restaurant operations by providing an organized platform for managing menu items, processing customer orders, monitoring order statuses, and recording payments.

The system provides separate interfaces and functionalities for customers, administrators, and staff members.

## 2. Project Objectives

The project aims to:

- Develop a centralized system for managing restaurant menu items and categories.
- Allow customers to browse the available menu and place orders online.
- Provide staff with an interface for managing and updating customer orders.
- Allow administrators to manage menu information and monitor restaurant operations.
- Record order payments through Cash and GCash.
- Allow customers to track their orders using their queue numbers.
- Allow customers to view their previous orders using their contact numbers.
- Improve the efficiency and organization of restaurant operations.

## 3. System Features

### Customer Features

- View available menu items.
- Browse menu items by category.
- View menu item descriptions, prices, and available variants.
- Select item variants and quantities.
- Place orders without creating an account.
- Provide customer name and contact number.
- Select Cash or GCash as the payment method.
- Receive order confirmation and queue number.
- Track order details and current order status using a queue number.
- View previous orders using their contact number.
- View order details, purchased items, quantities, and total amounts.

### Administrator Features

- Secure administrator login.
- Manage menu categories.
- Add, view, update, and delete categories.
- Manage menu items.
- Add, view, update, and delete menu items.
- Manage menu item variants and prices.
- Enable or disable menu item availability.
- Access the administrator dashboard.
- Monitor total sales, Cash sales, GCash sales, total orders, and completed orders.

### Staff Features

- Secure staff login.
- View customer orders.
- Monitor order details.
- Update order statuses.
- Update payment statuses.
- Monitor pending and completed orders.
- View daily sales information.

## 4. Technologies Used

### Backend
- C#
- ASP.NET Core Web API
- .NET 8
- Entity Framework Core
- Repository Pattern
- Service Pattern
- JWT Authentication
- ASP.NET Core Identity PasswordHasher

### Frontend
- HTML5
- CSS3
- JavaScript

### Database
- Microsoft SQL Server
- SQL Server LocalDB

### Development Tools
- Visual Studio
- SQL Server Management Studio
- Swagger / OpenAPI
- Git
- GitHub

## 5. User Roles

The system has three user roles:

| Role | Responsibilities |
|---|---|
| Administrator | Manages menu categories, menu items, variants, and dashboard information. |
| Waiter / Staff | Monitors customer orders and updates order and payment statuses. |
| Customer | Browses the menu, places orders, tracks orders, and views order history without logging in. |

Customer ordering and order tracking are available without customer account registration. Administrator and Waiter accounts use JWT authentication.

## 6. Team Members and Responsibilities

The project is developed by three second-year BS Information Technology students.

| Team Member | Assigned Responsibility |
|---|---|
| Maxene Garcia | Project Foundation, Database, and Menu Management |
| Althea Onez | Staff Order Management |
| Nichole Pebris | Customer Ordering and Checkout |

### Member 1: Project Foundation, Database, and Menu Management

Responsibilities:
- Set up the ASP.NET Core Web API project.
- Configure the database connection.
- Implement Entity Framework Core and AppDbContext.
- Create database models and migrations.
- Configure repository and service patterns.
- Develop category management.
- Develop menu item management.
- Implement customer menu display.
- Configure shared application services and dependencies.

### Member 2: Staff Order Management

Responsibilities:
- Develop the staff order management interface.
- Display customer orders.
- View order details.
- Update order statuses.
- Update payment statuses.
- Monitor daily sales and order information.

### Member 3: Customer Ordering and Checkout

Responsibilities:
- Develop the customer ordering interface.
- Display available menu items.
- Allow customers to select menu item variants and quantities.
- Collect customer information.
- Implement checkout and order submission.
- Support Cash and GCash payment methods.
- Display order confirmation information.
- Implement customer order tracking.
- Implement customer order history.

## 7. Project Structure

The project follows a layered organization to separate application responsibilities.

```text
AngeCafe
│
├── AngeCafe
│   ├── Controllers
│   │   ├── AuthController.cs
│   │   ├── CategoryController.cs
│   │   ├── MenuItemController.cs
│   │   └── OrderController.cs
│   │
│   ├── Data
│   │   ├── AppDbContext.cs
│   │   ├── UserSeeder.cs
│   │   └── MenuSeeder.cs
│   │
│   ├── DTOs
│   │
│   ├── Models
│   │
│   ├── Repositories
│   │   ├── Interfaces
│   │   └── Implementations
│   │
│   ├── Services
│   │   ├── Interfaces
│   │   └── Implementations
│   │
│   ├── Migrations
│   │
│   ├── wwwroot
│   │   ├── login.html
│   │   ├── menu.html
│   │   ├── track-order.html
│   │   ├── admin-menu.html
│   │   ├── admin-dashboard.html
│   │   └── staff-orders.html
│   │
│   ├── Program.cs
│   ├── appsettings.json
│   └── AngeCafe.Api.csproj
│
└── README.md
```

## 8. Database Configuration

The system uses Microsoft SQL Server LocalDB.

### Database Name

`AngeCafeDB`

### Connection String

The default local development connection string is:

```text
Server=(localdb)\MSSQLLocalDB;Database=AngeCafeDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

### Database Tables

The database includes the following main tables:

| Table | Description |
|---|---|
| Categories | Stores menu categories. |
| MenuItems | Stores menu item information. |
| MenuItemVariants | Stores menu item variants and their prices. |
| Users | Stores administrator and staff account information. |
| Orders | Stores customer order information. |
| OrderItems | Stores the menu items included in each order. |

Entity Framework Core Code First Migrations are used to create and update the database structure.

## 9. Installation and Setup

### Prerequisites

Before running the project, install the following:

- Visual Studio 2022 or later.
- .NET 8 SDK.
- SQL Server LocalDB.
- Git.

### Step 1: Clone the Repository

Open a terminal and execute:

```bash
git clone https://github.com/maxenegarcia/AngeCafe.git
```

### Step 2: Open the Project

Open the cloned repository in Visual Studio.

Navigate to the folder containing:

`AngeCafe.Api.csproj`

Open the project in Visual Studio or use the terminal.

### Step 3: Restore Dependencies

Run:

```bash
dotnet restore
```

### Step 4: Configure User Secrets

The project uses .NET User Secrets to store sensitive information outside the repository.

Open a terminal in the project folder containing `AngeCafe.Api.csproj`.

Initialize User Secrets:

```powershell
dotnet user-secrets init
```

If the project already has a UserSecretsId, this command can be skipped.

Configure the administrator account:

```powershell
dotnet user-secrets set "SeedUsers:AdminUsername" "admin"
dotnet user-secrets set "SeedUsers:AdminPassword" "YOUR_ADMIN_PASSWORD"
```

Configure the waiter account:

```powershell
dotnet user-secrets set "SeedUsers:WaiterUsername" "staff"
dotnet user-secrets set "SeedUsers:WaiterPassword" "YOUR_WAITER_PASSWORD"
```

Replace the password placeholders with your chosen local development passwords.

### Step 5: Configure the JWT Signing Key

Generate a secure random JWT signing key using PowerShell:

```powershell
$bytes = New-Object byte[] 64
$rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
$rng.GetBytes($bytes)
$rng.Dispose()
$key = [Convert]::ToBase64String($bytes)
dotnet user-secrets set "Jwt:Key" "$key"
```

The generated key is stored in User Secrets and is not included in the repository.

### Step 6: Verify User Secrets

Run:

```powershell
dotnet user-secrets list
```

Verify that the following settings are configured:

- SeedUsers:AdminUsername
- SeedUsers:AdminPassword
- SeedUsers:WaiterUsername
- SeedUsers:WaiterPassword
- Jwt:Key

Do not share or commit the actual secret values.

### Step 7: Apply Database Migrations

Run:

```powershell
dotnet ef database update
```

If the Entity Framework Core CLI is not installed, install it using:

```powershell
dotnet tool install --global dotnet-ef --version 8.0.0
```

Then execute the database update command again.

### Step 8: Run the Application

Execute:

```powershell
dotnet run
```

Wait until the application starts successfully.

## 10. Accessing the System

The default development URL is:

`https://localhost:7293`

The actual URL may differ depending on the project's launch settings.

| Page | URL |
|---|---|
| Swagger API Documentation | https://localhost:7293/swagger |
| Login Page | https://localhost:7293/login.html |
| Customer Menu | https://localhost:7293/menu.html |
| Customer Order Tracking and History | https://localhost:7293/track-order.html |
| Admin Menu Management | https://localhost:7293/admin-menu.html |
| Admin Dashboard | https://localhost:7293/admin-dashboard.html |
| Staff Order Management | https://localhost:7293/staff-orders.html |

Customers can access the order tracking page through the **TRACK MY ORDER** navigation link on the customer menu.

## 11. Test Accounts

The administrator and waiter accounts are created through the UserSeeder using credentials configured in User Secrets.

For a fresh database, the usernames and passwords are determined by the values configured during the User Secrets setup.

For the existing local development database, the original test accounts are:

| Role | Username | Existing Local Password |
|---|---|---|
| Administrator | admin | 123 |
| Waiter / Staff | staff | 123 |

The existing accounts are not automatically updated when User Secrets passwords are changed. The seeder only creates an account when the username does not already exist.

Customers do not need an account to place orders, track orders, or view order history.

**Note:** The listed existing local passwords are for academic development and testing only. Use stronger passwords for new accounts and never use these credentials in a production environment.

## 12. API Endpoints

The application provides RESTful API endpoints for authentication, category management, menu management, and order processing.

### Authentication

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Auth/login` | Authenticates an administrator or waiter. |

### Categories

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Category` | Retrieves all categories. |
| GET | `/api/Category/{id}` | Retrieves a category by ID. |
| POST | `/api/Category` | Creates a category. |
| PUT | `/api/Category/{id}` | Updates a category. |
| DELETE | `/api/Category/{id}` | Deletes a category. |

### Menu Items

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/MenuItem` | Retrieves menu items. |
| GET | `/api/MenuItem/{id}` | Retrieves a menu item by ID. |
| POST | `/api/MenuItem` | Creates a menu item. |
| PUT | `/api/MenuItem/{id}` | Updates a menu item. |
| DELETE | `/api/MenuItem/{id}` | Deletes a menu item. |

### Orders

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Order` | Submits a customer order. |
| GET | `/api/Order/{queueNumber}` | Retrieves an order using its queue number. |
| GET | `/api/Order/history?contactNumber={number}` | Retrieves orders using a customer contact number. |
| GET | `/api/Order/staff` | Retrieves orders for staff management. |
| GET | `/api/Order/staff/{orderId}` | Retrieves a specific order for staff. |
| PUT | `/api/Order/staff/{id}/status` | Updates an order status. |
| PUT | `/api/Order/staff/{orderId}/payment` | Updates a payment status. |
| GET | `/api/Order/staff/daily-sales` | Retrieves daily sales information. |

Some endpoints require authentication and role authorization. Refer to Swagger for request formats, response details, and authorization requirements.

## 13. Customer Order Tracking and History

The system provides a dedicated customer page for tracking submitted orders and viewing previous transactions.

### Customer Order Tracking

Customers can track an order using the queue number received after successfully placing an order.

The tracking page retrieves and displays:

- Queue number.
- Customer name.
- Contact number.
- Order date.
- Order status.
- Payment method.
- Payment status.
- Ordered items and variants.
- Item quantities and prices.
- Total order amount.

### Customer Order History

Customers can enter the contact number used when placing their orders to retrieve their previous transactions.

The order history displays the available order records associated with the entered contact number, including order details, statuses, and total amounts.

### How to Track an Order

1. Open the customer menu.
2. Click **TRACK MY ORDER**.
3. Enter the queue number received after checkout.
4. Click the tracking button.
5. View the retrieved order information and current status.

### How to View Order History

1. Open the customer order tracking page.
2. Find the Order History section.
3. Enter the contact number used when placing the order.
4. Click the order history button.
5. View the previous orders associated with that contact number.

The order tracking and history features use the customer-facing order API endpoints.

## 14. Order and Payment Management

### Order Statuses

The system uses the following order statuses:

| Status | Description |
|---|---|
| Pending | The order has been submitted and is awaiting preparation. |
| Preparing | The staff is preparing the order. |
| Ready for Pickup | The order is ready for the customer. |
| Completed | The order has been completed. |
| Cancelled | The order has been cancelled. |

### Payment Methods

The system supports:

- Cash
- GCash

### Payment Statuses

| Status | Description |
|---|---|
| Paid | The payment has been received. |
| Unpaid | The payment has not yet been received. |

## 15. Authentication and Security

The system uses JSON Web Token (JWT) authentication for administrator and staff access.

Security features include:

- Password hashing through ASP.NET Core Identity PasswordHasher.
- JWT-based authentication.
- Role-based authorization.
- Protected administrator and staff endpoints.
- Customer access to public ordering functionality.
- Sensitive configuration stored through .NET User Secrets during local development.

JWT signing keys and account passwords must not be committed to the public repository.

## 16. Testing

The following functionalities should be tested:

### Authentication
- Administrator login.
- Waiter login.
- Invalid username and password handling.
- Role-based access restrictions.

### Menu Management
- Create a category.
- Update a category.
- Delete a category.
- Create a menu item.
- Update a menu item.
- Delete a menu item.
- Manage menu item variants and prices.
- Verify menu item availability.

### Customer Ordering
- Display available menu items.
- Select item variants.
- Select quantities.
- Submit customer information.
- Place an order.
- Verify order confirmation and queue number.

### Customer Order Tracking and History
- Open the tracking page through the customer menu.
- Retrieve an order using a valid queue number.
- Verify that order details and current status are displayed.
- Enter a contact number used for a previous order.
- Verify that matching order history is displayed.
- Verify that an invalid queue number is handled appropriately.
- Verify that a contact number with no matching orders displays an appropriate message.

### Staff Order Management
- View customer orders.
- View order details.
- Update order statuses.
- Update payment statuses.
- Verify daily sales information.

### Database
- Verify that migrations are applied.
- Confirm that records are saved in the database.
- Verify relationships between categories, menu items, variants, orders, and order items.

## 17. Git and Branching Strategy

The team follows a branch-based development workflow to separate each member's assigned responsibilities.

Each member works on their assigned branch and pushes changes to the GitHub repository.

### Development Workflow

1. Clone the repository.
2. Switch to the assigned working branch.
3. Implement the assigned features.
4. Test the changes locally.
5. Commit the changes with descriptive commit messages.
6. Push the branch to GitHub.
7. Create a Pull Request.
8. Review and merge approved changes into the main branch.

### Branch Responsibilities

| Team Member | Feature Responsibility |
|---|---|
| Maxene Garcia | Project Foundation, Database, and Menu Management |
| Althea Onez | Staff Order Management |
| Nichole Pebris | Customer Ordering and Checkout |

This workflow helps prevent conflicts and keeps each member's contributions organized.

## 18. Project Information

**Project Title:** Restaurant Ordering and Menu Management System for Ange Cafe – UST Angelicum College

**Course:** Bachelor of Science in Information Technology

**Institution:** UST Angelicum College

**Project Type:** Academic Group Project

**Repository:** https://github.com/maxenegarcia/AngeCafe.git

## 19. License

This project was developed for academic purposes at UST Angelicum College. It is intended for educational use and demonstration.
