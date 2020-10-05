using System;

namespace MyLogicApp_sample.Models
{
    public class GorevViewModel
    {
        public Guid IsAtamaId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? SonTarih { get; set; }
        public int AdimSirasi { get; set; }
        public int ToplamAdim { get; set; }
        public bool TamamlandiMi { get; internal set; }
        public string YonlendirmeUrl { get; internal set; }
    }
}