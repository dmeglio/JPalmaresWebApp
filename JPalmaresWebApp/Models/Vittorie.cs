using System;
using System.Collections.Generic;

namespace JPalmaresWebApp.Models
{
    public partial class Vittorie
    {
        public long Id { get; set; }
        public long Idtrofeo { get; set; }
        public string Stagione { get; set; }
        public string Commenti { get; set; }
        public string Ordstagione { get; set; }
        public long? Ordprogressivo { get; set; }
        public string Allenatore { get; set; }
    }
}
