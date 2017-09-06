using System;
using System.Collections.Generic;

namespace JPalmaresWebApp.Models
{
    public partial class Partite
    {
        public long Id { get; set; }
        public long Idvittoria { get; set; }
        public long Idsquadra1 { get; set; }
        public long Idsquadra2 { get; set; }
        public string Luogo { get; set; }
        public string Data { get; set; }
        public string Tabellino { get; set; }
        public string Identif { get; set; }
        public string Dts { get; set; }
        public string Dcr { get; set; }
        public string Commento { get; set; }
        public long Punteggio1 { get; set; }
        public long Punteggio2 { get; set; }
        public long Puntbcr1 { get; set; }
        public long Puntbcr2 { get; set; }
        public long Ordine { get; set; }
    }
}
