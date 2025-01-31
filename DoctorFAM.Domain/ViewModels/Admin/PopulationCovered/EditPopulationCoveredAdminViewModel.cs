﻿using DoctorFAM.Domain.Enums.Gender;
using DoctorFAM.Domain.Enums.InsuranceType;
using DoctorFAM.Domain.Enums.Population_Covered;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Domain.ViewModels.Admin.PopulationCovered
{
    public class EditPopulationCoveredAdminViewModel
    {
        #region properties

        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        public ulong UserId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public string Name { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public string LastName { get; set; }

        [Display(Name = "NationalId")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public string NationalId { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public Gender Gender { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public int Age { get; set; }

        [Display(Name = "InsuranceType")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public ulong InsuranceId { get; set; }

        public ulong Id { get; set; }

        [Display(Name = "Ratio")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public Ratio Ratio { get; set; }

        [Display(Name = "BirthDay")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [RegularExpression(@"^\d{4}\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$")]
        public string BirthDay { get; set; }

        #endregion
    }

    public enum EditPopulationCoveredAdminResult
    {
        Success,
        Faild
    }
}
