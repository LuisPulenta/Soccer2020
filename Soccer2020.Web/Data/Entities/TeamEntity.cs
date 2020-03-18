using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Soccer2020.Web.Data.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }
        
        [Display(Name = "Equipo")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Logo")]
        public string LogoPath { get; set; }
        
        public ICollection<GroupDetailEntity> GroupDetails { get; set; }
        
        public ICollection<UserEntity> Users { get; set; }

        [Display(Name = "Logo")]
        public string LogoFullPath => string.IsNullOrEmpty(LogoPath)
           ? "noimage"//null
           : $"http://keypress.serveftp.net:88/Soccer2020Api{LogoPath.Substring(1)}";
    }
}