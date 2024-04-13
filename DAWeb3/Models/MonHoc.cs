using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class MonHoc
{
    public int IdMonHoc { get; set; }

    public string? TenMonHoc { get; set; }

    public string? Meta { get; set; }

    public int? MaKhoi { get; set; }

    public byte? DaXoa { get; set; }

    public virtual ICollection<Chuong> Chuongs { get; set; } = new List<Chuong>();

    public virtual ICollection<Day> Days { get; set; } = new List<Day>();

    public virtual Khoi? MaKhoiNavigation { get; set; }
}
