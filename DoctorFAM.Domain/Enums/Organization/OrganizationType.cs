﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Domain.Enums.Organization
{
    public enum OrganizationType
    {
        [Display(Name = "DoctorOffice")]
        DoctorOffice,

        [Display(Name = "Laboratory")]
        Labratory,

        [Display(Name = "Pharmacy")]
        Pharmacy
    }
}
