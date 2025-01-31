﻿using DoctorFAM.Domain.Entities.PriodicExamination;
using DoctorFAM.Domain.ViewModels.Admin.MedicalExamination;
using DoctorFAM.Domain.ViewModels.Common;
using DoctorFAM.Domain.ViewModels.Site.MedicalExamination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Application.Services.Interfaces
{
    public interface IMedicalExaminationService 
    {
        #region Admin Side 

        //Create Medical Examination From Admin 
        Task<bool> CreateMedicalExaminationFromAdmin(CreateMEdicalExaminationAdminSideViewModel model);

        //Filter Medical Examination 
        Task<FilterMedicalExaminationAdminSideViewModel> FilterMedicalExamination(FilterMedicalExaminationAdminSideViewModel filter);

        //Get Medical Examination By Id 
        Task<MedicalExamination?> GetMedicalExaminationById(ulong medicalExaminationId);

        //Fill Edit Medical Examination Admin Side View Model 
        Task<EditMedicalExaminationAdminSideViewModel?> FillEditMedicalExaminationAdminSideViewModel(ulong medicalExaminationId);

        //Edit Medical Examination Admin Side
        Task<bool> EditMedicalExaminationAdminSide(EditMedicalExaminationAdminSideViewModel model);

        //Delete Medical Examination Admin Side 
        Task<bool> DeleteMedicalExaminationAdminSide(ulong medicalExamination);

        #endregion

        #region Site Side 

        //Get List Of Medical Examinations
        Task<List<MedicalExamination>?> GetListOfMedicalExaminations();

        //Get List Of Medical Examinations With Select List 
        Task<List<SelectListViewModel>> GetListOfMedicalExaminationsWithSelectList();

        //Create Priodic Examination From Site Side 
        Task<CreatePriodicEcaminationFromUser> CreatePriodicPatientExaminationSiteSideViewModel(CreatePriodicPatientExaminationSiteSideViewModel model, ulong userId);

        //List Of User Priodic Patient Examination 
        Task<List<ListOfUserPriodicExaminationSiteSideViewModel>?> ListOfUserPriodicPatientExamination(ulong userId);

        //Get Priodic Examination By Priodic Examination Id
        Task<PriodicPatientsExamination?> GetPriodicExaminationByPriodicExaminationId(ulong priodicExaminationId);

        //Delete Priodic Examination From User
        Task<bool> DeletePriodicExaminationFromUser(ulong priodicExaminationId, ulong userId);

        //Check That Current User Has Any Priodic Examination In This Week
        Task<List<PriodicPatientsExamination>?> CheckThatCurrentUserHasAnyPriodicExaminationInThisWeek(ulong userId);

        #endregion

        #region User Panel Side

        //Check That Current User Has Any Priodic Examination After Today
        Task<List<PriodicPatientsExamination>?> CheckThatCurrentUserHasAnyPriodicExaminationAfterToday(ulong userId);

        #endregion

        #region Background Task

        //Send Alert SMS For Medical Examination Alarm
        Task GetListOfUserMedicalExaminationForSendSMSOneDayBefore();

        #endregion
    }
}
