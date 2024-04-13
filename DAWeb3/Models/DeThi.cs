using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class DeThi
{
    public int IdDeThi { get; set; }

    public DateTime? NgayThi { get; set; }

    public int? ThoiGianThi { get; set; }

    public int? SoLuongCauHoi { get; set; }

    public string? TenDeThi { get; set; }

    public byte? DaXoa { get; set; }

    public int? NguoiTao { get; set; }

    public virtual ICollection<DeThisChiTiet> DeThisChiTiets { get; set; } = new List<DeThisChiTiet>();

    public virtual GiaoVien? NguoiTaoNavigation { get; set; }

    public virtual ICollection<HocSinh> MaThanhViens { get; set; } = new List<HocSinh>();
}
