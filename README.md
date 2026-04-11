# ASP.NET MVC 5 - Master Details CRUD Operation

A complete **ASP.NET MVC 5** web application demonstrating **Master-Details CRUD** operations with proper relationship between entities.

### Project Overview
This project is built using **ASP.NET MVC 5** and **Entity Framework 6**. It includes full Create, Read, Update, and Delete (CRUD) functionality with Master-Detail relationship (e.g., Customer and their Transactions/Orders).

### ✨ Features

- Master-Details Relationship Implementation
- Full CRUD Operations (Customer & Product Management)
- User Authentication & Authorization using ASP.NET Identity
- Responsive UI with Bootstrap 5 and Font Awesome
- Form Validation using Data Annotations
- AJAX Support for better user experience
- Clean Separation of Concerns

### 🛠️ Technologies Used

- **ASP.NET MVC 5**
- **.NET Framework 4.8.1**
- **Entity Framework 6 ( Database First)**
- **Microsoft Identity (Login, Register, Role Management)**
- **Bootstrap 5**
- **jQuery & AJAX**
- **SQL Server LocalDB (.mdf file)**

### 🚀 How to Run the Project Locally

1. **Clone the Repository**
   ```bash
   git clone https://github.com/DotNetAtikurrahman/ASP.NET-MVC-Master-Details-CRUD-Operation.git
   
2. Open the solution file (1294236.sln or .sln file) in Visual Studio 2022.
3. Restore NuGet Packages (Right-click on Solution → Restore NuGet Packages).
4. Set MollahThaiGlassHouse project as Startup Project.
5. Press F5 or click the IIS Express button to run the application.
6. The application will open in your browser with Login/Register functionality.

📁 Project Structure
textASP.NET-MVC-Master-Details-CRUD-Operation/
├── MollahThaiGlassHouse/
│   ├── Controllers/          # AccountController, CustomersController, ProductsController
│   ├── Models/               # Entity Models, ViewModels, Identity Models
│   ├── Views/                # All Razor Views (CRUD + Shared Layout)
│   ├── App_Start/            # RouteConfig, BundleConfig, IdentityConfig
│   ├── Content/ & Scripts/   # CSS, JavaScript, Bootstrap files
│   └── App_Data/             # Local Database files (.mdf)
├── packages/                 # NuGet Packages
└── README.md

🎯 Key Modules
User Registration & Login System
Customer Management (Master)
Product Management
Transaction/Order Details (Details)

📌 Future Improvements
Migrate to ASP.NET Core MVC
Implement Repository Pattern + Unit of Work
Add Role-based Authorization (Admin/User)
API Development with Web API
Deploy to Azure / IIS Server

Developed by: Atikur Rahman
.NET Developer
