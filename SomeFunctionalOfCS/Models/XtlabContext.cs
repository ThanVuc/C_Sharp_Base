using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SomeFunctionalOfCS.Models;

public partial class XtlabContext : DbContext
{
    public XtlabContext()
    {
    }

    public XtlabContext(DbContextOptions<XtlabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cungcap> Cungcaps { get; set; }

    public virtual DbSet<Danhmuc> Danhmucs { get; set; }

    public virtual DbSet<Donhang> Donhangs { get; set; }

    public virtual DbSet<DonhangChitiet> DonhangChitiets { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=xtlab;User ID=SA;Password=88888888;TrustServerCertificate = True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cungcap>(entity =>
        {
            entity.HasKey(e => e.CungcapId).HasName("PK__Cungcap__C6380F3D8189B72C");

            entity.ToTable("Cungcap");

            entity.Property(e => e.CungcapId).HasColumnName("CungcapID");
            entity.Property(e => e.Diachi).HasMaxLength(255);
            entity.Property(e => e.Dienthoai).HasMaxLength(255);
            entity.Property(e => e.MaBuudien).HasMaxLength(255);
            entity.Property(e => e.Quocgia).HasMaxLength(255);
            entity.Property(e => e.TenLienhe).HasMaxLength(255);
            entity.Property(e => e.Tendaydu).HasMaxLength(255);
            entity.Property(e => e.Thanhpho).HasMaxLength(255);
        });

        modelBuilder.Entity<Danhmuc>(entity =>
        {
            entity.HasKey(e => e.DanhmucId).HasName("PK__Danhmuc__15F7E73AF6910631");

            entity.ToTable("Danhmuc");

            entity.Property(e => e.DanhmucId).HasColumnName("DanhmucID");
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(255);
        });

        modelBuilder.Entity<Donhang>(entity =>
        {
            entity.HasKey(e => e.DonhangId).HasName("PK__Donhang__99AA9CD5F1A1FFFD");

            entity.ToTable("Donhang");

            entity.Property(e => e.DonhangId).HasColumnName("DonhangID");
            entity.Property(e => e.KhachhangId).HasColumnName("KhachhangID");
            entity.Property(e => e.NhanvienId).HasColumnName("NhanvienID");
            entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
        });

        modelBuilder.Entity<DonhangChitiet>(entity =>
        {
            entity.HasKey(e => e.DonhangChitietId).HasName("PK__DonhangC__96D8B175242FF5AC");

            entity.ToTable("DonhangChitiet");

            entity.Property(e => e.DonhangChitietId).HasColumnName("DonhangChitietID");
            entity.Property(e => e.DonhangId).HasColumnName("DonhangID");
            entity.Property(e => e.SanphamId).HasColumnName("SanphamID");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.KhachhangId).HasName("PK__Khachhan__800808009F79E3CC");

            entity.ToTable("Khachhang");

            entity.Property(e => e.KhachhangId).HasColumnName("KhachhangID");
            entity.Property(e => e.Diachi).HasMaxLength(255);
            entity.Property(e => e.HoTen).HasMaxLength(255);
            entity.Property(e => e.MaBuudien).HasMaxLength(255);
            entity.Property(e => e.QuocGia).HasMaxLength(255);
            entity.Property(e => e.TenLienLac).HasMaxLength(255);
            entity.Property(e => e.Thanhpho).HasMaxLength(255);
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.NhanVienId).HasName("PK__Nhanvien__92550447826C7F23");

            entity.ToTable("Nhanvien");

            entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");
            entity.Property(e => e.Anh).HasMaxLength(255);
            entity.Property(e => e.Ghichu).HasColumnType("text");
            entity.Property(e => e.Ho).HasMaxLength(255);
            entity.Property(e => e.Ten).HasMaxLength(255);
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.SanphamId).HasName("PK__Sanpham__BFF15FAC5C136BC2");

            entity.ToTable("Sanpham");

            entity.Property(e => e.SanphamId).HasColumnName("SanphamID");
            entity.Property(e => e.CungcapId).HasColumnName("CungcapID");
            entity.Property(e => e.DanhmucId).HasColumnName("DanhmucID");
            entity.Property(e => e.Donvi).HasMaxLength(255);
            entity.Property(e => e.Gia).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.TenSanpham).HasMaxLength(255);
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.ShipperId).HasName("PK__Shippers__1F8AFFB9BE331A08");

            entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
            entity.Property(e => e.Hoten).HasMaxLength(255);
            entity.Property(e => e.Sodienthoai).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
