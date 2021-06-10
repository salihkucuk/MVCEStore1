using System.ComponentModel.DataAnnotations;

namespace MVCEStoreWeb.Models
{
    public class LoginViewModel
{       
        [Display(Name = "E-Posta")]
        [Required(ErrorMessage ="{0} alanı boş bırakılmaz!")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir e-posta adresi yazınız!")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmaz!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Oturum Açık Kalsın")]
        public bool IsPersistent { get; set; }

        public string ReturnUrl { get; set; }

    }
}
