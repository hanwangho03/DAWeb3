using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class DapAn
{
    public int Id { get; set; }

    public string? DapAn1 { get; set; }

    public byte? DaXoa { get; set; }

    public virtual ICollection<CauHoi> CauHois { get; set; } = new List<CauHoi>();
}
