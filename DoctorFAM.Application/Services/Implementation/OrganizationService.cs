﻿using DoctorFAM.Application.Services.Interfaces;
using DoctorFAM.Domain.Entities.Organization;
using DoctorFAM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Application.Services.Implementation
{
    public class OrganizationService : IOrganizationService
    {
        #region Ctor

        private readonly IOrganizationRepository _organization;

        public OrganizationService(IOrganizationRepository organization)
        {
            _organization = organization;
        }

        #endregion

        #region General

        public async Task<ulong> AddOrganizationWithReturnId(Organization organization)
        {
            return await _organization.AddOrganizationWithReturnId(organization);
        }

        public async Task AddOrganizationMember(OrganizationMember member)
        {
            await _organization.AddOrganizationMember(member);
        }

        public async Task<Organization?> GetOrganizationByUserId(ulong userId)
        {
            return await _organization.GetOrganizationByUserId(userId);
        }

        public async Task UpdateOrganization(Organization organization)
        {
            await _organization.UpdateOrganization(organization);
        }

        public async Task<bool> DeleteEmployeeFromYourOrganization(ulong employeeId , ulong userId)
        {
            #region Get Organization

            var organization = await GetOrganizationByUserId(userId);
            if (organization == null) return false;

            //Owner Can Not Be Deleted
            if (organization.OwnerId == employeeId) return false;

            #endregion

            #region Delete Employee From Organization 

            var res = await _organization.DeleteEmployeeFromOrganization(employeeId , organization.Id);
            if (res == false) return false;

            #endregion

            return true;
        }

        public async Task<bool> IsExistAnyEmployeeByUserId(ulong userId)
        {
            return await _organization.IsExistAnyEmployeeByUserId(userId);
        }

        #endregion
    }
}
