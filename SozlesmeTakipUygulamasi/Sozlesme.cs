using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlesmeTakipUygulamasi
{
    public class Sozlesme
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Taraflar { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public double Tutar { get; set; }
        public bool Gecerlilik { get; set; }
        public string DosyaYolu { get; set; }
    }
}
