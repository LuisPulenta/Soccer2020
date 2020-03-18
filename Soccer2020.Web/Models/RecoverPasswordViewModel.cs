using System.ComponentModel.DataAnnotations;

namespace Soccer2020.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}