using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaQualquer.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato incorreto.")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [DisplayName("Senha")]
        public string Password { get; set; }
    }
}