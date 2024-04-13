using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class GiaoVien
{
    public string? Magiaovien { get; set; }

    public int IdGiaovien { get; set; }

    public string? Matkhau { get; set; }

    public string? Hoten { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public byte? DaXoa { get; set; }

    public byte? LaTruongBm { get; set; }

    public virtual ICollection<CauHoi> CauHois { get; set; } = new List<CauHoi>();

    public virtual ICollection<Day> Days { get; set; } = new List<Day>();

    public virtual ICollection<DeThi> DeThis { get; set; } = new List<DeThi>();
}
