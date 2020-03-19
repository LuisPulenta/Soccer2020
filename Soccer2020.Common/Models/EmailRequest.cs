using System.ComponentModel.DataAnnotations;

namespace Soccer2020.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}