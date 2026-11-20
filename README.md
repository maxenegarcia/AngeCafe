# ANGE CAFE
## Restaurant Ordering and Menu Management System
**UST Angelicum College**

---

## Project Overview

Ange Cafe is a web-based Restaurant Ordering and Menu Management System developed for UST Angelicum College. The system aims to simplify restaurant operations by providing an organized platform for customer ordering, menu management, order processing, and payment monitoring.

Customers can browse available menu items, select variants, place orders, and track their order status. Administrators can manage menu categories and menu items, while staff members can process customer orders and update order and payment statuses.

## Project Objectives

- Provide customers with an accessible online menu and ordering system.
- Simplify restaurant order processing and management.
- Allow administrators to manage menu categories, items, variants, and prices.
- Provide staff with tools to monitor and update customer orders.
- Track payment statuses and daily sales.
- Store restaurant transactions and information in a centralized database.

## System Features

### Customer Features
- Browse available menu items and categories.
- View menu item variants and prices.
- Add items to the shopping cart.
- Update item quantities and manage cart contents.
- Place customer orders.
- Enter customer name and contact number.
- Select Cash or GCash as the payment method.
- Receive an automatically generated queue number.
- Track order status.
- View order history using a contact number.

### Admin Features
- Secure administrator login.
- Manage menu categories.
- Add new menu items.
- Edit existing menu items.
- Activate and deactivate menu items.
- Manage item variants, prices, and availability.
- Access the admin dashboard.
- View total orders and completed orders.
- Monitor daily sales.
- View Cash and GCash sales.
- Access staff order management.

### Staff Features
- Secure staff login.
- View customer orders.
- Search orders by customer name or queue number.
- Filter orders by order status.
- View customer and order information.
- Update order statuses.
- Update payment statuses.
- Monitor pending and completed orders.
- Log out securely.

## Technologies Used

### Frontend
- HTML
- CSS
- JavaScript

### Backend
- C#
- ASP.NET Core Web API
- Entity Framework Core
- JSON Web Token (JWT) Authentication

### Database
- Microsoft SQL Server
- SQL Server LocalDB

### Development Tools
- Visual Studio
- SQL Server Management Studio
- Git
- GitHub
- Swagger / OpenAPI

## System User Roles

| Role | Description |
|---|---|
| Admin | Manages menu information, categories, sales reports, and system operations. |
| Waiter | Views customer orders and manages order and payment statuses. |
| Customer | Browses the menu, places orders, and tracks order information without requiring an account. |

## Team Members and Branch Assignments

The project is developed by three Bachelor of Science in Information Technology students. Each member is assigned a specific system component and works on a separate Git branch.

| Team Member | Assigned Branch | Responsibilities |
|---|---|---|
| Maxene Garcia | Menu and Database Management | Project foundation, database configuration, menu categories, menu items, and menu management. |
| Althea Onez | Staff Order Management | Staff order viewing, order processing, order status updates, and payment status management. |
| Nichole Pebris | Customer Ordering and Checkout | Customer ordering, shopping cart, checkout process, payment method selection, and order submission. |

### Branching Strategy

The project follows a branch-based development workflow. Each team member works independently on their assigned branch to develop and test their respective features.

The team follows these steps:

1. Each member works on their assigned Git branch.
2. Changes are committed locally.
3. Completed changes are pushed to the GitHub repository.
4. A Pull Request is created for review.
5. Team members review and test the submitted changes.
6. Approved changes are merged into the main branch.

This workflow helps organize development tasks, minimize conflicts, and maintain collaboration among team members.

## Test Accounts

The following development accounts are provided for local testing:

| Role | Username | Password |
|---|---|---|
| Admin | admin | 123 |
| Waiter | staff | 123 |
| Customer | No login required | No password required |

**Note:** Customers do not need to log in. They provide their name and contact number during checkout.

These credentials are intended for academic testing only and should not be used in a production environment.

## Database Information

**Database Name:** AngeCafeDB

**Database Server:** SQL Server LocalDB

### Connection String

