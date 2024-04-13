using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class KetQua
{
    public string IdThanhVien { get; set; } = null!;

    public long IdCauHoiDeThi { get; set; }

    public double? TongDiem { get; set; }

    public byte? DaXoa { get; set; }

    public long? IdCauhoidalam { get; set; }

    public int? Dapandachon { get; set; }

    public virtual DeThisChiTiet IdCauHoiDeThiNavigation { get; set; } = null!;

    public virtual DeThisChiTiet? IdCauhoidalamNavigation { get; set; }

    public virtual HocSinh IdThanhVienNavigation { get; set; } = null!;
}
