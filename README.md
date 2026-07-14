# CRUD Contact Manager

A modern, full-featured contact management application built with **ASP.NET Core 10.0** and **Entity Framework Core**. Manage your contacts with ease using this clean, intuitive web application that supports advanced filtering, sorting, and multiple export formats.

![.NET Version](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square)
![License](https://img.shields.io/badge/license-MIT-green?style=flat-square)

---

## 📋 Features

✨ **Core Functionality**
- **Create** new contacts with detailed information
- **Read** and view all contacts in a responsive table
- **Update** existing contact details
- **Delete** contacts with confirmation
- **Search** contacts by multiple fields (Name, Email, Country, etc.)
- **Sort** contacts by any column (ascending/descending)

📊 **Export Options**
- Export contacts to **PDF** format
- Export contacts to **CSV** format
- Export contacts to **Excel** format

🛡️ **Advanced Features**
- **Action Filters** for clean separation of concerns
- **Dependency Injection** for scalable architecture
- **Comprehensive Logging** using Serilog
- **Entity Framework Core** with SQL Server integration
- **Async/Await** for responsive operations
- **Custom Response Headers** for API integration

🎨 **User Interface**
- Modern, clean design with responsive layout
- Real-time search and filtering
- Sortable column headers with visual indicators
- Contact count badge
- Smooth animations and transitions on buttons
- Soft, eye-friendly color scheme
- Mobile-friendly interface

---

## 🏗️ Project Architecture

The application follows a **layered architecture pattern**:

```
CRUDContactManager/
├── Controllers/                 # Request handlers
├── Views/                       # Razor views for UI
├── ViewModels/                  # View-specific models
├── wwwroot/                     # Static files (CSS, JS, images)
├── Filters/                     # Action filters
├── Services/                    # Business logic layer
├── ServiceContracts/            # Service interfaces
├── Entities/                    # Domain models
├── Tests/                       # Unit tests
└── Views/Shared/                # Shared layout files
```

---

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 10.0
- **Language**: C# 13
- **Database**: SQL Server
- **ORM**: Entity Framework Core 10.0
- **Logging**: Serilog
- **PDF Export**: Rotativa.AspNetCore
- **Version Control**: Git

---

## 📦 Dependencies

Key NuGet packages used:

| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.EntityFrameworkCore.SqlServer | 10.0.9 | Database access |
| Serilog.AspNetCore | 10.0.0 | Structured logging |
| Serilog.Sinks.MSSqlServer | 10.0.0 | Log persistence |
| Rotativa.AspNetCore | 1.4.0 | PDF export |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or higher
- [SQL Server 2019](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or higher
- [Visual Studio 2026](https://visualstudio.microsoft.com/) or Visual Studio Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Downdate/CRUDContactManager.git
   cd CRUDContactManager
   ```

2. **Update the connection string** in `appsettings.json`
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=ContactManagerDB;Trusted_Connection=true;"
     }
   }
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update --project CRUDContactManager
   ```

5. **Build the project**
   ```bash
   dotnet build
   ```

6. **Run the application**
   ```bash
   dotnet run --project CRUDContactManager
   ```

7. **Open in browser**
   Navigate to `https://localhost:5001` (or the port specified in your console)

---

## 📝 Usage

### Adding a Contact

1. Click the **"✚ Create"** button on the Persons page
2. Fill in contact details (Name, Email, Date of Birth, Country, Gender)
3. Click **"Save"** to add the contact

### Searching Contacts

1. Use the **Search dropdown** to select the field to search by
2. Enter your search term in the **Search field**
3. Click **Search** to filter results
4. Click **"Clear all"** to reset filters

### Sorting Contacts

Click any column header to sort by that column:
- **First click**: Sort ascending (↑)
- **Second click**: Sort descending (↓)
- **Third click**: Remove sort

### Exporting Data

- **PDF**: Click "📄 PDF" to download a formatted PDF report
- **CSV**: Click "📊 CSV" to download a comma-separated file
- **Excel**: Click "📈 Excel" to download an Excel spreadsheet

### Updating a Contact

1. Click the **"✏️ Update"** button next to a contact
2. Modify the contact information
3. Click **"Update"** to save changes

### Deleting a Contact

1. Click the **"🗑️ Delete"** button next to a contact
2. Confirm the deletion when prompted
3. The contact will be permanently removed

---

## 🏗️ Code Structure

### Controllers

**PersonsController** - Handles all person-related operations
- `Index()` - Display all persons with search, filter, and sort
- `Create()` - Display create form and handle creation
- `Update()` - Display update form and handle updates
- `Delete()` - Handle deletion
- `PersonsPDF()` - Generate PDF export
- `PersonsCSV()` - Generate CSV export
- `PersonsExcel()` - Generate Excel export

### Services

**IPersonsService** - Business logic for person operations
- GetFilteredPersons()
- GetSortedPersons()
- GetPersonByID()
- AddPerson()
- UpdatePerson()
- DeletePerson()

**ICountriesService** - Manages country data
- GetCountriesList()
- UploadCountriesFromExcel()

### Filters

**PersonsListActionFilter** - Pre-processes person list data
**ResponseHeaderActionFilter** - Adds custom response headers

---

## 🧪 Testing

Run the test suite:

```bash
dotnet test
```

Tests are located in the `Tests/` directory and cover:
- Service layer functionality
- Data validation
- CRUD operations
- Export functionality

---

## 🎨 Customization

### Styling

The application uses custom CSS located in `wwwroot/css/`:
- `Style.css` - Main stylesheet with modern button styling
- `normalize.css` - CSS normalization

To customize colors, fonts, or layouts, edit `Style.css`.

### Database Schema

Modify entity models in the `Entities/` project and create new migrations:

```bash
dotnet ef migrations add MigrationName --project Entities
dotnet ef database update
```

---

## 📋 Configuration

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "MSSqlServer",
        "Args": {
          "connectionString": "Server=YOUR_SERVER;Database=ContactManagerDB;..."
        }
      }
    ]
  }
}
```

### Logging

Logs are stored in:
- **Console** - Real-time application output
- **SQL Server** - Persistent log storage (Logs table)

Access logs through the logging interface or database queries.

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📝 License

This project is licensed under the **MIT License** - see the LICENSE file for details.

---

## 🐛 Troubleshooting

### Database Connection Issues

**Problem**: "Cannot connect to SQL Server"
- **Solution**: Verify your connection string in `appsettings.json` matches your SQL Server instance

### Migrations Not Applied

**Problem**: "Table 'Persons' does not exist"
- **Solution**: Run `dotnet ef database update` to apply pending migrations

### PDF Export Not Working

**Problem**: PDF download fails or returns error
- **Solution**: Ensure Rotativa.AspNetCore is installed and wkhtmltopdf is available on your system

### Port Already in Use

**Problem**: "Cannot start server, port xxx is already in use"
- **Solution**: Change the port in `Properties/launchSettings.json` or stop the conflicting application

---

## 📚 Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Docs](https://docs.microsoft.com/en-us/ef/core/)
- [Serilog Documentation](https://serilog.net/)
- [Rotativa Documentation](https://github.com/webgio/Rotativa.AspNetCore)

---

## 👨‍💻 Author

**Project**: CRUD Contact Manager  
**Repository**: [GitHub - Downdate/CRUDContactManager](https://github.com/Downdate/CRUDContactManager)

---

## 📞 Support

For issues, questions, or feature requests, please open an [Issue](https://github.com/Downdate/CRUDContactManager/issues) on GitHub.

---


**Status**: Active Development ✅

---

---

# مدیریت تماس CRUD

<div dir="rtl">

یک برنامه مدیریت تماس مدرن و کامل ساخته شده با **ASP.NET Core 10.0** و **Entity Framework Core**. مدیریت تماس‌های خود را با سهولت انجام دهید و از رابط کاربری تمیز و شهودی استفاده کنید که از فیلتر پیشرفته، مرتب‌سازی و فرمت‌های صادرات متعدد پشتیبانی می‌کند.

![.NET نسخه](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square)
![مجوز](https://img.shields.io/badge/license-MIT-green?style=flat-square)

---

## 📋 ویژگی‌ها

✨ **عملکرد اصلی**
- **ایجاد** تماس‌های جدید با اطلاعات دقیق
- **خواندن** و مشاهده تمام تماس‌ها در جدول پاسخ‌گو
- **بروزرسانی** اطلاعات تماس موجود
- **حذف** تماس‌ها با تأیید
- **جستجو** تماس‌ها بر اساس چندین فیلد (نام، ایمیل، کشور و غیره)
- **مرتب‌سازی** تماس‌ها بر اساس هر ستون (صعودی/نزولی)

📊 **گزینه‌های صادرات**
- صادرات تماس‌ها به فرمت **PDF**
- صادرات تماس‌ها به فرمت **CSV**
- صادرات تماس‌ها به فرمت **Excel**

🛡️ **ویژگی‌های پیشرفته**
- **Action Filters** برای جداسازی مناسب مسائل
- **Dependency Injection** برای معماری مقیاس‌پذیر
- **ثبت کامل** با استفاده از Serilog
- **Entity Framework Core** با ادغام SQL Server
- **Async/Await** برای عملیات پاسخ‌گو
- **Header‌های پاسخ سفارشی** برای ادغام API

🎨 **رابط کاربری**
- طراحی مدرن و تمیز با چیدمان پاسخ‌گو
- جستجو و فیلترکردن در زمان واقعی
- سرستون‌های قابل مرتب‌سازی با شاخص‌های بصری
- مدال شمارش تماس
- انتقال‌ها و حرکات صاف بر روی دکمه‌ها
- طرح رنگی نرم و آسان برای چشم‌ها
- رابط متناسب با موبایل

---

## 🏗️ معماری پروژه

برنامه از الگوی **معماری چند لایه** پیروی می‌کند:

```
CRUDContactManager/
├── Controllers/                 # دستگیره‌های درخواست
├── Views/                       # نمایش‌های Razor
├── ViewModels/                  # مدل‌های مختص نمایش
├── wwwroot/                     # فایل‌های ثابت (CSS، JS، تصاویر)
├── Filters/                     # فیلترهای عملی
├── Services/                    # لایه منطق تجاری
├── ServiceContracts/            # رابط‌های سرویس
├── Entities/                    # مدل‌های دامنه
├── Tests/                       # تست‌های واحد
└── Views/Shared/                # فایل‌های چیدمان مشترک
```

---

## 🛠️ پشته‌ی تکنولوژی

- **فریم‌ورک**: ASP.NET Core 10.0
- **زبان**: C# 13
- **پایگاه‌ داده**: SQL Server
- **ORM**: Entity Framework Core 10.0
- **ثبت**: Serilog
- **صادرات PDF**: Rotativa.AspNetCore
- **کنترل نسخه**: Git

---

## 📦 وابستگی‌ها

بسته‌های NuGet کلیدی استفاده شده:

| بسته | نسخه | منظور |
|---------|---------|---------|
| Microsoft.EntityFrameworkCore.SqlServer | 10.0.9 | دسترسی به پایگاه داده |
| Serilog.AspNetCore | 10.0.0 | ثبت ساختاریافته |
| Serilog.Sinks.MSSqlServer | 10.0.0 | پایداری ثبت |
| Rotativa.AspNetCore | 1.4.0 | صادرات PDF |

---

## 🚀 شروع کار

### پیش‌نیازها

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) یا بالاتر
- [SQL Server 2019](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) یا بالاتر
- [Visual Studio 2026](https://visualstudio.microsoft.com/) یا Visual Studio Code

### نصب

1. **مخزن را شبیه‌سازی کنید**
   ```bash
   git clone https://github.com/Downdate/CRUDContactManager.git
   cd CRUDContactManager
   ```

2. **رشته اتصال را در `appsettings.json` به‌روزرسانی کنید**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=ContactManagerDB;Trusted_Connection=true;"
     }
   }
   ```

