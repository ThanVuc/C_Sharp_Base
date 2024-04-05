IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Cungcap] (
    [CungcapID] int NOT NULL IDENTITY,
    [Tendaydu] nvarchar(255) NULL,
    [TenLienhe] nvarchar(255) NULL,
    [Diachi] nvarchar(255) NULL,
    [Thanhpho] nvarchar(255) NULL,
    [MaBuudien] nvarchar(255) NULL,
    [Quocgia] nvarchar(255) NULL,
    [Dienthoai] nvarchar(255) NULL,
    CONSTRAINT [PK__Cungcap__C6380F3D8189B72C] PRIMARY KEY ([CungcapID])
);
GO

CREATE TABLE [Danhmuc] (
    [DanhmucID] int NOT NULL IDENTITY,
    [TenDanhMuc] nvarchar(255) NULL,
    [MoTa] nvarchar(255) NULL,
    CONSTRAINT [PK__Danhmuc__15F7E73AF6910631] PRIMARY KEY ([DanhmucID])
);
GO

CREATE TABLE [Donhang] (
    [DonhangID] int NOT NULL IDENTITY,
    [KhachhangID] int NULL,
    [NhanvienID] int NULL,
    [Ngaydathang] date NULL,
    [ShipperID] int NULL,
    CONSTRAINT [PK__Donhang__99AA9CD5F1A1FFFD] PRIMARY KEY ([DonhangID])
);
GO

CREATE TABLE [DonhangChitiet] (
    [DonhangChitietID] int NOT NULL IDENTITY,
    [DonhangID] int NULL,
    [SanphamID] int NULL,
    [Soluong] int NULL,
    CONSTRAINT [PK__DonhangC__96D8B175242FF5AC] PRIMARY KEY ([DonhangChitietID])
);
GO

CREATE TABLE [Khachhang] (
    [KhachhangID] int NOT NULL IDENTITY,
    [HoTen] nvarchar(255) NULL,
    [TenLienLac] nvarchar(255) NULL,
    [Diachi] nvarchar(255) NULL,
    [Thanhpho] nvarchar(255) NULL,
    [MaBuudien] nvarchar(255) NULL,
    [QuocGia] nvarchar(255) NULL,
    CONSTRAINT [PK__Khachhan__800808009F79E3CC] PRIMARY KEY ([KhachhangID])
);
GO

CREATE TABLE [Nhanvien] (
    [NhanVienID] int NOT NULL IDENTITY,
    [Ten] nvarchar(255) NULL,
    [Ho] nvarchar(255) NULL,
    [NgaySinh] date NULL,
    [Anh] nvarchar(255) NULL,
    [Ghichu] text NULL,
    CONSTRAINT [PK__Nhanvien__92550447826C7F23] PRIMARY KEY ([NhanVienID])
);
GO

CREATE TABLE [Sanpham] (
    [SanphamID] int NOT NULL IDENTITY,
    [TenSanpham] nvarchar(255) NULL,
    [CungcapID] int NULL,
    [DanhmucID] int NULL,
    [Donvi] nvarchar(255) NULL,
    [Gia] decimal(13,2) NULL,
    CONSTRAINT [PK__Sanpham__BFF15FAC5C136BC2] PRIMARY KEY ([SanphamID])
);
GO

CREATE TABLE [Shippers] (
    [ShipperID] int NOT NULL IDENTITY,
    [Hoten] nvarchar(255) NULL,
    [Sodienthoai] nvarchar(255) NULL,
    CONSTRAINT [PK__Shippers__1F8AFFB9BE331A08] PRIMARY KEY ([ShipperID])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240404154542_ver1', N'9.0.0-preview.2.24128.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240404154622_ver2', N'9.0.0-preview.2.24128.4');
GO

COMMIT;
GO

