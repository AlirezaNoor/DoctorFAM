﻿using DoctorFAM.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Domain.Entities.Insurance
{
    public class Insurance: BaseEntity
    {
        #region properties

        public string Title { get; set; }

        #endregion
    }
}
