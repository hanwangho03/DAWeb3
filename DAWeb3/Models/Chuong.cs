using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class Chuong
{
    public int IdChuong { get; set; }

    public string? Tenchuong { get; set; }

    public string? Meta { get; set; }

    public int? IdMonHoc { get; set; }

    public byte? DaXoa { get; set; }

    public virtual ICollection<Bai> Bais { get; set; } = new List<Bai>();

    public virtual MonHoc? IdMonHocNavigation { get; set; }
}