3. **وابستگی‌ها را بازگردانید**
   ```bash
   dotnet restore
   ```

4. **انتقال پایگاه داده را اعمال کنید**
   ```bash
   dotnet ef database update --project CRUDContactManager
   ```

5. **پروژه را بسازید**
   ```bash
   dotnet build
   ```

6. **برنامه را اجرا کنید**
   ```bash
   dotnet run --project CRUDContactManager
   ```

7. **در مرورگر باز کنید**
   به `https://localhost:5001` بروید (یا پورتی که در کنسول مشخص شده است)

---

## 📝 استفاده

### افزودن یک تماس

1. روی دکمه **"✚ ایجاد"** در صفحه Persons کلیک کنید
2. اطلاعات تماس را پر کنید (نام، ایمیل، تاریخ تولد، کشور، جنسیت)
3. برای افزودن تماس روی **"ذخیره"** کلیک کنید

### جستجوی تماس‌ها

1. از منوی کشویی **جستجو** برای انتخاب فیلد جستجو استفاده کنید
2. اصطلاح جستجو خود را در **فیلد جستجو** وارد کنید
3. برای فیلتر کردن نتایج روی **جستجو** کلیک کنید
4. برای تنظیم مجدد فیلترها روی **"پاک کردن همه"** کلیک کنید

