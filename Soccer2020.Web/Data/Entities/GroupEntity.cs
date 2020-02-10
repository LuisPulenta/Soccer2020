using System.ComponentModel.DataAnnotations;

namespace Soccer2020.Web.Data.Entities
{
    public class GroupEntity
    {
        public int Id { get; set; }

        [Display(Name = "Grupo")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }

        public TournamentEntity Tournament { get; set; }
    }
}