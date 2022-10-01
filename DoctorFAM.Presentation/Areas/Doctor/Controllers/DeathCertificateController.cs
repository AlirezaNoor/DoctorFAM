﻿using DoctorFAM.Application.Convertors;
using DoctorFAM.Application.Extensions;
using DoctorFAM.Application.Services.Implementation;
using DoctorFAM.Application.Services.Interfaces;
using DoctorFAM.Application.StaticTools;
using DoctorFAM.Domain.ViewModels.DoctorPanel.DeathCertificate;
using DoctorFAM.Domain.ViewModels.Site.Notification;
using DoctorFAM.Web.Areas.Doctor.ActionFilterAttributes;
using DoctorFAM.Web.Doctor.Controllers;
using DoctorFAM.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DoctorFAM.Web.Areas.Doctor.Controllers
{
    [IsUserDoctor]
    public class DeathCertificateController : DoctorBaseController
    {
        #region Ctor

        private readonly IHomeVisitService _homeVisitService;
        private readonly IDeathCertificateService _deathCertificateService;
        private readonly IDoctorsService _doctorsService;
        private readonly IRequestService _requestService;
        private readonly ISMSService _smsservice;
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public DeathCertificateController(IHomeVisitService homeVisitService , IDoctorsService dctorsService, IDeathCertificateService deathCertificateService
                                            , IRequestService requestService, ISMSService smsservice, IHubContext<NotificationHub> notificationHub
                                                , INotificationService notificationService, IUserService userService)
        {
            _homeVisitService = homeVisitService;
            _doctorsService = dctorsService;
            _deathCertificateService = deathCertificateService;
            _requestService = requestService;
            _smsservice = smsservice;
            _notificationService = notificationService;
            _notificationHub = notificationHub;
            _userService = userService;
        }

        #endregion

        #region List Of Death Certificate 

        public async Task<IActionResult> ListOfDeathCertificateRequests(ListOfPayedDeathCertificateRequestDoctorSideViewModel filter)
        {
            #region Validate Doctor Interest

            var doctorInterest = await _doctorsService.GetDoctorsSideBarInfo(User.GetUserId());
            if (doctorInterest.DeathCertificate != true) return NotFound();

            #endregion

            return View(await _homeVisitService.ListOfPayedDeathCertificateRequestsDoctorPanelSide(filter));
        }

        #endregion

        #region List Of Your Death Certificate 

        public async Task<IActionResult> ListOfYourDeathCertificateRequests(ListOfPayedDeathCertificateRequestDoctorSideViewModel filter)
        {
            #region Validate Doctor Interest

            var doctorInterest = await _doctorsService.GetDoctorsSideBarInfo(User.GetUserId());
            if (doctorInterest.DeathCertificate != true) return NotFound();

            #endregion

            return View(await _deathCertificateService.ListOfYourDeathCertificateRequestsDoctorPanelSide(filter , User.GetUserId()));
        }

        #endregion

        #region Death Certificate Request Detail

        [HttpGet]
        public async Task<IActionResult> DeathCertificateRequestDetail(ulong requestId)
        {
            #region Fill View Model 

            var model = await _deathCertificateService.FillDeathCertificateRequestDetailDoctorPanelViewModel(requestId);
            if (model == null) return NotFound();

            #endregion

            return View(model);
        }

        #endregion

        #region Confirm Death Certificate Request

        public async Task<IActionResult> ConfirmDeathCertificateRequest(ulong requestId)
        {
            #region Get Request By Request Id

            var request = await _requestService.GetRequestById(requestId);
            if (request == null) return NotFound();

            #endregion

            #region Confirm Death Certificate Request 

            var res = await _deathCertificateService.ConfirmDeathCertificateRequestFromDoctor(requestId, User.GetUserId());
            if (res)
            {
                #region Send SMS

                var link = $"{PathTools.SiteFarsiName}/UserPanel/DeathCertificate/RequestDetail?requestId={requestId}";

                var message = Messages.SendSMSForAcceptDeathCertificateRequestFromDoctor();
                var messagelink = Messages.SendSMSForLinkOfDeathCertificateRequestFromDoctor(link);

                await _smsservice.SendSimpleSMS(request.User.Mobile, message);
                await _smsservice.SendSimpleSMS(request.User.Mobile, messagelink);

                #endregion

                #region Send Notification In SignalR

                //Create Notification For Supporters And Admins
                var notifyResult = await _notificationService.CreateSupporterNotification(request.Id, Domain.Enums.Notification.SupporterNotificationText.AcceptDeathCertificateRequestFromDoctor, Domain.Enums.Notification.NotificationTarget.request, User.GetUserId());

                //Send Notification From Doctor 
                await _notificationService.CreateNotificationForSendAcceptHomeVisitRequest(requestId, Domain.Enums.Notification.SupporterNotificationText.AcceptDeathCertificateRequestFromDoctor, Domain.Enums.Notification.NotificationTarget.request, User.GetUserId());

                //Get Current User
                var currentUser = await _userService.GetUserById(User.GetUserId());

                if (notifyResult)
                {
                    //Get List Of Admins And Supporter To Send Notification Into Them
                    var users = await _userService.GetAdminsAndSupportersNotificationForSendNotificationInDeathCertificate();

                    //Get Doctor For Send Notification  
                    users.Add(request.UserId.ToString());

                    //Fill Send Supporter Notification ViewModel For Send Notification
                    SendSupporterNotificationViewModel viewModel = new SendSupporterNotificationViewModel()
                    {
                        CreateNotificationDate = $"{DateTime.Now.ToShamsi()} - {DateTime.Now.Hour}:{DateTime.Now.Minute}",
                        NotificationText = "تایید درخواست گواهی فوت از طرف پزشک",
                        RequestId = request.Id,
                        Username = User.Identity.Name,
                        UserImage = currentUser.Avatar
                    };

                    await _notificationHub.Clients.Users(users).SendAsync("SendSupporterNotification", viewModel);
                }

                #endregion

                TempData[SuccessMessage] = "قبول درخواست باموفقیت ثبت شده است .";
                return RedirectToAction("Index", "Home", new { area = "Doctor" });
            }

            #endregion

            TempData[ErrorMessage] = "قبول درخواست باشکست مواجه شده است .";
            return RedirectToAction("Index", "Home", new { area = "Doctor" });
        }

        #endregion
    }
}
