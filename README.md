# WareUp - Product Management App

WareUp adalah aplikasi web sederhana berbasis ASP.NET Core MVC untuk mengelola produk dan kategori. Dibangun menggunakan .NET 8 dan Entity Framework Core dengan SQLite sebagai database lokal.

---

## 🛠 Features

- 🔐 User Authentication (Login, Register, Logout)
- 🖼️ Profile Update + Upload Foto
- 📦 CRUD Produk
- 🏷️ CRUD Kategori
- 📱 Responsive UI (Bootstrap 5 + Bootstrap Icons)
- 🗃️ Menggunakan SQLite untuk penyimpanan data lokal

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Optional: Visual Studio 2022+ / VS Code

---

### 📥 Clone Repository

```bash
git clone https://github.com/elsasalsa/product-web-app.git
cd product-web-app

⚙️ Setup Instructions
1. Restore dependencies
    dotnet restore

2. Install EF Core CLI (jika belum)
    dotnet tool install --global dotnet-ef

3. Apply database migrations
    dotnet ef database update

4. Run the application
    dotnet run

Lalu buka di browser:
http://localhost:5000
atau https://localhost:5001 (untuk HTTPS)
