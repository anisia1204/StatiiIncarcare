using StatiiIncarcare.Models.DB;
using System.ComponentModel;

namespace StatiiIncarcare.Models.ViewModels
{
    public class PrizeViewModel
    {
        public int IdStatie { get; set; } 
        [DisplayName("Lista Prize")]
        public IList<TipPriza> TipPrize { get; set; }
        public int IdTipPrize { get; set; }

    }
}
