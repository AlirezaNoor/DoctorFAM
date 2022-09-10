﻿using DoctorFAM.Domain.Entities.Doctors;
using DoctorFAM.Domain.Entities.Interest;
using DoctorFAM.Domain.ViewModels.Admin.Doctors.DoctorsInfo;
using DoctorFAM.Domain.ViewModels.DoctorPanel.DoctorsInfo;
using DoctorFAM.Domain.ViewModels.DoctorPanel.DosctorSideBarInfo;
using DoctorFAM.Domain.ViewModels.DoctorPanel.Employees;
using DoctorFAM.Domain.ViewModels.Site.Doctor;
using DoctorFAM.Domain.ViewModels.Site.Reservation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Application.Services.Interfaces
{
    public interface IDoctorsService
    {
        #region Doctors Panel Side

        Task<FilterDoctorOfficeEmployeesViewmodel> FilterDoctorOfficeEmployees(FilterDoctorOfficeEmployeesViewmodel filter);

        Task<bool> IsExistAnyDoctorInfoByUserId(ulong userId);

        Task<DoctorSideBarViewModel> GetDoctorsSideBarInfo(ulong userId);

        Task<DoctorsInfo?> GetDoctorsInformationByUserId(ulong userId);

        Task<ManageDoctorsInfoViewModel?> FillManageDoctorsInfoViewModel(ulong userId);

        Task<AddOrEditDoctorInfoResult> AddOrEditDoctorInfoDoctorsPanel(ManageDoctorsInfoViewModel model, IFormFile? MediacalFile);

        Task<Doctor?> GetDoctorByUserId(ulong userId);

        Task<Doctor?> GetDoctorById(ulong doctorId);

        Task<bool> IsExistAnyDoctorByUserId(ulong userId);

        Task AddDoctorForFirstTime(ulong userId);

        Task<List<DoctorsInterestInfo>> GetDoctorInterestsInfo();

        Task<DoctorInterestsViewModel> FillDoctorInterestViewModelFromDoctorPanel(ulong userId);

        Task<DoctorSelectedInterestResult> AddDoctorSelectedInterest(ulong interestId, ulong userId);

        Task<DoctorSelectedInterestResult> DeleteDoctorSelectedInterestDoctorPanel(ulong interestId, ulong userId);

        #endregion

        #region Admin Side 

        Task<ListOfDoctorsInfoViewModel> FilterDoctorsInfoAdminSide(ListOfDoctorsInfoViewModel filter);

        Task<DoctorsInfoDetailViewModel?> FillDoctorsInfoDetailViewModel(ulong doctorInfoId);

        Task<DoctorsInfo?> GetDoctorsInfoById(ulong doctorInfoId);

        Task<EditDoctorInfoResult> EditDoctorInfoAdminSide(DoctorsInfoDetailViewModel model, IFormFile? MediacalFile);

        #endregion

        #region Site Side 

        //Get List Of All Doctors
        Task<List<ListOfAllDoctorsViewModel>> ListOfDoctors();

        //Fill Doctor Page In Reservation Page 
        Task<DoctorPageInReservationViewModel?> FillDoctorPageDetailInReservationPage(ulong userId);

        //Fill Doctor Reservation Detail For Show Site Side View Model
        Task<ShowDoctorReservationDetailViewModel?> FillDoctorReservationDetailForShowSiteSide(ulong userId, string? loggedDateTime);

        //Get Doctro For Send Notification For Take Reservation Notification 
        Task<string?> GetDoctroForSendNotificationForTakeReservationNotification(ulong reservationDateTimeId);

        #endregion
    }
}
