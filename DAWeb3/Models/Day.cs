using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class Day
{
    public int Idgiaovien { get; set; }

    public int Idmonhoc { get; set; }

    public byte? DaXoa { get; set; }

    public virtual GiaoVien IdgiaovienNavigation { get; set; } = null!;

    public virtual MonHoc IdmonhocNavigation { get; set; } = null!;
}
