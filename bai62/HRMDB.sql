CREATE DATABASE HRMDB;
GO
USE HRMDB;
GO

-- 1. PhongBan
CREATE TABLE PhongBan(
    MaPB  INT IDENTITY(1,1) PRIMARY KEY,
    TenPB NVARCHAR(100) NOT NULL,
    Mota  NVARCHAR(255)
);

-- 2. NhanVien
CREATE TABLE NhanVien(
    MaNV       INT IDENTITY(1,1) PRIMARY KEY,
    HoTen      NVARCHAR(100) NOT NULL,
    NgaySinh   DATETIME,
    CCCD       VARCHAR(20) UNIQUE,
    MaPB       INT FOREIGN KEY REFERENCES PhongBan(MaPB),
    LuongCoBan DECIMAL(18,2) CHECK (LuongCoBan > 0),
    HeSoLuong  FLOAT DEFAULT 1.0
);

-- 3. BangChamCong
CREATE TABLE BangChamCong(
    MaCong     INT IDENTITY(1,1) PRIMARY KEY,
    MaNV       INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    ThangNam   VARCHAR(7),               -- ví dụ: '11/2026'
    SoNgayCong FLOAT CHECK (SoNgayCong <= 31),
    SoGioOT    FLOAT DEFAULT 0
);

-- 4. BangLuong (Primary key phụ: MaNV + ThangNam)
CREATE TABLE BangLuong(
    MaLuong   INT IDENTITY(1,1) PRIMARY KEY,
    MaNV      INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    ThangNam  VARCHAR(7),
    ThucLinh  DECIMAL(18,2),
    CONSTRAINT UQ_Luong UNIQUE (MaNV, ThangNam)
);
GO

-- Dữ liệu mẫu
INSERT INTO PhongBan(TenPB, Mota) VALUES
(N'Nhân sự',  N'Quản lý nhân sự'),
(N'Kế toán',  N'Quản lý tài chính'),
(N'Kinh doanh', N'Bán hàng & marketing');

INSERT INTO NhanVien(HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) VALUES
(N'Nguyễn Văn An', '1990-05-12', '012345678901', 1, 8000000, 1.5),
(N'Trần Thị Bình', '1995-03-20', '023456789012', 2, 9000000, 2.0),
(N'Lê Văn Cường', '1992-11-08', '034567890123', 3, 7000000, 1.8),
(N'Phạm Thị Dung', '1998-07-25', '045678901234', 1, 6500000, 1.2),
(N'Hoàng Văn Em', '1993-09-15', '056789012345', 3, 7500000, 1.6);
GO
