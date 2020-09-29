using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLogicApp_sample.ENTITY.Models.WorkFlow
{
    /// <summary>
    /// İki adımı birbirine bağlar.
    /// </summary>
    /// <param name="Id">Önceki adım</param>
    /// <param name="Id2">Sonraki adım</param>
    [Table("Baglayicilar")]
    public class Baglayici : BaseEntity2<Guid, Guid>
    {
        public Guid SemaId { get; set; }
        [ForeignKey(nameof(SemaId))]
        public virtual Sema Sema { get; set; }
        [ForeignKey(nameof(Id))]
        public virtual Adim OncekiAdim { get; set; }
        [ForeignKey(nameof(Id2))]
        public virtual Adim SonrakiAdim { get; set; }
    }
}
