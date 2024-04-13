using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class Admin
{
    public string TaiKhoan { get; set; } = null!;

    public string? MatKhau { get; set; }

    public byte? DaXoa { get; set; }
}
