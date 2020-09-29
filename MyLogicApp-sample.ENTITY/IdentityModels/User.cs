using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace MyLogicApp_sample.ENTITY.IdentityModels
{
    public class User : IdentityUser
    {
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(35)]
        public string Surname { get; set; }
        [StringLength(11)]
        public string AltPhoneNumber { get; set; }
    }
}
