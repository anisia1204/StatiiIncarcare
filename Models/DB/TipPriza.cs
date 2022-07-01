using System;
using System.Collections.Generic;

namespace StatiiIncarcare.Models.DB
{
    public partial class TipPriza
    {
        public TipPriza()
        {
            Prizas = new HashSet<Priza>();
        }

        public int IdTip { get; set; }
        public string? NumePriza { get; set; }

        public virtual ICollection<Priza> Prizas { get; set; }
    }
}
