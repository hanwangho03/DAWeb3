using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class KetQua
{
    public string IdThanhVien { get; set; } = null!;

    public byte? DaXoa { get; set; }

    public long IdKetQua { get; set; }

    public double? TongDiem { get; set; }

    public byte? DaNop { get; set; }

    public int? IdDethi { get; set; }

    public virtual ICollection<ChiTietKetQua> ChiTietKetQuas { get; set; } = new List<ChiTietKetQua>();

    public virtual DeThi? IdDethiNavigation { get; set; }

    public virtual HocSinh IdThanhVienNavigation { get; set; } = null!;
}
