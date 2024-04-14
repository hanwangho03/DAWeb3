using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class DeThisChiTiet
{
    public long Id { get; set; }

    public int? IdDeThi { get; set; }

    public long? IdCauHoi { get; set; }

    public virtual ICollection<ChiTietKetQua> ChiTietKetQuas { get; set; } = new List<ChiTietKetQua>();

    public virtual CauHoi? IdCauHoiNavigation { get; set; }

    public virtual DeThi? IdDeThiNavigation { get; set; }
}
