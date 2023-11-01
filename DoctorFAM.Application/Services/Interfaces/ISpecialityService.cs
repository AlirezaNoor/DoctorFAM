﻿#region proeprties

using DoctorFAM.Domain.Entities.Speciality;
using DoctorFAM.Domain.ViewModels.Admin.Speciality;
using DoctorFAM.Domain.ViewModels.Site.Specialists;
using DoctorFAM.Domain.ViewModels.UserPanel.FamilyDoctor;

namespace DoctorFAM.Application.Services.Interfaces;

#endregion

public interface ISpecialityService
{
    #region Admin Side 

    //List Of Specilaity Admin Side 
    Task<FilterSpecialityViewModel> ListOfSpecilaityAdminSide(FilterSpecialityViewModel filter);

    //Get Speciality By Id 
    Task<Speciality?> GetSpecialityById(ulong specialityId);

    //Check That Is Exist Any Speciality With This Unique Name 
    Task<bool> CheckThatExistAnySpecialityWithThisUniqueName(string uniqueName);

    //Check That Is Exist Any Speciality With This Unique Id 
    Task<bool> CheckThatExistAnySpecialityWithThisUniqueId(ulong uniqueId);

    //Is Exist Speciality By Speciality Id
    Task<bool> IsExistSpecialityBySpecialityId(ulong speciality);

    //Create Speciality
    Task<CreateSpecialityResult> CreateSpeciality(CreateSpecialityViewModel Speciality);

    //Fill Edit Speciality View Model 
    Task<EditSpecialityViewModel?> FillEditSpecialityViewModel(ulong specialityId);

    //Edit Speciality
    Task<EditSpecialityResult> EditSpeciality(EditSpecialityViewModel SpecialityViewModel);

    Task<bool> DeleteSpeciality(ulong specialityId);

    //Get List Of Specialities 
    Task<List<SpecialtiyInfo>> GetListOfSpecialities();

    #endregion

    #region Doctor Panel Side 

    //Get Docto Selected Specialities By User Id
    Task<List<DoctorSelectedSpeciality>?> GetDoctoSelectedSpecialitiesByUserId(ulong userid);

    #endregion

    #region Site Side 

    //List Of Specialists Site Side 
    Task<List<ListOfSpecialistsSiteSideViewModel>> ListOfSpecialistsSiteSide(FilterFamilyDoctorUserPanelSideViewModel filter);

    //List Of Super Specialists 
    Task<List<ListOfSpecialistsSiteSideViewModel>> ListOfSuperSpecialists();

    #endregion
}
