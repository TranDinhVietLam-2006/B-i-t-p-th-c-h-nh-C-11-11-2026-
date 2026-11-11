CREATE DATABASE SalesDB;
GO
USE SalesDB;
GO

CREATE TABLE NhanVien (
    MaNV INT PRIMARY KEY IDENTITY(1,1),
    TenNV NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) UNIQUE,
    ChucVu NVARCHAR(50)
);

CREATE TABLE KhachHang (
    MaKH INT PRIMARY KEY IDENTITY(1,1),
    TenKH NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(15) UNIQUE,
    DiemTichLuy INT DEFAULT 0
);

CREATE TABLE SanPham (
    MaSP VARCHAR(20) PRIMARY KEY,
    TenSP NVARCHAR(150) NOT NULL,
    GiaBan DECIMAL(18,2) CHECK (GiaBan > 0),
    SoLuongTon INT CHECK (SoLuongTon >= 0)
);

CREATE TABLE HoaDon (
    MaHD INT PRIMARY KEY IDENTITY(1,1),
    NgayLap DATETIME DEFAULT GETDATE(),
    MaNV INT NOT NULL FOREIGN KEY REFERENCES NhanVien(MaNV),
    MaKH INT NULL FOREIGN KEY REFERENCES KhachHang(MaKH),
    TongTien DECIMAL(18,2)
);

CREATE TABLE ChiTietHoaDon (
    MaHD INT NOT NULL FOREIGN KEY REFERENCES HoaDon(MaHD),
    MaSP VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES SanPham(MaSP),
    SoLuong INT CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2),
    PRIMARY KEY (MaHD, MaSP)
);
GO

INSERT INTO NhanVien (TenNV, SoDienThoai, ChucVu) VALUES
(N'Nguyễn Văn A', '0901111111', N'Thu ngân'),
(N'Trần Thị B', '0902222222', N'Quản lý');

INSERT INTO KhachHang (TenKH, DienThoai, DiemTichLuy) VALUES
(N'Lê Văn C', '0911111111', 0),
(N'Phạm Thị D', '0912222222', 10);

INSERT INTO SanPham (MaSP, TenSP, GiaBan, SoLuongTon) VALUES
('SP001', N'Sữa tươi Vinamilk', 25000, 100),
('SP002', N'Bánh mì sandwich', 15000, 50),
('SP003', N'Nước suối Aquafina', 10000, 200);
GO