```text
Server=(localdb)\MSSQLLocalDB;Database=AngeCafeDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

### Main Database Tables

| Table Name | Description |
|---|---|
| Users | Stores administrator and staff account information. |
| Categories | Stores menu categories. |
| MenuItems | Stores restaurant menu item information. |
| MenuItemVariants | Stores menu item variants, prices, and availability. |
| Orders | Stores customer order information. |
| OrderItems | Stores the individual items included in each order. |

## Installation and Setup

### Prerequisites

Before running the project, make sure the following are installed:

- Visual Studio
- .NET 8 SDK
- SQL Server LocalDB
- Git
- Entity Framework Core tools

### Step 1: Clone the Repository

Open a terminal and execute:

```bash
git clone https://github.com/maxenegarcia/AngeCafe.git
```

### Step 2: Open the Project

Open the cloned repository in Visual Studio.

Locate the ASP.NET Core API project and open the solution or project file.

### Step 3: Configure the Database

Make sure SQL Server LocalDB is installed and running.

Configure the database connection string in the application's configuration file.

```text
Server=(localdb)\MSSQLLocalDB;Database=AngeCafeDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

### Step 4: Install Dependencies

Open a terminal in the directory containing the project file and execute:

```bash
dotnet restore
```

### Step 5: Apply Database Migrations

Execute the following command to create or update the database:

```bash
dotnet ef database update
```

### Step 6: Run the Application

Run the project through Visual Studio or execute:

```bash
dotnet run
```

### Step 7: Open the Application

Use the HTTPS address displayed in Visual Studio or the terminal.

The local development address may be:

```text
https://localhost:7293
```

### Available Pages

| Page | URL |
|---|---|
| Customer Menu | https://localhost:7293/menu.html |
| Admin Login | https://localhost:7293/login.html |
| Menu Management | https://localhost:7293/admin-menu.html |
| Admin Dashboard | https://localhost:7293/admin-dashboard.html |
| Staff Order Management | https://localhost:7293/staff-orders.html |
| Swagger API Documentation | https://localhost:7293/swagger |

**Note:** The HTTPS port may vary depending on the project's launch settings.

## API Documentation

The system uses ASP.NET Core Web API to handle communication between the frontend and backend.

Swagger is integrated to provide interactive API documentation and allow developers to test available endpoints.

**Swagger URL:**

```text
https://localhost:7293/swagger
```

### Main API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | /api/Category | Retrieve menu categories. |
| POST | /api/Category | Create a category. |
| PUT | /api/Category/{id} | Update a category. |
| DELETE | /api/Category/{id} | Delete a category. |
| GET | /api/MenuItem | Retrieve menu items. |
| POST | /api/MenuItem | Create a menu item. |
| PUT | /api/MenuItem/{id} | Update a menu item. |
| DELETE | /api/MenuItem/{id} | Delete a menu item. |
| POST | /api/Auth/login | Authenticate a user. |
| POST | /api/Order | Submit a customer order. |
| GET | /api/Order/{queueNumber} | Track an order. |
| GET | /api/Order/history | Retrieve customer order history. |
| GET | /api/Order/staff | Retrieve all customer orders for staff. |
| PUT | /api/Order/staff/{id}/status | Update order status. |
| PUT | /api/Order/staff/{orderId}/payment | Update payment status. |
| GET | /api/Order/staff/daily-sales | Retrieve the daily sales report. |

Some endpoints require JWT authentication and specific user roles.

## Order Statuses

Customer orders follow these statuses:

| Status | Description |
|---|---|
| Pending | The order has been received. |
| Preparing | The order is being prepared. |
| Ready for Pickup | The order is ready for collection. |
| Completed | The customer order has been completed. |
| Cancelled | The order has been cancelled. |

## Payment Methods and Statuses

The system supports the following payment methods:

- Cash
- GCash

Payment statuses:

- Unpaid
- Paid

## Testing and Verification

The following functionalities should be tested:

- Admin and staff authentication.
- Role-based access and authorization.
- Customer menu browsing.
- Menu category and item management.
- Menu variant selection.
- Customer order submission.
- Automatic queue number generation.
- Order retrieval and tracking.
- Order status updates.
- Payment status updates.
- Database record storage and retrieval.
- Daily sales calculations.
- Swagger API functionality.

## Security

The system uses JWT authentication to protect authorized API endpoints.

Role-based authorization restricts administrative and staff operations according to their assigned roles.

Passwords are stored as password hashes rather than plain text.

Development credentials and JWT signing keys must be configured locally and should not be exposed as production credentials.

## Project Information

**Project Title:** Restaurant Ordering and Menu Management System for Ange Cafe – UST Angelicum

**Institution:** UST Angelicum College

**Program:** Bachelor of Science in Information Technology

**Project Type:** Academic Group Project

**Repository:** https://github.com/maxenegarcia/AngeCafe

## License

This project was developed for academic purposes as part of a university project at UST Angelicum College.
