using Microsoft.AspNetCore.Http;
using Soccer2020.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Soccer2020.Web.Models
{
    public class TournamentViewModel : TournamentEntity
    {
        [Display(Name = "Logo")]
        public IFormFile LogoFile { get; set; }
    }
}