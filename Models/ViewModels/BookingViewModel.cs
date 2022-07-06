using System.ComponentModel.DataAnnotations;

namespace StatiiIncarcare.Models.ViewModels
{
    public class BookingViewModel
    {
        public int IdPriza { get; set; }
        public int IdStatie { get; set; }
        [Required]
        public DateTime StartDate { get; set; } 
        [Required]
        public DateTime EndDate { get; set; } 
        [Required]
        public string? NrMasina { get; set; }    
    }
}
