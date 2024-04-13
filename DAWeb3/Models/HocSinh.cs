using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class HocSinh
{
    public string MaThanhVien { get; set; } = null!;

    public string? MatKhau { get; set; }

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? DienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? IdNhom { get; set; }

    public int? MaKhoi { get; set; }

    public byte? DaXoa { get; set; }

    public virtual ICollection<KetQua> KetQuas { get; set; } = new List<KetQua>();

    public virtual Khoi? MaKhoiNavigation { get; set; }

    public virtual ICollection<DeThi> MaDeThis { get; set; } = new List<DeThi>();
}
