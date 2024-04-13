using System;
using System.Collections.Generic;

namespace DAWeb3.Models;

public partial class DeThisChiTiet
{
    public long Id { get; set; }

    public int? IdDeThi { get; set; }

    public long? IdCauHoi { get; set; }

    public virtual CauHoi? IdCauHoiNavigation { get; set; }

    public virtual DeThi? IdDeThiNavigation { get; set; }

    public virtual ICollection<KetQua> KetQuaIdCauHoiDeThiNavigations { get; set; } = new List<KetQua>();

    public virtual ICollection<KetQua> KetQuaIdCauhoidalamNavigations { get; set; } = new List<KetQua>();
}
