<div align="center">

# 🛒 ASP.NET MVC — Code-First & Role-Based Auth

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-MVC-blue?style=for-the-badge&logo=microsoft&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework-Code--First-green?style=for-the-badge&logo=nuget&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-Database-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/Identity-Role--Based_Auth-orange?style=for-the-badge&logo=microsoftazure&logoColor=white" />
  <img src="https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge" />
</p>

> A production-style **ASP.NET Core 10 MVC** application demonstrating Code-First EF Core, Identity-based role authorization, full CRUD with image uploads, AJAX partial views, View Components, and custom routing — all wired together cleanly.

</div>

---

## 📌 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Data Models & ERD](#-data-models--erd)
- [Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
  - [Run the App](#run-the-app)
- [Role-Based Authorization](#-role-based-authorization)
- [Routing](#-routing)
- [Contributing](#-contributing)
- [License](#-license)

---

## 🧭 Overview

This project is a **real-world reference implementation** of an ASP.NET Core MVC application built entirely with the Code-First approach. Every table in the database is derived from C# model classes — no SQL scripts, no database-first scaffolding.

It covers everything a mid-to-senior developer would expect in a production codebase:

- 🔐 Secure authentication and role-protected actions
- 🗄️ Relational data with EF Core navigation properties
- 📁 File upload, storage, and deletion from the web root
- ⚡ AJAX-driven forms with partial view responses
- 🧩 Reusable UI blocks via View Components
- 🛣️ Both conventional and attribute-based custom routing

---

## ✨ Features

| Feature | Description |
|---|---|
| 🔐 **Authentication** | Register & Login powered by ASP.NET Core Identity Razor Pages |
| 👑 **Role Management** | Create roles (`SuperAdmin`, `Admin`) and assign them to users |
| 👤 **Customer Management** | Full CRUD — create, list, edit, delete customers with photo upload |
| 📦 **Product Management** | Full CRUD with details view and sidebar View Component |
| 🧾 **Transaction Details** | Many-to-many relationship linking customers to their purchased products |
| 🖼️ **Image Upload** | Profile pictures uploaded to `wwwroot/Images`, deleted on customer removal |
| ⚡ **AJAX Partial Views** | `_success` and `_error` partial responses for seamless UX |
| 🧩 **View Components** | `ProductMenuViewComponent` renders a dynamic sidebar product menu |
| 🛣️ **Custom Routing** | Both conventional named routes and `[Route(...)]` attribute routes |
| 🔄 **EF Core Migrations** | Full migration history tracked, database schema managed in code |

---

## 🛠 Tech Stack

| Layer | Technology |
|---|---|
| Runtime | .NET 10 |
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core 10 (Code-First) |
| Database | Microsoft SQL Server |
| Auth | ASP.NET Core Identity |
| Frontend | Razor Views · Bootstrap · jQuery Unobtrusive AJAX |
| Tooling | Visual Studio 2022 · LibMan · EF Core CLI |

---

## 📁 Project Structure

```
📦 ASP.NET_CORE_CodeFirst/
│
├── 📂 Areas/Identity/Pages/Account/
│   ├── Login.cshtml / .cs              # Scaffolded login page
│   └── Register.cshtml / .cs           # Scaffolded registration page
│
├── 📂 Controllers/
│   ├── CustomersController.cs          # CRUD + image upload + role guards
│   ├── ProductsController.cs           # Full product CRUD
│   ├── RoleController.cs               # Create & assign roles
│   └── HomeController.cs
│
├── 📂 Data/
│   ├── ApplicationDbContext.cs         # EF Core DbContext (Products, Customers, TransactionDetails)
│   ├── ApplicationUser.cs              # Extended IdentityUser
│   └── Migrations/                     # Auto-generated EF Core migration files
│
├── 📂 Models/
│   ├── DataModel.cs                    # Product · Customer · TransactionDetail entities
│   └── ViewModel/ClientVM.cs           # Customer ViewModel with IFormFile for image upload
│
├── 📂 ViewComponents/
│   └── ProductMenuViewComponent.cs     # Sidebar product menu component
│
├── 📂 Views/
│   ├── Customers/                      # Index · Create · Edit · Delete + _addNewProduct partial
│   ├── Products/                       # Index · Create · Edit · Delete · Details
│   ├── Role/                           # Index · AssignRole
│   └── Shared/
│       ├── Components/ProductMenu/     # View Component template
│       ├── _Layout.cshtml
│       ├── _LoginPartial.cshtml
│       ├── _success.cshtml             # AJAX success feedback partial
│       └── _error.cshtml               # AJAX error feedback partial
│
├── 📂 wwwroot/
│   ├── css/site.css
│   └── Images/                         # Runtime-uploaded customer photos
│
├── appsettings.json
├── Program.cs                          # Middleware pipeline + DI + custom routing
└── ASP.NET_CORE_CodeFirst.csproj
```

---

## 🗃 Data Models & ERD

### Entities

**`Product`**
```csharp
public int    ProductId   { get; set; }   // PK
public string ProductName { get; set; }
```

**`Customer`**
```csharp
public int      CustomerId    { get; set; }   // PK
public string   CustomerName  { get; set; }   // Required
public string   Picture       { get; set; }   // Path to uploaded image
public string   Address       { get; set; }
public string   Phone         { get; set; }
public DateTime PurchaseDate  { get; set; }   // Stored as date
public double   TotalBill     { get; set; }
public bool     IsPaid        { get; set; }
```

**`TransactionDetail`** *(join table)*
```csharp
public int CustomerId { get; set; }   // FK → Customer
public int ProductId  { get; set; }   // FK → Product
```

### Relationship Diagram

```
┌─────────────┐        ┌───────────────────┐        ┌─────────────┐
│   Customer  │ 1 ───► │ TransactionDetail │ ◄─── * │   Product   │
│─────────────│        │───────────────────│        │─────────────│
│ CustomerId  │        │ TransactionDetailId        │ ProductId   │
│ CustomerName│        │ CustomerId (FK)   │        │ ProductName │
│ Picture     │        │ ProductId  (FK)   │        └─────────────┘
│ TotalBill   │        └───────────────────┘
│ IsPaid      │
└─────────────┘
```

---

## 🚀 Getting Started

### Prerequisites

Make sure the following are installed before you begin:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (LocalDB, Express, or Developer edition)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) v17.10+ or VS Code with C# Dev Kit
- EF Core CLI:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Installation

```bash
# Clone the repository
git clone https://github.com/<your-username>/<repo-name>.git
cd <repo-name>

# Restore all NuGet packages
dotnet restore
```

### Database Setup

**1.** Open `appsettings.json` and update your connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB_NAME;Trusted_Connection=True;TrustServerCertificate=True"
}
```

**2.** Apply all migrations to create the schema:

```bash
dotnet ef database update
```

> **Adding new migrations** after modifying a model:
> ```bash
> dotnet ef migrations add <YourMigrationName>
> dotnet ef database update
> ```

### Run the App

```bash
dotnet run --project ASP.NET_CORE_CodeFirst
```

Or press **`F5`** in Visual Studio. The app launches at the URL defined in `Properties/launchSettings.json`.

---

## 🔐 Role-Based Authorization

The app ships with two roles enforced via `[Authorize(Roles = "...")]`:

| Role | Create | Edit | Delete | Manage Roles |
|---|:---:|:---:|:---:|:---:|
| `SuperAdmin` | ✅ | ✅ | ✅ | ✅ |
| `Admin` | ✅ | ❌ | ❌ | ❌ |
| Authenticated User | ❌ | ❌ | ❌ | ❌ |
| Anonymous | 🔒 | 🔒 | 🔒 | 🔒 |

```csharp
// Only SuperAdmin and Admin can create customers
[Authorize(Roles = "SuperAdmin,Admin")]
public IActionResult Create() { ... }

// Only SuperAdmin can edit or delete
[Authorize(Roles = "SuperAdmin")]
public async Task<IActionResult> Edit(int? id) { ... }

[Authorize(Roles = "SuperAdmin")]
public async Task<IActionResult> Delete(int? id) { ... }
```

> **First-time setup:** Register an account → navigate to `/Role` → create your roles → assign `SuperAdmin` to your user.

---

## 🛣 Routing

### Default Conventional Route
```
/{controller=Home}/{action=Index}/{id?}
```

### Custom Named Route — Customer Create
A friendly URL aliased to the `Customers/Create` action:
```
/add/newcustomer/mydatabase
```
```csharp
app.MapControllerRoute(
    name: "amercustomroute",
    pattern: "add/newcustomer/mydatabase",
    defaults: new { controller = "Customers", action = "Create" }
);
```

### Attribute Route — SuperAdmin Edit Only
The edit endpoint is locked behind a descriptive custom URL:
```
/ohbrotheronlysuperadmin/canedit
```
```csharp
[Authorize(Roles = "SuperAdmin")]
[Route("ohbrotheronlysuperadmin/canedit")]
public async Task<IActionResult> Edit(int? id) { ... }
```

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch
   ```bash
   git checkout -b feature/amazing-feature
   ```
3. **Commit** your changes with a clear message
   ```bash
   git commit -m "feat: add amazing feature"
   ```
4. **Push** to your branch
   ```bash
   git push origin feature/amazing-feature
   ```
5. **Open** a Pull Request and describe what you changed

Please keep code style consistent with existing patterns and add XML doc comments on public members.

---

## 📄 License

Distributed under the **MIT License**. See [`LICENSE`](LICENSE) for full details.

---

<div align="center">

Made with ❤️ using **ASP.NET MVC 10**

</div>
