using System;
using System.Collections.Generic;

namespace JPalmaresWebApp.Models
{
    public partial class Trofei
    {
        public long Id { get; set; }
        public string Trofeo { get; set; }
        public string Immagine { get; set; }
        public string Bigimmagine { get; set; }
        public string Ufficiale { get; set; }
        public string Antico { get; set; }
        public string Amichevole { get; set; }
        public string Importante { get; set; }
        public long Ordprogressivo { get; set; }
    }
}
