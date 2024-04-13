using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class Khoi
{
    public int IdKhoi { get; set; }

    public string? TenKhoi { get; set; }

    public string? Meta { get; set; }

    public byte? DaXoa { get; set; }

    public virtual ICollection<HocSinh> HocSinhs { get; set; } = new List<HocSinh>();

    public virtual ICollection<MonHoc> MonHocs { get; set; } = new List<MonHoc>();
}
