using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class Bai
{
    public int Idbai { get; set; }

    public string? TenBai { get; set; }

    public string? Meta { get; set; }

    public int? Idchuong { get; set; }

    public byte? DaXoa { get; set; }

    public virtual ICollection<CauHoi> CauHois { get; set; } = new List<CauHoi>();

    public virtual Chuong? IdchuongNavigation { get; set; }
}
