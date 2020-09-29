using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLogicApp_sample.ENTITY.Models
{
    [Table("Adimlar")]
    public class Adim : BaseEntity<Guid>
    {
        public Adim()
        {
            this.Id = Guid.NewGuid();
        }
        public string YonlendirmeUrl { get; set; }
    }
}
