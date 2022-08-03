﻿using DoctorFAM.Application.Extensions;
using DoctorFAM.Application.Services.Interfaces;
using DoctorFAM.Domain.ViewModels.Pharmacy.HomePharmacy;
using Microsoft.AspNetCore.Mvc;

namespace DoctorFAM.Web.Areas.Pharmacy.Controllers
{
    public class HomePharmacyController : PharmacyBaseController
    {
        #region Ctor

        private readonly IPharmacyService _pharmacyService;

        public HomePharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        #endregion

        #region List Of Pharmacy Requests 

        public async Task<IActionResult> FilterHomePharamcy(FilterListOfHomePharmacyRequestViewModel filter)
        {
            filter.PharmacyId = User.GetUserId();
            return View(await _pharmacyService.FilterListOfHomePharmacyRequestViewModel(filter));
        }

        #endregion
    }
}
