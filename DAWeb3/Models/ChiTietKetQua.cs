using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class ChiTietKetQua
{
    public long Idketqua { get; set; }

    public long? IdCauhoiDeThi { get; set; }

    public byte? IdDapAnDaChon { get; set; }

    public byte? IdDapAnDung { get; set; }

    public long Id { get; set; }

    public virtual DeThisChiTiet? IdCauhoiDeThiNavigation { get; set; }

    public virtual KetQua IdketquaNavigation { get; set; } = null!;
}
