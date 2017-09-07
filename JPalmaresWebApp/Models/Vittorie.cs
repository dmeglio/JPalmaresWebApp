using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JPalmaresWebApp.Models
{
    public partial class Vittorie
    {
        public long Id { get; set; }
        [ForeignKey("Trofei")]
        public long Idtrofeo { get; set; }
        public string Stagione { get; set; }
        public string Commenti { get; set; }
        public string Ordstagione { get; set; }
        public long? Ordprogressivo { get; set; }
        public string Allenatore { get; set; }

        public Trofei Trofei { get; set; }
        public ICollection<Partite> Partites { get; set; }
    }
}
