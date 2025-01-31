﻿using DoctorFAM.Data.DbContext;
using DoctorFAM.Data.Migrations;
using DoctorFAM.Domain.Entities.Account;
using DoctorFAM.Domain.Entities.Common;
using DoctorFAM.Domain.Entities.PeriodicTest;
using DoctorFAM.Domain.Entities.PriodicExamination;
using DoctorFAM.Domain.Interfaces.EFCore;
using DoctorFAM.Domain.ViewModels.BackgroundTasks.PriodicTest;
using DoctorFAM.Domain.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DoctorFAM.Data.Repository
{
    public class PeriodicTestRepository : IPeriodicTestRepository
    {
        #region Ctor

        private readonly DoctorFAMDbContext _context;

        public PeriodicTestRepository(DoctorFAMDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Site Side 

        //Create Periodic Test Admin Side
        public async Task CreatePeriodicTestAdminSide(PeriodicTest test)
        {
            await _context.PeriodicTests.AddAsync(test);
            await _context.SaveChangesAsync();
        }

        //Get Periodic Test By Id 
        public async Task<PeriodicTest?> GetPeriodicTestById(ulong id)
        {
            return await _context.PeriodicTests.FirstOrDefaultAsync(p => !p.IsDelete && p.Id == id);
        }

        //Update Periodic Test Admin Side 
        public async Task UpdatePeriodicTestAdminSide(PeriodicTest test)
        {
            _context.PeriodicTests.Update(test);
            await _context.SaveChangesAsync();
        }

        //Get List Of Periodic Test 
        public async Task<List<PeriodicTest>?> GetListOfPeriodicTest()
        {
            return await _context.PeriodicTests.Where(p => !p.IsDelete).ToListAsync();
        }

        //Get List Of Diabet Part Of Periodic Test
        public async Task<List<SelectListViewModel>> GetListOfDiabetPartOfPeriodicTest()
        {

            return await _context.PeriodicTests.Where(p => !p.IsDelete &&
                                        p.PeriodicTestType == Domain.Enums.PeriodicTestType.PeriodicTestType.Diabet
                                        ||
                                        p.PeriodicTestType == Domain.Enums.PeriodicTestType.PeriodicTestType.General
                                        )
               .Select(s => new SelectListViewModel
               {
                   Id = s.Id,
                   Title = s.Name
               }).ToListAsync();
        }

        //Get List Of Blood Pressure Part Of Periodic Test
        public async Task<List<SelectListViewModel>> GetListOfBloodPressurePartOfPeriodicTest()
        {

            return await _context.PeriodicTests.Where(p => !p.IsDelete &&
                                        p.PeriodicTestType == Domain.Enums.PeriodicTestType.PeriodicTestType.BloodPressure
                                        ||
                                        p.PeriodicTestType == Domain.Enums.PeriodicTestType.PeriodicTestType.General
                                        )
               .Select(s => new SelectListViewModel
               {
                   Id = s.Id,
                   Title = s.Name
               }).ToListAsync();
        }

        //Add User Periodic Test Drom User
        public async Task AddUserPeriodicTestDromUser(UserPeriodicTest entity)
        {
            await _context.UserPeriodicTests.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        //Get List OF User Periodic Test By UserId
        public async Task<List<UserPeriodicTest>?> GetListOFUserPeriodicTestByUserId(ulong userId)
        {
            return await _context.UserPeriodicTests.Include(p => p.PeriodicTest)
                                                        .Where(p => !p.IsDelete && p.UserId == userId)
                                                                .ToListAsync();
        }

        //Get User Periodic Test By User Id And Periodic Id
        public async Task<UserPeriodicTest?> GetUserPeriodicTestByUserIdAndPeriodicId(ulong periodicId, ulong userId)
        {
            return await _context.UserPeriodicTests.FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == userId && p.PeriodicTestId == periodicId);
        }

        //Update User Periodic Test
        public async Task UpdateUserPeriodicTest(UserPeriodicTest test)
        {
            _context.UserPeriodicTests.Update(test);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region User Panel

        //Check That Current User Has Any Priodic Test After Today
        public async Task<List<UserPeriodicTest>?> CheckThatCurrentUserHasAnyPriodicTestAfterToday(ulong userId)
        {
            return await _context.UserPeriodicTests.Include(p => p.PeriodicTest)
                                                    .Where(p => !p.IsDelete && p.UserId == userId && (
                                                    (p.SystemOrderForNextTest >= DateTime.Now)
                                                    )).ToListAsync();
        }

        #endregion

        #region Background Task

        //Get List Of User Periodic test For Send SMS One Day Before
        public async Task<List<SendSMSForPriodicTestViewModel>> GetListOfUserPeriodictestForSendSMSOneDayBefore()
        {
            return await _context.UserPeriodicTests.Where(p => !p.IsDelete && p.DoctorOrderForNextTest.HasValue
                                                          && p.DoctorOrderForNextTest.Value.Year == DateTime.Now.Year
                                                          && p.DoctorOrderForNextTest.Value.DayOfYear == DateTime.Now.AddDays(1).DayOfYear)
                                                          .Select(p => new SendSMSForPriodicTestViewModel()
                                                          {
                                                              UserSelectedPriodicTestId = p.Id,
                                                              PeriodicTestType = _context.PeriodicTests.Where(s => !s.IsDelete && s.Id == p.PeriodicTestId).Select(s => s.Name).FirstOrDefault(),
                                                              Mobile = _context.Users.Where(s => !s.IsDelete && s.Id == p.UserId).Select(s => s.Mobile).FirstOrDefault()
                                                          }).ToListAsync();
        }

        //Get User Selected Priodic Test By Id
        public async Task<UserPeriodicTest?> GetUserPriodicTestById(ulong id)
        {
            return await _context.UserPeriodicTests.FirstOrDefaultAsync(p => !p.IsDelete && p.Id == id);
        }

        //Update User Priodic Test Without Save Changes
        public void UpdateUserPriodicTestWithoutSaveChanges(UserPeriodicTest model)
        {
            _context.UserPeriodicTests.Update(model);
        }

        //Save Chamges 
        public async Task Savechanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
