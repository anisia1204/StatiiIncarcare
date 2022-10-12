using StatiiIncarcare.Models.DB;
using System.Runtime.Serialization;

namespace StatiiIncarcare.Models.ViewModels
{
    [DataContract]
    public class Chart
    {
        [DataMember(Name = "label")]
        public string Nume { get; set; }
        [DataMember(Name = "y")]
        public int NumarRezervari { get; set; }
    }
}
