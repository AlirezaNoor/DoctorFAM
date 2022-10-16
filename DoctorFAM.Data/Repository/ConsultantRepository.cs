﻿using DoctorFAM.Data.DbContext;
using DoctorFAM.Domain.Entities.Consultant;
using DoctorFAM.Domain.Entities.Doctors;
using DoctorFAM.Domain.Entities.FamilyDoctor;
using DoctorFAM.Domain.Entities.Nurse;
using DoctorFAM.Domain.Enums.Request;
using DoctorFAM.Domain.Interfaces;
using DoctorFAM.Domain.ViewModels.Admin.Consultant;
using DoctorFAM.Domain.ViewModels.Admin.Doctor;
using DoctorFAM.Domain.ViewModels.Admin.OnlineVisit;
using DoctorFAM.Domain.ViewModels.Consultant.ConsultantRequest;
using DoctorFAM.Domain.ViewModels.Consultant.ConsultantSideBar;
using DoctorFAM.Domain.ViewModels.DoctorPanel.OnlineVisit;
using DoctorFAM.Domain.ViewModels.DoctorPanel.PopulationCovered;
using DoctorFAM.Domain.ViewModels.UserPanel.FamilyDoctor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Data.Repository
{
    public class ConsultantRepository : IConsultantRepository
    {
        #region Ctor

        private readonly DoctorFAMDbContext _context;

        public ConsultantRepository(DoctorFAMDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Consultant Panel Side 

        //Get User Selected Consultant By Request Id
        public async Task<UserSelectedConsultant?> GetUserSelectedConsultantByRequestId(ulong requestId)
        {
            return await _context.UserSelectedConsultants
                            .FirstOrDefaultAsync(p => !p.IsDelete && p.Id == requestId);
        }

        //Get User Selected Consultant By Patient And Consultant Id
        public async Task<UserSelectedConsultant?> GetUserSelectedConsultantByPatientAndConsultantId(ulong patientId , ulong consultantId)
        {
            return await _context.UserSelectedConsultants
                            .FirstOrDefaultAsync(p => !p.IsDelete && p.PatientId == patientId && p.ConsultantId == consultantId
                                                    && p.ConsultantRequestState == Domain.Enums.FamilyDoctor.FamilyDoctorRequestState.Accepted);
        }

        //List Of Consultant Population Covered Users
        public async Task<ListOfConsultantPopulationCoveredViewModel> FilterListOfConsultantPopulationCoveredViewModel(ListOfConsultantPopulationCoveredViewModel filter)
        {
            #region Get organization 

            var consultantOffice = await _context.OrganizationMembers.Include(p => p.Organization)
                                .FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == filter.UserId);
            if (consultantOffice == null) return null;

            #endregion

            var query = _context.UserSelectedConsultants
                .Include(p => p.Patient)
                .ThenInclude(p => p.PopulationCovered)
                .Where(s => !s.IsDelete && s.ConsultantId == consultantOffice.Organization.OwnerId && s.ConsultantRequestState == Domain.Enums.FamilyDoctor.FamilyDoctorRequestState.WaitingForConfirm)
                .OrderByDescending(s => s.CreateDate)
                .Select(p => p.Patient)
                .AsQueryable();


            #region Filter

            if (!string.IsNullOrEmpty(filter.Mobile))
            {
                query = query.Where(s => s.Mobile != null && EF.Functions.Like(s.Mobile, $"%{filter.Mobile}%"));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                query = query.Where(s => s.Username.Contains(filter.Username));
            }

            if (!string.IsNullOrEmpty(filter.NationalId))
            {
                query = query.Where(s => s.NationalId.Contains(filter.NationalId));
            }

            #endregion

            await filter.Paging(query);

            return filter;
        }

        //List Of Your Consultant Population Covered Users
        public async Task<ListOfConsultantPopulationCoveredViewModel> FilterYourListOfConsultantPopulationCoveredViewModel(ListOfConsultantPopulationCoveredViewModel filter)
        {
            #region Get organization 

            var consultantOffice = await _context.OrganizationMembers.Include(p => p.Organization)
                                .FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == filter.UserId);
            if (consultantOffice == null) return null;

            #endregion

            var query = _context.UserSelectedConsultants
                .Include(p => p.Patient)
                .ThenInclude(p => p.PopulationCovered)
                .Where(s => !s.IsDelete && s.ConsultantId == consultantOffice.Organization.OwnerId && s.ConsultantRequestState == Domain.Enums.FamilyDoctor.FamilyDoctorRequestState.Accepted)
                .OrderByDescending(s => s.CreateDate)
                .Select(p => p.Patient)
                .AsQueryable();


            #region Filter

            if (!string.IsNullOrEmpty(filter.Mobile))
            {
                query = query.Where(s => s.Mobile != null && EF.Functions.Like(s.Mobile, $"%{filter.Mobile}%"));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                query = query.Where(s => s.Username.Contains(filter.Username));
            }

            if (!string.IsNullOrEmpty(filter.NationalId))
            {
                query = query.Where(s => s.NationalId.Contains(filter.NationalId));
            }

            #endregion

            await filter.Paging(query);

            return filter;
        }

        //Fill Consultant Side Bar Panel
        public async Task<ConsultantSideBarViewModel> GetConsultantSideBarInfo(ulong userId)
        {
            #region Get Doctor Office

            var OrganitionMember = await _context.OrganizationMembers.Include(p => p.Organization)
                                                .FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == userId && p.Organization.OrganizationType == Domain.Enums.Organization.OrganizationType.Consultant);

            #endregion

            ConsultantSideBarViewModel model = new ConsultantSideBarViewModel();

            #region Consultant State 

            //If Consultant Registers Now
            if (OrganitionMember.Organization.OrganizationInfoState == OrganizationInfoState.JustRegister) model.ConsultantInfoState = "NewUser";

            //If Consultant State Is WatingForConfirm
            if (OrganitionMember.Organization.OrganizationInfoState == OrganizationInfoState.WatingForConfirm) model.ConsultantInfoState = "WatingForConfirm";

            //If Consultant State Is Rejected
            if (OrganitionMember.Organization.OrganizationInfoState == OrganizationInfoState.Rejected) model.ConsultantInfoState = "Rejected";

            //If Consultant State Is Accepted
            if (OrganitionMember.Organization.OrganizationInfoState == OrganizationInfoState.Accepted) model.ConsultantInfoState = "Accepted";

            #endregion

            return model;
        }

        //Is Exist Any Consultant By This User Id 
        public async Task<bool> IsExistAnyConsultantByUserId(ulong userId)
        {
            return await _context.consultant.AnyAsync(p => p.UserId == userId && !p.IsDelete);
        }

        //Add Consultant To Data Base
        public async Task<ulong> AddConsultant(Consultant consultant)
        {
            await _context.consultant.AddAsync(consultant);
            await _context.SaveChangesAsync();

            return consultant.Id;
        }

        //Get Consultant By User Id
        public async Task<Consultant?> GetConsultantByUserId(ulong userId)
        {
            return await _context.consultant.Include(p => p.User).FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == userId);
        }

        //Get Consultant Information By User Id
        public async Task<ConsultantInfo?> GetConsultantInformationByUserId(ulong userId)
        {
            return await _context.ConsultantInfos.Include(p => p.Consultant).FirstOrDefaultAsync(p => p.Consultant.UserId == userId && !p.IsDelete);
        }

        //Check Is Exist Consultant Info By User ID
        public async Task<bool> IsExistAnyConsultantInfoByUserId(ulong userId)
        {
            return await _context.ConsultantInfos.Include(p => p.Consultant).AnyAsync(p => !p.IsDelete && p.Consultant.UserId == userId);
        }

        //Update Consultant Info 
        public async Task UpdateConsultantInfo(ConsultantInfo consultantInfo)
        {
            _context.ConsultantInfos.Update(consultantInfo);
            await _context.SaveChangesAsync();
        }

        //Add Consultant Info
        public async Task AddConsultantInfo(ConsultantInfo consultantInfo)
        {
            await _context.ConsultantInfos.AddAsync(consultantInfo);
            await _context.SaveChangesAsync();
        }

        //Get User Selected Consultant By Patient Id And Consultant Id With Accepted And Waiting State
        public async Task<UserSelectedConsultant?> GetUserSelectedConsultantByPatientIdAndConsultantWithAcceptedAndWaitingState(ulong userId, ulong consultantId)
        {
            return await _context.UserSelectedConsultants.FirstOrDefaultAsync(p => !p.IsDelete && p.ConsultantId == consultantId && p.PatientId == userId
                                                             && p.ConsultantRequestState != Domain.Enums.FamilyDoctor.FamilyDoctorRequestState.Decline);
        }

        #endregion

        #region Admin Side 

        //Filter Consultant Requests Admin Side 
        public async Task<FilterConsultantAdminSideViewModel> FilterConsultantAdminSideViewModel(FilterConsultantAdminSideViewModel filter)
        {
            var query = _context.UserSelectedConsultants
             .Include(p=> p.Patient)
             .Include(p=> p.Consultant)
             .Where(s => !s.IsDelete)
             .OrderByDescending(s => s.CreateDate)
             .AsQueryable();

            #region Status

            switch (filter.FilterRequestAdminSideOrder)
            {
                case FilterRequestAdminSideOrder.CreateDate_Des:
                    break;
                case FilterRequestAdminSideOrder.CreateDate_Asc:
                    query = query.OrderBy(p => p.CreateDate);
                    break;
            }

            #endregion

            #region Filter

            if (!string.IsNullOrEmpty(filter.UserMobile))
            {
                query = query.Where(s => s.Patient.Mobile != null && EF.Functions.Like(s.Patient.Mobile, $"%{filter.UserMobile}%"));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                query = query.Where(s => EF.Functions.Like(s.Patient.Username, $"%{filter.Username}%"));
            }

            if (!string.IsNullOrEmpty(filter.NationalId))
            {
                query = query.Where(s => EF.Functions.Like(s.Patient.NationalId, $"%{filter.NationalId}%"));
            }

            if (!string.IsNullOrEmpty(filter.ConsultantUserMobile))
            {
                query = query.Where(s => s.Consultant.Mobile != null && EF.Functions.Like(s.Consultant.Mobile, $"%{filter.ConsultantUserMobile}%"));
            }

            if (!string.IsNullOrEmpty(filter.ConsultantUsername))
            {
                query = query.Where(s => EF.Functions.Like(s.Consultant.Username, $"%{filter.ConsultantUsername}%"));
            }

            if (!string.IsNullOrEmpty(filter.ConsultantNatinalId))
            {
                query = query.Where(s => EF.Functions.Like(s.Consultant.NationalId, $"%{filter.ConsultantNatinalId}%"));
            }

            #endregion

            await filter.Paging(query);

            return filter;
        }

        //Filter Consultant Info Admin Side
        public async Task<ListOfConsultantInfoViewModel> FilterConsultantInfoAdminSide(ListOfConsultantInfoViewModel filter)
        {
            var query = _context.Organizations
                .Where(s => !s.IsDelete && s.OrganizationType == Domain.Enums.Organization.OrganizationType.Consultant)
                .Include(p => p.User)
                .OrderByDescending(s => s.CreateDate)
                .AsQueryable();

            #region State

            switch (filter.ConsultantState)
            {
                case ConsultantState.All:
                    break;
                case ConsultantState.Accepted:
                    query = query.Where(p => p.OrganizationInfoState == OrganizationInfoState.Accepted);
                    break;
                case ConsultantState.WaitingForConfirm:
                    query = query.Where(p => p.OrganizationInfoState == OrganizationInfoState.WatingForConfirm);
                    break;
                case ConsultantState.Rejected:
                    query = query.Where(p => p.OrganizationInfoState == OrganizationInfoState.Rejected);
                    break;
            }

            #endregion

            #region Filter

            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(s => EF.Functions.Like(s.User.Email, $"%{filter.Email}%"));
            }

            if (!string.IsNullOrEmpty(filter.Mobile))
            {
                query = query.Where(s => s.User.Mobile != null && EF.Functions.Like(s.User.Mobile, $"%{filter.Mobile}%"));
            }

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                query = query.Where(s => s.User.Username.Contains(filter.FullName));
            }

            if (!string.IsNullOrEmpty(filter.NationalCode))
            {
                query = query.Where(s => s.User.NationalId.Contains(filter.NationalCode));
            }

            #endregion

            await filter.Paging(query);

            return filter;
        }

        //Get Consultant Info By Nurse Id
        public async Task<ConsultantInfo?> GetConsultantInfoByConsultantId(ulong consultantId)
        {
            return await _context.ConsultantInfos.Include(p => p.Consultant).FirstOrDefaultAsync(p => !p.IsDelete && p.ConsultantId == consultantId);
        }

        //Get Consultant By Consultant Id
        public async Task<Consultant?> GetConsultantById(ulong consultantId)
        {
            return await _context.consultant.Include(p => p.User).FirstOrDefaultAsync(p => !p.IsDelete && p.Id == consultantId);
        }

        //Get Consultant Info By Nurse Info Id
        public async Task<ConsultantInfo?> GetConsultantInfoById(ulong consultantInfoId)
        {
            return await _context.ConsultantInfos.Include(p => p.Consultant).FirstOrDefaultAsync(p => !p.IsDelete && p.Id == consultantInfoId);
        }

        #endregion

        #region User Panel Side 

        //Get User Selected Consultant 
        public async Task<UserSelectedConsultant?> GetUserSelectedConsultantByUserId(ulong userId)
        {
            return await _context.UserSelectedConsultants
                            .FirstOrDefaultAsync(p => !p.IsDelete && p.PatientId == userId);
        }

        //Get List Of Consultant
        public async Task<List<Consultant>?> FilterConsultantUserPanelSide(FilterConsultantUserPanelSideViewModel filter)
        {
            var query = await _context.consultant
                    .Include(p => p.CounsultantInfo)
                    .Include(p => p.User)
                    .ThenInclude(p => p.WorkAddresses)
                    .ThenInclude(p => p.City)
                    .Include(p => p.User)
                    .ThenInclude(p => p.WorkAddresses)
                    .ThenInclude(p => p.State)
                    .Include(p => p.User)
                    .ThenInclude(p => p.WorkAddresses)
                    .ThenInclude(p => p.Country)
                    .Where(s => !s.IsDelete)
                    .OrderByDescending(s => s.CreateDate)
                    .ToListAsync();

            var model = new List<Consultant>();

            foreach (var item in query)
            {
                if (await _context.Organizations.FirstOrDefaultAsync(p => !p.IsDelete && p.OwnerId == item.UserId && p.OrganizationInfoState == OrganizationInfoState.Accepted) != null)
                {
                    model.Add(item);
                }
            }

            #region Filter

            if (!string.IsNullOrEmpty(filter.Username))
            {
                model = model.Where(s => s.User.Username.Contains(filter.Username)).ToList();
            }

            if (filter.CountryId.HasValue)
            {
                List<Consultant?> CountryModel = new List<Consultant?>();

                foreach (var item in model.Select(p => p.UserId))
                {
                    var address = await _context.WorkAddresses.FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == item && p.CountryId == filter.CountryId.Value);

                    if (address != null)
                    {
                        CountryModel.Add(await _context.consultant
                                                .Include(p => p.User)
                                                .Where(s => !s.IsDelete && s.UserId == address.UserId).FirstOrDefaultAsync());
                    }
                }

                model = CountryModel;
            }

            if (filter.StateId.HasValue)
            {
                List<Consultant?> StateModel = new List<Consultant?>();

                foreach (var item in query.Select(p => p.UserId))
                {
                    var address = await _context.WorkAddresses.FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == item && p.StateId == filter.StateId.Value);

                    if (address != null)
                    {
                        StateModel.Add(await _context.consultant
                                                .Include(p => p.User)
                                                .Where(s => !s.IsDelete && s.UserId == address.UserId).FirstOrDefaultAsync());
                    }
                }

                model = StateModel;
            }

            if (filter.CityId.HasValue)
            {
                List<Consultant?> CityModel = new List<Consultant?>();

                foreach (var item in query.Select(p => p.UserId))
                {
                    var address = await _context.WorkAddresses.FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == item && p.CityId == filter.CityId.Value);

                    if (address != null)
                    {
                        CityModel.Add(await _context.consultant
                                                .Include(p => p.User)
                                                .Where(s => !s.IsDelete && s.UserId == address.UserId).FirstOrDefaultAsync());
                    }
                }

                model = CityModel;
            }

            #endregion

            return model;
        }

        //Remove Consultant For This Patient 
        public async Task RemoveConsultantForThisPatient(UserSelectedConsultant consultant)
        {
            consultant.IsDelete = true;

            _context.UserSelectedConsultants.Update(consultant);
            await _context.SaveChangesAsync();
        }

        //Add Consultant For This Patient 
        public async Task AddConsultantForThisPatient(UserSelectedConsultant consultant)
        {
            await _context.UserSelectedConsultants.AddAsync(consultant);
            await _context.SaveChangesAsync();
        }

        //Update User Selected Consultant 
        public async Task UpdateUserSelectedConsultant(UserSelectedConsultant userSelectedConsultant)
        {
            _context.UserSelectedConsultants.Update(userSelectedConsultant);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}