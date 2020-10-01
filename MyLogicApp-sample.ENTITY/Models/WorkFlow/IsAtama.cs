using MyLogicApp_sample.ENTITY.IdentityModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLogicApp_sample.ENTITY.Models.WorkFlow
{
    [Table("IsAtamalar")]
    public class IsAtama : BaseEntity<Guid>
    {
        public IsAtama()
        {
            this.Id = Guid.NewGuid();
        }
        public string Aciklama { get; set; }
        public DateTime? SonTarih { get; set; }
        public bool TamamlandiMi { get; set; }
        public DateTime? TamamlanmaTarihi { get; set; }
        public Guid SonAdimId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public string AtananKullaniciId { get; set; }
        public Guid SemaId { get; set; }

        [ForeignKey(nameof(AtananKullaniciId))]
        public virtual User AtananKullanici { get; set; }
        [ForeignKey(nameof(SemaId))]
        public virtual Sema Sema { get; set; }
    }
}
