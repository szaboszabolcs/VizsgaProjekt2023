using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_Adatkarbantartas.Models
{
    public partial class Filmek
    {
        public int Id { get; set; }
        public string Cim { get; set; }
        public string Kategoria { get; set; }
        public string Leiras { get; set; }
        public decimal Ertekeles { get; set; }
        public byte[] Filmlogo { get; set; }
    }
}
