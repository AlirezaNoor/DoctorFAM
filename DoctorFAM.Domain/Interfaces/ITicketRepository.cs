﻿using DoctorFAM.Domain.Entities.Contact;
using DoctorFAM.Domain.ViewModels.Admin.Ticket;
using DoctorFAM.Domain.ViewModels.UserPanel.Ticket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Domain.Interfaces
{
    public interface ITicketRepository
    {
        #region General Methods 

        //Filter Tickets In Each Panels Side
        Task<FilterSiteTicketViewModel> FilterSiteTicket(FilterSiteTicketViewModel filter, ulong userId);

        //Update Ticket Message 
        Task UpdateTicketMessage(TicketMessage ticketMessage);

        //Get Message By Message Id 
        Task<TicketMessage?> GetMessageById(ulong ticketMessageId);

        //Add Ticket Method
        Task AddTicket(Ticket ticket);

        //Add Ticekt Message
        Task AddTicketMessage(TicketMessage ticketMessage);

        //Get Ticket By Online Visit Request Id
        Task<Ticket?> GetTicketByOnlineVisitRequestId(ulong requestId);

        //Update Request 
        Task UpdateRequest(Ticket ticket);

        //Get Tikcet By Tikcet Id
        Task<Ticket?> GetTicketById(ulong ticketId);

        //Get Tikcet Messages By Ticket Id
        Task<List<TicketMessage>?> GetTikcetMessagesByTicketId(ulong ticketId);

        //Update Request Without Save Changes
        Task UpdateRequestWithoutSaveChanges(Ticket ticket);

        //Save Changes
        Task SaveChanges();

        #endregion

        #region Admin Side 

        //Filter Admin side Ticketes
        Task<AdminFilterTicketViewModel> FilterAdminTicketViewModel(AdminFilterTicketViewModel filter);

        //Read Ticket By Admin
        Task ReadTicketByAdmin(Ticket ticket);

        #endregion
    }
}
