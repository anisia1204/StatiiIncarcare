using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StatiiIncarcare.Models.DB
{
    public partial class Statie
    {
        public Statie()
        {
            Prizas = new HashSet<Priza>();
        }

        public int IdStatie { get; set; }
        [Required]
        public string? Adresa { get; set; }
        [Required]
        public string? Nume { get; set; }
        [Required]
        public string? Oras { get; set; }

        public virtual ICollection<Priza> Prizas { get; set; }
    }
}
