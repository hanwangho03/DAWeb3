using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class CauHoi
{
    public long IdCauhoi { get; set; }

    public string? CauHoi1 { get; set; }

    public string? DapAnA { get; set; }

    public string? DapAnB { get; set; }

    public string? DapAnC { get; set; }

    public string DapAnD { get; set; } = null!;

    public int? MaDapAn { get; set; }

    public string? GhiChu { get; set; }

    public byte? Dapheduyet { get; set; }

    public int? IdBai { get; set; }

    public int? MaMucDo { get; set; }

    public int? Nguoitao { get; set; }

    public DateTime? NgayTao { get; set; }

    public byte? DaXoa { get; set; }

    public virtual ICollection<DeThisChiTiet> DeThisChiTiets { get; set; } = new List<DeThisChiTiet>();

    public virtual Bai? IdBaiNavigation { get; set; }

    public virtual DapAn? MaDapAnNavigation { get; set; }

    public virtual MucDoKho? MaMucDoNavigation { get; set; }

    public virtual GiaoVien? NguoitaoNavigation { get; set; }
}
