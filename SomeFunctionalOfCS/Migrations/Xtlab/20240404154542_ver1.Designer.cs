﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SomeFunctionalOfCS.Models;

#nullable disable

namespace SomeFunctionalOfCS.Migrations.Xtlab
{
    [DbContext(typeof(XtlabContext))]
    [Migration("20240404154542_ver1")]
    partial class ver1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SomeFunctionalOfCS.Models.Cungcap", b =>
                {
                    b.Property<int>("CungcapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CungcapID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CungcapId"));

                    b.Property<string>("Diachi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Dienthoai")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MaBuudien")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Quocgia")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TenLienhe")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Tendaydu")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Thanhpho")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("CungcapId")
                        .HasName("PK__Cungcap__C6380F3D8189B72C");

                    b.ToTable("Cungcap", (string)null);
                });

            modelBuilder.Entity("SomeFunctionalOfCS.Models.Danhmuc", b =>
                {
                    b.Property<int>("DanhmucId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DanhmucID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DanhmucId"));

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TenDanhMuc")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("DanhmucId")
                        .HasName("PK__Danhmuc__15F7E73AF6910631");

                    b.ToTable("Danhmuc", (string)null);
                });

            modelBuilder.Entity("SomeFunctionalOfCS.Models.Donhang", b =>
                {
                    b.Property<int>("DonhangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DonhangID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonhangId"));

                    b.Property<int?>("KhachhangId")
                        .HasColumnType("int")
                        .HasColumnName("KhachhangID");

                    b.Property<DateOnly?>("Ngaydathang")
                        .HasColumnType("date");

                    b.Property<int?>("NhanvienId")
                        .HasColumnType("int")
                        .HasColumnName("NhanvienID");

                    b.Property<int?>("ShipperId")
                        .HasColumnType("int")
                        .HasColumnName("ShipperID");

                    b.HasKey("DonhangId")
                        .HasName("PK__Donhang__99AA9CD5F1A1FFFD");

                    b.ToTable("Donhang", (string)null);
                });

            modelBuilder.Entity("SomeFunctionalOfCS.Models.DonhangChitiet", b =>
                {
                    b.Property<int>("DonhangChitietId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DonhangChitietID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonhangChitietId"));

                    b.Property<int?>("DonhangId")
                        .HasColumnType("int")
                        .HasColumnName("DonhangID");

                    b.Property<int?>("SanphamId")
                        .HasColumnType("int")
                        .HasColumnName("SanphamID");

                    b.Property<int?>("Soluong")
                        .HasColumnType("int");

                    b.HasKey("DonhangChitietId")
                        .HasName("PK__DonhangC__96D8B175242FF5AC");

                    b.ToTable("DonhangChitiet", (string)null);
                });

            modelBuilder.Entity("SomeFunctionalOfCS.Models.Khachhang", b =>
                {
                    b.Property<int>("KhachhangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("KhachhangID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KhachhangId"));

                    b.Property<string>("Diachi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HoTen")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MaBuudien")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("QuocGia")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TenLienLac")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Thanhpho")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("KhachhangId")
                        .HasName("PK__Khachhan__800808009F79E3CC");

                    b.ToTable("Khachhang", (string)null);
                });

            modelBuilder.Entity("SomeFunctionalOfCS.Models.Nhanvien", b =>
                {
                    b.Property<int>("NhanVienId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("NhanVienID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NhanVienId"));

                    b.Property<string>("Anh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Ghichu")
                        .HasColumnType("text");

                    b.Property<string>("Ho")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateOnly?>("NgaySinh")
                        .HasColumnType("date");

                    b.Property<string>("Ten")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("NhanVienId")
                        .HasName("PK__Nhanvien__92550447826C7F23");

                    b.ToTable("Nhanvien", (string)null);
                });

            modelBuilder.Entity("SomeFunctionalOfCS.Models.Sanpham", b =>
                {
                    b.Property<int>("SanphamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SanphamID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SanphamId"));

                    b.Property<int?>("CungcapId")
                        .HasColumnType("int")
                        .HasColumnName("CungcapID");

                    b.Property<int?>("DanhmucId")
                        .HasColumnType("int")
                        .HasColumnName("DanhmucID");

                    b.Property<string>("Donvi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("Gia")
                        .HasColumnType("decimal(13, 2)");

                    b.Property<string>("TenSanpham")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("SanphamId")
                        .HasName("PK__Sanpham__BFF15FAC5C136BC2");

                    b.ToTable("Sanpham", (string)null);
                });

            modelBuilder.Entity("SomeFunctionalOfCS.Models.Shipper", b =>
                {
                    b.Property<int>("ShipperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ShipperID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShipperId"));

                    b.Property<string>("Hoten")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Sodienthoai")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ShipperId")
                        .HasName("PK__Shippers__1F8AFFB9BE331A08");

                    b.ToTable("Shippers");
                });
#pragma warning restore 612, 618
        }
    }
}
