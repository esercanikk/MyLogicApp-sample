using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogicApp_sample.ENTITY.Models
{
    public abstract class BaseEntity<TId> : AuditBase
    {
        [Key]
        [Column(Order = 1)]
        public TId Id { get; set; }
    }
}
