using System.ComponentModel.DataAnnotations;

namespace MyLogicApp_sample.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Şifreniz en az 5 karakter olmalıdır!")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public string Id { get; set; }
    }
}