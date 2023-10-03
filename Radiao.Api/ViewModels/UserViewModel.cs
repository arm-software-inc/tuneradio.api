using System.ComponentModel.DataAnnotations;

namespace Radiao.Api.ViewModels
{
    public class UserViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Campo Email obrigatório")]
        [MaxLength(255, ErrorMessage = "Máximo de 255 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Senha obrigatório")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo Nome obrigatório")]
        [MaxLength(50, ErrorMessage = "Máximo de 50 caracteres")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; private set; }
    }
}