### مرتب‌سازی تماس‌ها

برای مرتب‌سازی بر اساس آن ستون روی هر سرستون کلیک کنید:
- **کلیک اول**: مرتب‌سازی صعودی (↑)
- **کلیک دوم**: مرتب‌سازی نزولی (↓)
- **کلیک سوم**: حذف مرتب‌سازی

### صادرات داده‌ها

- **PDF**: برای دانلود یک گزارش PDF قالب‌شده روی "📄 PDF" کلیک کنید
- **CSV**: برای دانلود یک فایل جدا شده با کاما روی "📊 CSV" کلیک کنید
- **Excel**: برای دانلود یک جدول اکسل روی "📈 Excel" کلیک کنید

### بروزرسانی یک تماس

1. کنار یک تماس روی دکمه **"✏️ بروزرسانی"** کلیک کنید
2. اطلاعات تماس را تغییر دهید
3. برای ذخیره تغییرات روی **"بروزرسانی"** کلیک کنید

### حذف یک تماس

1. کنار یک تماس روی دکمه **"🗑️ حذف"** کلیک کنید
2. حذف را تأیید کنید
3. تماس برای همیشه حذف می‌شود

---

## 🏗️ ساختار کد

### کنترل‌کننده‌ها

**PersonsController** - تمام عملیات مرتبط با شخص را مدیریت می‌کند
- `Index()` - نمایش تمام اشخاص با جستجو، فیلتر و مرتب‌سازی
- `Create()` - نمایش فرم ایجاد و مدیریت ایجاد
- `Update()` - نمایش فرم بروزرسانی و مدیریت بروزرسانی‌ها
- `Delete()` - مدیریت حذف
- `PersonsPDF()` - تولید صادرات PDF
- `PersonsCSV()` - تولید صادرات CSV
- `PersonsExcel()` - تولید صادرات Excel

