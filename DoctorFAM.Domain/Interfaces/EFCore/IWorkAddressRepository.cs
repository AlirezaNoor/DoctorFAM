﻿using DoctorFAM.Domain.Entities.WorkAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Domain.Interfaces
{
    public interface IWorkAddressRepository
    {
        #region User Side

        Task<List<WorkAddress>?> GetUserWorkAddressesByUserId(ulong userId);

        Task AddWorkAddress(WorkAddress workAddress);

        Task<WorkAddress?> GetUserWorkAddressById(ulong userid);

        Task UpdateUserWorkAddress(WorkAddress workAddress);

        Task<WorkAddress?> GetLastWorkAddressByUserId(ulong userId);

        #endregion
    }
}