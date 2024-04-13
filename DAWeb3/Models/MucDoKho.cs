using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class MucDoKho
{
    public int IdDoKho { get; set; }

    public string? TenMucDo { get; set; }

    public byte? DaXoa { get; set; }

    public virtual ICollection<CauHoi> CauHois { get; set; } = new List<CauHoi>();
}
