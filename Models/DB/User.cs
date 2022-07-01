using System;
using System.Collections.Generic;

namespace StatiiIncarcare.Models.DB
{
    public partial class User
    {
        public User()
        {
            Rezervares = new HashSet<Rezervare>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int IdUser { get; set; }

        public virtual ICollection<Rezervare> Rezervares { get; set; }
    }
}