### سرویس‌ها

**IPersonsService** - منطق تجاری برای عملیات شخص
- GetFilteredPersons()
- GetSortedPersons()
- GetPersonByID()
- AddPerson()
- UpdatePerson()
- DeletePerson()

**ICountriesService** - مدیریت داده‌های کشور
- GetCountriesList()
- UploadCountriesFromExcel()

### فیلترها

**PersonsListActionFilter** - پیش‌پردازش داده‌های فهرست شخص
**ResponseHeaderActionFilter** - افزودن سرستون‌های پاسخ سفارشی

---

## 🧪 تست‌کردن

مجموعه تست را اجرا کنید:

```bash
dotnet test
```

تست‌ها در دایرکتوری `Tests/` قرار دارند و شامل موارد زیر هستند:
- عملکرد لایه سرویس
- اعتبارسنجی داده‌ها
- عملیات CRUD
- عملکرد صادرات

---

## 🎨 سفارشی‌سازی

### استایل‌دهی

برنامه از CSS سفارشی واقع در `wwwroot/css/` استفاده می‌کند:
- `Style.css` - برگه سبک اصلی با استایل دکمه مدرن
- `normalize.css` - نرمال‌سازی CSS

برای سفارشی‌سازی رنگ‌ها، فونت‌ها یا چیدمان‌ها، `Style.css` را ویرایش کنید.

