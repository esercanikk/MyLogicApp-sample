using System;
using System.Collections.Generic;

namespace MyLogicApp_sample.Web.Models
{
    public class SemaViewModel
    {
        public Guid Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public List<AdimViewModel> Adimlar { get; set; }
    }
}