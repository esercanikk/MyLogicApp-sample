using System;

namespace MyLogicApp_sample.Web.Models
{
    public class IsAtamaViewModel
    {
        public Guid SemaId { get; set; }
        public string CalisanId { get; set; }
        public string Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? SonTarih { get; set; }
    }
}