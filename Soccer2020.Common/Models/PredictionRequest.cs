using System;
using System.ComponentModel.DataAnnotations;

namespace Soccer2020.Common.Models
{
    public class PredictionRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int TournamentId { get; set; }
    }
}