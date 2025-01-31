﻿using DoctorFAM.Domain.Entities.BMI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Domain.Interfaces
{
    public interface IBMIRepository 
    {
        #region Site Side 

        //Add BMI To Data Base 
        Task CreateBMI(BMI bmi);

        //Add GFR To Data Base 
        Task CreateGFR(GFR gfr);

        #endregion

        #region User Panel 

        //Get List Of User BMI History
        Task<List<BMI>?> GetUserBMIHistory(ulong userId);

        //Get List Of User GFR History
        Task<List<GFR>?> GetUserGFRHistory(ulong userId);

        #endregion
    }
}
