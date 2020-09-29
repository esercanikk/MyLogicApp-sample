using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLogicApp_sample.ENTITY.Models
{
    public abstract class BaseEntity2<T1, T2> : BaseEntity<T1>
    {
        [Key]
        [Column(Order = 20)]
        public T2 Id2 { get; set; }
    }
}
