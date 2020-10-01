using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLogicApp_sample.ENTITY.Models.WorkFlow
{
    [Table("Semalar")]
    public class Sema : BaseEntity<Guid>
    {
        public Sema()
        {
            this.Id = Guid.NewGuid();
        }

        [Required, StringLength(50)]
        public string Baslik { get; set; }
        [Required, StringLength(500)]
        public string Aciklama { get; set; }
        public int AdimSayisi { get; set; }

        public virtual ICollection<Baglayici> Baglayicilar { get; set; } = new HashSet<Baglayici>();
    }
}
