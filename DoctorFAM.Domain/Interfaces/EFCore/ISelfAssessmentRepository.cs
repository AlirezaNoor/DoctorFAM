﻿using DoctorFAM.Domain.Entities.SelfAssessment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Domain.Interfaces.EFCore
{
    public interface ISelfAssessmentRepository
    {
        #region Blood Pressure Self Assessment 

        #region Site Side 

        //Add Blood Pressure Self Assessment To The Data Base 
        Task AddBloodPressureSelfAssessmentToTheDataBase(BloodPressureSelfAssessment model);

        #endregion

        #endregion

        #region Diabet Self Assessment 

        #region Site Side 

        //Add Diabet Self Assessment To The Data Base 
        Task AddDiabetSelfAssessmentToTheDataBase(DiabetSelfAssessment model);

        #endregion

        #region User Panel 



        #endregion

        #endregion
    }
}