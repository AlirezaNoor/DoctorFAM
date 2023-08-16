﻿#region Usings

using DoctorFAM.Data.DbContext;
using DoctorFAM.Data.Migrations;
using DoctorFAM.Domain.Entities.Account;
using DoctorFAM.Domain.Entities.Tourism;
using DoctorFAM.Domain.Entities.Tourism.Token;
using DoctorFAM.Domain.Interfaces.EFCore;
using DoctorFAM.Domain.ViewModels.Tourist.Token;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Data.Repository;

#endregion

public class TouristTokenRepository : ITouristTokenRepository
{
    #region Ctor

    private readonly DoctorFAMDbContext _context;

    public TouristTokenRepository(DoctorFAMDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Tourist Panel

    //Add Tourist Passenger To The Data Base
    public async Task AddTouristPassengerToTheDataBase(TouristPassengers tourist)
    {
        await _context.TouristPassengers.AddAsync(tourist);
        await _context.SaveChangesAsync();
    }

    //List Of Waiting User For Take in Token To Them
    public async Task<List<ListOfWaitingUserForTakeinTokenToThemTouristPanelViewModel>?> ListOfWaitingUserForTakeinTokenToThem(ulong touristId)
    {
        return await _context.TouristPassengers
                             .AsNoTracking()
                             .Where(p => !p.IsDelete && p.TouristId == touristId && p.PassengerInfoState == Domain.Enums.Tourist.TouristPassengersInfoState.WaitingForCompleteInfoFromTourist)
                             .Select(p => new ListOfWaitingUserForTakeinTokenToThemTouristPanelViewModel()
                             {
                                 Id = p.Id,
                                 RequiredAmount = p.RequiredAmount,
                                 TouristPassengerInfo = _context.Users
                                                                .AsNoTracking()
                                                                .Where(s => !s.IsDelete && s.Id == p.UserId)
                                                                .Select(s => new TouristPassengerInfo()
                                                                {
                                                                    UserId = p.UserId,
                                                                    Mobile = s.Mobile,
                                                                    UserAvatar = s.Avatar,
                                                                    Username = s.Username
                                                                })
                                                                .FirstOrDefault()
                             })
                             .ToListAsync();
    }

    //Check That Is Exist Any Waiting Record With This User Infomation
    public async Task<TouristPassengers?> CheckThatIsExistAnyWaitingRecordWithThisUserInfomation(ulong userId, ulong touristId)
    {
        return await _context.TouristPassengers
                             .AsNoTracking()
                             .Where(p => !p.IsDelete && p.TouristId == touristId && p.UserId == userId
                                    && p.PassengerInfoState == Domain.Enums.Tourist.TouristPassengersInfoState.WaitingForCompleteInfoFromTourist)
                             .FirstOrDefaultAsync();
    }

    //Update Tourist Passenger Data Information 
    public async Task UpdateTouristPassengerDataInformation(TouristPassengers passengers)
    {
        _context.TouristPassengers.Update(passengers);
        await _context.SaveChangesAsync();
    }

    //Get Tourist Passenger By Id
    public async Task<TouristPassengers?> GetTouristPassengerById(ulong touristPassengerId)
    {
        return await _context.TouristPassengers
                             .AsNoTracking()
                             .FirstOrDefaultAsync(p => !p.IsDelete && p.Id == touristPassengerId);
    }

    //Count Of Waiting User For Initial Token
    public async Task<int> CountOfWaitingUserForInitialToken(ulong touristId)
    {
        return await _context.TouristPassengers
                             .AsNoTracking()
                             .CountAsync(p => !p.IsDelete && p.TouristId == touristId && p.PassengerInfoState == Domain.Enums.Tourist.TouristPassengersInfoState.WaitingForCompleteInfoFromTourist);
    }

    //Add Token To The Data Base
    public async Task AddTokenToTheDataBase(TouristToken token)
    {
        await _context.TouristTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    //Count Of Waiting Passengers With Their Required Amount
    public async Task<int> CountOfWaitingPassengersWithTheirRequiredAmount(ulong touristId)
    {
        return await _context.TouristPassengers
                             .AsNoTracking()
                             .Where(p => !p.IsDelete && p.TouristId == touristId && p.PassengerInfoState == Domain.Enums.Tourist.TouristPassengersInfoState.WaitingForCompleteInfoFromTourist)
                             .SumAsync(p => p.RequiredAmount);
    }

    //Get Last Waiting Fot Payment Token 
    public async Task<TouristToken?> GetLastWaitingFotPaymentToken(ulong touristId)
    {
        return await _context.TouristTokens
                               .AsNoTracking()
                               .FirstOrDefaultAsync(p => !p.IsDelete && p.TouristId == touristId && p.TouristTokenState == Domain.Enums.Tourist.TouristTokenState.WaitingForPayment);
    }

    #endregion
}