### طرح پایگاه داده

مدل‌های موجود را در پروژه `Entities/` تغییر دهید و انتقال‌های جدیدی ایجاد کنید:

```bash
dotnet ef migrations add MigrationName --project Entities
dotnet ef database update
```

---

## 📋 پیکربندی

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "MSSqlServer",
        "Args": {
          "connectionString": "Server=YOUR_SERVER;Database=ContactManagerDB;..."
        }
      }
    ]
  }
}
```

### ثبت

ثبت‌ها در مکان‌های زیر ذخیره می‌شوند:
- **کنسول** - خروجی برنامه در زمان واقعی
- **SQL Server** - ذخیره‌ظ ثبت دائمی (جدول ثبت‌ها)

ثبت‌ها را از طریق رابط ثبت یا پرس‌وجوی پایگاه داده دسترسی یابید.

---

## 🤝 مشارکت

مشارکت خوش‌آمد است! لطفا این مراحل را دنبال کنید:

1. مخزن را فورک کنید
2. شاخه ویژگی ایجاد کنید (`git checkout -b feature/amazing-feature`)
3. تغییرات خود را کامیت کنید (`git commit -m 'Add amazing feature'`)
4. به شاخه پوش کنید (`git push origin feature/amazing-feature`)
5. يک درخواست Pull باز کنید

---

## 📝 مجوز

این پروژه تحت مجوز **MIT** مجاز است - برای جزئیات فایل LICENSE را ببینید.

---

## 🐛 حل مشکلات

### مشکلات اتصال پایگاه داده

**مشکل**: "نمی‌توان به SQL Server متصل شود"
- **راه‌حل**: رشته اتصال خود را در `appsettings.json` تأیید کنید که با نمونه SQL Server شما مطابقت داشته باشد

### انتقال‌ها اعمال نشده‌اند

**مشکل**: "جدول 'Persons' وجود ندارد"
- **راه‌حل**: `dotnet ef database update` را اجرا کنید تا انتقال‌های معلق را اعمال کنید

### صادرات PDF کار نمی‌کند

**مشکل**: دانلود PDF ناموفق است یا خطا برمی‌گرداند
- **راه‌حل**: اطمینان یافته‌اید که Rotativa.AspNetCore نصب شده است و wkhtmltopdf در سیستم شما موجود است

### پورت در حال استفاده است

**مشکل**: "نمی‌توان سرور را شروع کنید، پورت xxx در حال استفاده است"
- **راه‌حل**: پورت را در `Properties/launchSettings.json` تغییر دهید یا برنامه متنازع را متوقف کنید

---

## 📚 منابع

- [مستندات ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
- [مستندات Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [مستندات Serilog](https://serilog.net/)
- [مستندات Rotativa](https://github.com/webgio/Rotativa.AspNetCore)

---

## 👨‍💻 نویسنده

**پروژه**: مدیریت تماس CRUD  
**مخزن**: [GitHub - Downdate/CRUDContactManager](https://github.com/Downdate/CRUDContactManager)

---

## 📞 پشتیبانی

برای مشکلات، سؤالات یا درخواست‌های ویژگی، لطفاً یک [Issue](https://github.com/Downdate/CRUDContactManager/issues) را در GitHub باز کنید.

---


**وضعیت**: پایان یافته ✅
ادامه این پروژه در ریپازتوری جدید و با رعایت اصول Clean Architecture یا Onion Architecture انجام میشود

</div>
