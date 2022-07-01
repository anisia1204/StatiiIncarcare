using System;
using System.Collections.Generic;

namespace StatiiIncarcare.Models.DB
{
    public partial class Priza
    {
        public Priza()
        {
            Rezervares = new HashSet<Rezervare>();
        }

        public int IdPriza { get; set; }
        public int IdTip { get; set; }
        public int IdStatie { get; set; }

        public virtual Statie IdStatieNavigation { get; set; } = null!;
        public virtual TipPriza IdTipNavigation { get; set; } = null!;
        public virtual ICollection<Rezervare> Rezervares { get; set; }
    }
}
