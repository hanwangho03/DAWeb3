using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAWeb3.Models;

public partial class WebTracNghiemContext : DbContext
{
    public WebTracNghiemContext()
    {
    }

    public WebTracNghiemContext(DbContextOptions<WebTracNghiemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Bai> Bais { get; set; }

    public virtual DbSet<CauHoi> CauHois { get; set; }

    public virtual DbSet<ChiTietKetQua> ChiTietKetQuas { get; set; }

    public virtual DbSet<Chuong> Chuongs { get; set; }

    public virtual DbSet<DapAn> DapAns { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<DeThi> DeThis { get; set; }

    public virtual DbSet<DeThisChiTiet> DeThisChiTiets { get; set; }

    public virtual DbSet<GiaoVien> GiaoViens { get; set; }

    public virtual DbSet<HocSinh> HocSinhs { get; set; }

    public virtual DbSet<KetQua> KetQuas { get; set; }

    public virtual DbSet<Khoi> Khois { get; set; }

    public virtual DbSet<MonHoc> MonHocs { get; set; }

    public virtual DbSet<MucDoKho> MucDoKhos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=THUAN\\MSSQLSERVER02;Database=WebTracNghiem;User Id=sa;Password=123456;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.TaiKhoan);

            entity.ToTable("admins");

            entity.Property(e => e.TaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.MatKhau).HasMaxLength(50);
        });

        modelBuilder.Entity<Bai>(entity =>
        {
            entity.HasKey(e => e.Idbai);

            entity.Property(e => e.Idbai).HasColumnName("idbai");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.Idchuong).HasColumnName("idchuong");
            entity.Property(e => e.Meta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenBai).HasMaxLength(50);

            entity.HasOne(d => d.IdchuongNavigation).WithMany(p => p.Bais)
                .HasForeignKey(d => d.Idchuong)
                .HasConstraintName("FK_Bais_Chuongs");
        });

        modelBuilder.Entity<CauHoi>(entity =>
        {
            entity.HasKey(e => e.IdCauhoi);

            entity.Property(e => e.IdCauhoi).HasColumnName("idCauhoi");
            entity.Property(e => e.CauHoi1)
                .HasMaxLength(250)
                .HasColumnName("CauHoi");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.DapAnA)
                .HasMaxLength(250)
                .HasColumnName("dap_an_a");
            entity.Property(e => e.DapAnB)
                .HasMaxLength(250)
                .HasColumnName("dap_an_b");
            entity.Property(e => e.DapAnC)
                .HasMaxLength(250)
                .HasColumnName("dap_an_c");
            entity.Property(e => e.DapAnD)
                .HasMaxLength(250)
                .HasColumnName("dap_an_d");
            entity.Property(e => e.Dapheduyet)
                .HasDefaultValue((byte)0)
                .HasColumnName("dapheduyet");
            entity.Property(e => e.GhiChu)
                .HasMaxLength(250)
                .HasColumnName("ghi_chu");
            entity.Property(e => e.IdBai).HasColumnName("idBai");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");

            entity.HasOne(d => d.IdBaiNavigation).WithMany(p => p.CauHois)
                .HasForeignKey(d => d.IdBai)
                .HasConstraintName("FK_CauHois_Bais");

            entity.HasOne(d => d.MaDapAnNavigation).WithMany(p => p.CauHois)
                .HasForeignKey(d => d.MaDapAn)
                .HasConstraintName("FK_CauHois_DapAns");

            entity.HasOne(d => d.MaMucDoNavigation).WithMany(p => p.CauHois)
                .HasForeignKey(d => d.MaMucDo)
                .HasConstraintName("FK_CauHois_MucDoKhos");

            entity.HasOne(d => d.NguoitaoNavigation).WithMany(p => p.CauHois)
                .HasForeignKey(d => d.Nguoitao)
                .HasConstraintName("FK_CauHois_GiaoViens");
        });

        modelBuilder.Entity<ChiTietKetQua>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ChiTietKetQua_1");

            entity.ToTable("ChiTietKetQua");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCauhoiDeThi).HasColumnName("idCauhoiDeThi");
            entity.Property(e => e.Idketqua).HasColumnName("idketqua");

            entity.HasOne(d => d.IdCauhoiDeThiNavigation).WithMany(p => p.ChiTietKetQuas)
                .HasForeignKey(d => d.IdCauhoiDeThi)
                .HasConstraintName("FK_ChiTietKetQua_DeThis_ChiTiets");

            entity.HasOne(d => d.IdketquaNavigation).WithMany(p => p.ChiTietKetQuas)
                .HasForeignKey(d => d.Idketqua)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietKetQua_KetQuas");
        });

        modelBuilder.Entity<Chuong>(entity =>
        {
            entity.HasKey(e => e.IdChuong);

            entity.Property(e => e.IdChuong).HasColumnName("idChuong");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.IdMonHoc).HasColumnName("idMonHoc");
            entity.Property(e => e.Meta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tenchuong)
                .HasMaxLength(50)
                .HasColumnName("tenchuong");

            entity.HasOne(d => d.IdMonHocNavigation).WithMany(p => p.Chuongs)
                .HasForeignKey(d => d.IdMonHoc)
                .HasConstraintName("FK_Chuongs_MonHocs");
        });

        modelBuilder.Entity<DapAn>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.DapAn1)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("DapAn");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => new { e.Idgiaovien, e.Idmonhoc });

            entity.Property(e => e.Idgiaovien).HasColumnName("idgiaovien");
            entity.Property(e => e.Idmonhoc).HasColumnName("idmonhoc");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);

            entity.HasOne(d => d.IdgiaovienNavigation).WithMany(p => p.Days)
                .HasForeignKey(d => d.Idgiaovien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Days_GiaoViens");

            entity.HasOne(d => d.IdmonhocNavigation).WithMany(p => p.Days)
                .HasForeignKey(d => d.Idmonhoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Days_MonHocs");
        });

        modelBuilder.Entity<DeThi>(entity =>
        {
            entity.HasKey(e => e.IdDeThi);

            entity.Property(e => e.IdDeThi).HasColumnName("idDeThi");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.NgayThi).HasColumnType("datetime");
            entity.Property(e => e.TenDeThi).HasMaxLength(250);

            entity.HasOne(d => d.NguoiTaoNavigation).WithMany(p => p.DeThis)
                .HasForeignKey(d => d.NguoiTao)
                .HasConstraintName("FK_DeThis_GiaoViens");
        });

        modelBuilder.Entity<DeThisChiTiet>(entity =>
        {
            entity.ToTable("DeThis_ChiTiets");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCauHoi).HasColumnName("idCauHoi");
            entity.Property(e => e.IdDeThi).HasColumnName("idDeThi");

            entity.HasOne(d => d.IdCauHoiNavigation).WithMany(p => p.DeThisChiTiets)
                .HasForeignKey(d => d.IdCauHoi)
                .HasConstraintName("FK_DeThis_ChiTiets_CauHois");

            entity.HasOne(d => d.IdDeThiNavigation).WithMany(p => p.DeThisChiTiets)
                .HasForeignKey(d => d.IdDeThi)
                .HasConstraintName("FK_DeThis_ChiTiets_DeThis");
        });

        modelBuilder.Entity<GiaoVien>(entity =>
        {
            entity.HasKey(e => e.IdGiaovien);

            entity.Property(e => e.IdGiaovien)
                .ValueGeneratedNever()
                .HasColumnName("idGiaovien");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Hoten).HasMaxLength(50);
            entity.Property(e => e.LaTruongBm)
                .HasDefaultValue((byte)0)
                .HasColumnName("LaTruongBM");
            entity.Property(e => e.Magiaovien)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("magiaovien");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("matkhau");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SDT");
        });

        modelBuilder.Entity<HocSinh>(entity =>
        {
            entity.HasKey(e => e.MaThanhVien).HasName("PK_ThanhVien");

            entity.Property(e => e.MaThanhVien)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.DiaChi).HasMaxLength(250);
            entity.Property(e => e.DienThoai)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsFixedLength();
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.IdNhom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idNhom");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.MaKhoiNavigation).WithMany(p => p.HocSinhs)
                .HasForeignKey(d => d.MaKhoi)
                .HasConstraintName("FK_HocSinhs_Khois");

            entity.HasMany(d => d.MaDeThis).WithMany(p => p.MaThanhViens)
                .UsingEntity<Dictionary<string, object>>(
                    "DeThiThanhVien",
                    r => r.HasOne<DeThi>().WithMany()
                        .HasForeignKey("MaDeThi")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DeThi_ThanhVien_DeThis"),
                    l => l.HasOne<HocSinh>().WithMany()
                        .HasForeignKey("MaThanhVien")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_DeThi_ThanhVien_ThanhVien"),
                    j =>
                    {
                        j.HasKey("MaThanhVien", "MaDeThi");
                        j.ToTable("DeThi_ThanhVien");
                        j.IndexerProperty<string>("MaThanhVien")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<KetQua>(entity =>
        {
            entity.HasKey(e => e.IdKetQua);

            entity.Property(e => e.IdKetQua).HasColumnName("idKetQua");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.IdDethi).HasColumnName("idDethi");
            entity.Property(e => e.IdThanhVien)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idThanhVien");

            entity.HasOne(d => d.IdDethiNavigation).WithMany(p => p.KetQuas)
                .HasForeignKey(d => d.IdDethi)
                .HasConstraintName("FK_KetQuas_DeThis");

            entity.HasOne(d => d.IdThanhVienNavigation).WithMany(p => p.KetQuas)
                .HasForeignKey(d => d.IdThanhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KetQuas_HocSinhs");
        });

        modelBuilder.Entity<Khoi>(entity =>
        {
            entity.HasKey(e => e.IdKhoi);

            entity.Property(e => e.IdKhoi).HasColumnName("idKhoi");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.Meta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenKhoi).HasMaxLength(50);
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.IdMonHoc);

            entity.Property(e => e.IdMonHoc).HasColumnName("idMonHoc");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.Meta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenMonHoc).HasMaxLength(50);

            entity.HasOne(d => d.MaKhoiNavigation).WithMany(p => p.MonHocs)
                .HasForeignKey(d => d.MaKhoi)
                .HasConstraintName("FK_MonHocs_Khois");
        });

        modelBuilder.Entity<MucDoKho>(entity =>
        {
            entity.HasKey(e => e.IdDoKho);

            entity.Property(e => e.IdDoKho).HasColumnName("idDoKho");
            entity.Property(e => e.DaXoa).HasDefaultValue((byte)0);
            entity.Property(e => e.TenMucDo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
