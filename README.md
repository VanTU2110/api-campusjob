# API Backend - Đồ án Tốt nghiệp

Đây là API backend cho đồ án tốt nghiệp của tôi, được xây dựng với .NET và Entity Framework, sử dụng MariaDB làm cơ sở dữ liệu.

## Mục tiêu

Xây dựng một API để quản lý các chức năng của ứng dụng tìm việc làm bán thời gian cho sinh viên, bao gồm:

- Quản lý người dùng (Đăng ký, Đăng nhập, Hồ sơ)
- Quản lý công ty (Tạo, Chỉnh sửa, Xóa công ty)
- Quản lý công việc (Tạo, Chỉnh sửa, Xóa công việc)
- Quản lý ứng tuyển (Ứng tuyển, Hủy ứng tuyển, Cập nhật trạng thái ứng tuyển)
- Quản lý vùng miền (Tỉnh, Huyện, Xã)

## Các công nghệ sử dụng

- **.NET 6/7**: Nền tảng phát triển API
- **Entity Framework Core**: ORM để truy vấn cơ sở dữ liệu
- **MariaDB**: Hệ quản trị cơ sở dữ liệu
- **AutoMapper**: Dùng để ánh xạ giữa các đối tượng DTO và Entity
- **JWT (JSON Web Tokens)**: Quản lý xác thực và phân quyền
- **Swagger**: Tài liệu API tự động
- **FluentValidation**: Kiểm tra tính hợp lệ của dữ liệu đầu vào

## Cài đặt

1. **Cài đặt .NET SDK và MariaDB**:
   - Cài đặt .NET SDK: [Hướng dẫn cài đặt .NET](https://dotnet.microsoft.com/download)
   - Cài đặt MariaDB: [Hướng dẫn cài đặt MariaDB](https://mariadb.org/download/)

2. **Clone repository về máy**:
   ```bash
   git clone https://github.com/VanTU2110/api-campusjob.git
   cd api-campusjob
3 **Cấu hình cơ sở dữ liệu**
  - Tạo cơ sở dữ liệu MariaDB và cấu hình trong appsettings.json.
  - Cấu hình chuỗi kết nối đến cơ sở dữ liệu trong file appsettings.json:
    "ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=job_portal_db;User=root;Password=yourpassword;"
}
4. **Cài đặt các gói NuGet Package**
   -dotnet restore
5. **Chạy Migration để tạo bảng trong cơ sở dữ liệu:**
    ```bash
    -dotnet ef database update
6. **Chạy API**
7. ```bash
    -dotnet run

