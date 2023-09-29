using System.ComponentModel.DataAnnotations;

namespace Radiao.Api.ViewModels
{
    public class RecoveryPasswordViewModel
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }
    }
}
