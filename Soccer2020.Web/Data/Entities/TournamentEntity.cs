﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Soccer2020.Web.Data.Entities
{
         public class TournamentEntity
        {
            public int Id { get; set; }

            [Display(Name = "Torneo")]
            [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
            [Required(ErrorMessage = "El campo {0} es requerido.")]
            public string Name { get; set; }

            [DataType(DataType.DateTime)]
            [Display(Name = "Fecha Inicio")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
            public DateTime StartDate { get; set; }

            [DataType(DataType.DateTime)]
            [Display(Name = "Fecha Inicio")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
            public DateTime StartDateLocal => StartDate.ToLocalTime();

            [DataType(DataType.DateTime)]
            [Display(Name = "Fecha Fin")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
            public DateTime EndDate { get; set; }

            [DataType(DataType.DateTime)]
            [Display(Name = "Fecha Fin")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
            public DateTime EndDateLocal => EndDate.ToLocalTime();

            [Display(Name = "Está Activo?")]
            public bool IsActive { get; set; }

            [Display(Name = "Logo")]
            public string LogoPath { get; set; }

            public ICollection<GroupEntity> Groups { get; set; }
    }   
}