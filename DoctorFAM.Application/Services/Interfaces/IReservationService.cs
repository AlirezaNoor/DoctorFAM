﻿#region Usings

using DoctorFAM.Application.Services.Implementation;
using DoctorFAM.Domain.Entities.Account;
using DoctorFAM.Domain.Entities.DoctorReservation;
using DoctorFAM.Domain.Entities.Doctors;
using DoctorFAM.Domain.Enums.DoctorReservation;
using DoctorFAM.Domain.ViewModels.Admin.Reservation;
using DoctorFAM.Domain.ViewModels.Common;
using DoctorFAM.Domain.ViewModels.DoctorPanel.Appointment;
using DoctorFAM.Domain.ViewModels.Site.Reservation;
using DoctorFAM.Domain.ViewModels.Supporter.Reservation;
using DoctorFAM.Domain.ViewModels.UserPanel.Reservation;
using Microsoft.EntityFrameworkCore;


#endregion

namespace DoctorFAM.Application.Services.Interfaces;

public interface IReservationService
{
    #region Doctor Panel

    Task SendSMSForReminderToReservation();

    //List Of Appointments Received
    Task<ListOfAppointmentsReceivedJoinDoctorSideDTO?> ListOfAppointmentsReceived(ListOfAppointmentsReceivedJoinDoctorSideDTO filter, ulong userId);

    //List Of People Who Have Visited
    Task<ListOfPeopleWhoHaveVisitedDoctorSideDTO?> ListOfPeopleWhoHaveVisited(ListOfPeopleWhoHaveVisitedDoctorSideDTO filter, ulong userId);

    //List Of Doctor Reservation Date After Date Time Now In Dentist Panel
    Task<List<DoctorReservationDate>> ListOfDoctorReservationDateAfterDateTimeNowInDentistPanel(ulong userId);

    //List Of Doctor Reservation Date After Date Time Now
    Task<List<DoctorReservationDate>> ListOfDoctorReservationDateAfterDateTimeNow(ulong userId);

    //Add Reservation Date Time With Coputer   
    Task<bool> AddReservationDateTimeWithCoputer(AddReservationDateTimeWithComputerViewModel model, ulong userId);

    //Add Reservation Date Time With Coputer From Dentist Panel  
    Task<bool> AddReservationDateTimeWithCoputerFromDentistPanel(AddReservationDateTimeWithComputerViewModel model, ulong userId);

    //Fill Add Reservation Date Time With Computer View Model
    Task<AddReservationDateTimeWithComputerViewModel?> FillAddReservationDateTimeWithComputerViewModel(ulong reservationDateId, ulong doctorId);

    //Add Cancel Reservation Request 
    Task<bool> CreateCancelReservationRequestFromDoctorPanel(CancelReservationRequestViewModel model, ulong userId);

    //Get List Of Reservation Dete Time By Reservation Date Id For Select List  
    Task<List<SelectListViewModel>> GetReservationDateTimeByReservationDateIdSelectList(ulong reservationDateId, ulong userId);

    //Get List Of Reservation Dete Time By Reservation Date Id For Select List From Dentist Panel
    Task<List<SelectListViewModel>> GetReservationDateTimeByReservationDateIdSelectListFromDentistPanel(ulong reservationDateId, ulong userId);

    //Get Doctor Reservation Date By Date 
    Task<DoctorReservationDate?> GetDoctorReservationDateByDate(DateTime date, ulong userId);

    //Get Future Doctor Reservation For Cancel Reservation Request 
    Task<List<SelectListViewModel>> GetReservationsForAddCancelRequest(ulong userId);

    Task<FilterAppointmentViewModel> FilterDoctorReservationDateSide(FilterAppointmentViewModel filter);

    Task<List<DoctorReservationDate>?> FilterDoctorReservationDateSideWithoutPaging(FilterAppointmentViewModelWithoutPaging filter);

    //This Is Filter For Reservation Date From Today By Dentist Panel
    Task<FilterAppointmentViewModel?> FilterDoctorReservationDateSideByDentistPanel(FilterAppointmentViewModel filter);

    //Add Reservation Date 
    Task<bool> AddReservationDate(AddReservationDateViewModel model, ulong userId);

    //Add Reservation Date From Dentist Panel
    Task<bool> AddReservationDateFromDentistPanel(AddReservationDateViewModel model, ulong organizationOwnerId);

    Task<FilterAppointmentViewModel> FiltrDoctorReservationDateHistory(FilterAppointmentViewModel filter);

    //This Is History Of All Records That In Reservation Date By User Id ByDentistPanel 
    Task<FilterAppointmentViewModel?> FiltrDoctorReservationDateHistoryByDentistPanel(FilterAppointmentViewModel filter);

    Task<List<DoctorReservationDateTime>?> GetListOfReservationDateTimesByReservationDateId(ulong reservationDateId);

    Task<FilterReservationDateTimeDoctorPAnel> FilterReservationDateTimeDoctorSide(FilterReservationDateTimeDoctorPAnel filter);

    //Filter Reservation Date Time Dentist Side
    Task<FilterReservationDateTimeDoctorPAnel?> FilterReservationDateTimeDentistSide(FilterReservationDateTimeDoctorPAnel filter);

    Task<DoctorReservationDate?> GetReservationDateById(ulong reservationDateId);

    Task<bool> DeleteReservationDate(ulong reservationDateId, ulong userId);

    //Delete Reservation Date From Dentist Panel
    Task<bool> DeleteReservationDateFromDentistPanel(ulong reservationDateId, ulong userId);

    Task<AddReservationDateTimeViewModel?> FillAddReservationDateTime(ulong reservationDateId, ulong userId);

    //Fill Add Reservation Date Time From Dentist Panel
    Task<AddReservationDateTimeViewModel?> FillAddReservationDateTimeFromDentistPanel(ulong reservationDateId, ulong userId);

    //Fill Add Between Patient Time Doctor Side View Model
    Task<AddBetweenPatientTimeDoctorSideViewModel?> FillAddBetweenPatientTimeDoctorSideViewModel(ulong reservationDateId, ulong userId);

    Task<bool> AddReservationDateTimeDoctorPanel(AddReservationDateTimeViewModel model, ulong userId);

    //Add Reservation Date Time Dentist Panel
    Task<bool> AddReservationDateTimeDentistPanel(AddReservationDateTimeViewModel model, ulong userId);

    Task<bool> DeleteReservationDateTime(ulong reservationDateTimeId, ulong userId);

    //Delete Reservation Date Time From Dentist Panel
    Task<bool> DeleteReservationDateTimeFromDentistPanel(ulong reservationDateTimeId, ulong userId);

    Task<DoctorReservationDateTime?> GetDoctorReservationDateTimeById(ulong reservationDateTimeId);

    Task<ShowPatientDetailViewModel?> ShowPatientDetailViewModel(ulong reservationDateTimeId, ulong userId);

    //Show Patient Detail ViewModel From Dentist Panel
    Task<ShowPatientDetailViewModel?> ShowPatientDetailViewModelFromDentistPanel(ulong reservationDateTimeId, ulong userId);

    Task<bool> CloseReservation(ulong reservationTimeId);

    //Get The Owner Of Comment For Log For Waiting For Payment Reservation Request
    Task<User?> GetTheOwnerOfCommentForLogForWaitingForPaymentReservationRequest(ulong id);

    //Fill List Of Comments For Waiting For Payment Reservation Request Supporter Side DTO
    Task<List<ListOfCommentsForWaitingForPaymentReservationRequestSupporterSideDTO>?> FillListOfCommentsForWaitingForPaymentReservationRequestSupporterSideDTO(ulong id);

    //Check Doctor Reservation Date Time Validation For Add Doctor Personal Patient (Doctor Booking)
    Task<DoctorPersonalBookingViewModel?> FillDoctorPersonalBooking(ulong reservationDateTimeId, ulong userId);

    //Check Doctor Reservation Date Time Validation For Add Doctor Personal Patient (Doctor Booking) From Dentist Panel
    Task<DoctorPersonalBookingViewModel?> FillDoctorPersonalBookingFromDentistPanel(ulong reservationDateTimeId, ulong userId);

    //Get Doctor Reservation Booking By Doctor Reservation Date Time 
    Task<DoctorPersonalBooking?> GetDoctorReservationBookingByDoctorReservationDateTime(ulong doctorReservationDateTimeId);

    //Get Doctor Reservation Date Time By Include Relation With Doctor Booking
    Task<DoctorReservationDateTime?> GetDoctorReservationDateTimeByIncludeRelationWithDoctorBooking(ulong reservationDateTimeId);

    //Add Patient To Doctor Booking 
    Task<bool> AddPatientToDoctorBooking(DoctorPersonalBookingViewModel model, ulong userId);

    //Add Between Patient Time 
    Task<bool> AddBetweenPatientTime(AddBetweenPatientTimeDoctorSideViewModel model, ulong userId);

    //Add Patient To Doctor Booking From Dentist
    Task<bool> AddPatientToDoctorBookingFromDentist(DoctorPersonalBookingViewModel model, ulong userId);

    //Check That Is Doctor Reservation Is Doctor Personal Booking 
    Task<bool> CheckThatIsDoctorReservationIsDoctorPersonalBooking(ulong reservationId, ulong userId);

    //Check That Is Doctor Reservation Is Doctor Personal Booking From Dentist Panel
    Task<bool> CheckThatIsDoctorReservationIsDoctorPersonalBookingFromDentistPanel(ulong reservationId, ulong userId);

    #endregion

    #region User Panel

    Task<FilterReservationViewModel?> FilterReservationUserPanelViewModel(FilterReservationViewModel filter);

    Task<ShowReservationDetailUserSideViewModel?> FillShowReservationUserSideViewModel(ulong reservationId, ulong userId);

    Task<FilterReservationViewModel?> FilterReservationUserPanelViewComponent(FilterReservationViewModel filter);

    //This Is Filter For Reservation Date From Today 
    Task<FilterDoctorFamilyReservationDateViewModel?> FilterFamilyDoctorReservationDateFromUserPanel(FilterDoctorFamilyReservationDateViewModel filter);

    //Filter Family Doctor Reservation DateTime In UserPanel ViewModel
    Task<FilterFamilyDoctorReservationDateTimeUserPanelViewModel?> FilterFamilyDoctorReservationDateTimeUserPanel(FilterFamilyDoctorReservationDateTimeUserPanelViewModel filter);

    #endregion

    #region Admin Panel 

    Task<FilterReservationAdminSideViewModel?> FilterReservationAdminPanelViewModel(FilterReservationAdminSideViewModel filter);

    Task<ShowReservationDetailAdminSideViewModel?> FillShowReservationDetailAdminSideViewModel(ulong reservationId);

    Task<FilterClosedReservationAdminViewModel?> FilterClosedReservationAdminPanelViewModel(FilterClosedReservationAdminViewModel filter);

    //List Of Request For Cancelation Reservation
    Task<FilterCancelReservationRequestsViewModel?> FilterCancelReservationRequestsViewModel(FilterCancelReservationRequestsViewModel filter);

    //List Of Request For Cancelation Reservatio Date Time 
    Task<FilterCancelationRequestReservationDateTimeViewModel?> FilterCancelationRequestReservationDateTime(FilterCancelationRequestReservationDateTimeViewModel filter);

    //List Of Doctor Personal Booking
    Task<List<DoctorPersonalBooking>> ListOfDoctorPersonalBooking();

    #endregion

    #region Supporter Panel 

    //Fill ListOfSelectedReservationsSupporterSideDTO
    Task<List<ListOfSelectedReservationsSupporterSideDTO>?> FillListOfSelectedReservationsSupporterSideDTO();

    Task<FilterReservationSupporterSideViewModel?> FilterReservationSupporterPanelViewModel(FilterReservationSupporterSideViewModel filter);

    Task<ShowReservationDetailSupporterSideViewModel?> FillShowReservationDetailSupporterSideViewModel(ulong reservationId);

    Task<FilterWaitingForReservationRequestsSupporterSideViewModel?> FilterListOfWaitingForPaymentRequests(FilterWaitingForReservationRequestsSupporterSideViewModel filter);

    Task<ShowReservationDetailSupporterSideViewModel?> FillShowWaitingForPaymentReservationRequestDetailSupporterSideViewModel(ulong reservationId, ulong patientUserId);

    //Get Log For Waiting for Reservation Request By Id 
    Task<LogForDoctorReservationDateTimeWaitingForPayment?> GetLogForWaitingforReservationRequestById(ulong id);

    //Seen Log For Waiting For Payment Reservation Requests
    Task<bool> SeenLogForWaitingForPaymentReservationRequests(ulong requestId, ulong userId);

    //Add Comment For Waiting For Payment Reservation Request 
    Task AddCommentForWaitingForPaymentReservationRequest(ulong requestId, ulong userId, string comment);

    #endregion

    #region Site Side 

    //Show Invoice After Payment For Reservation
    Task<ReservationFactorSiteSideViewModel?> ShowInvoiceAfterPaymentForReservation(ulong reservationDateTimeId);

    //Is Exist Any Waiting For Payment Reservation Request By User Id
    Task<ulong?> IsExistAnyWaitingForPaymentReservationRequestByUserId(ulong userId);

    //Get And Delete Another Patient 
    Task GetAndDeleteAnotherPatient(ulong reservationDateTimeId, ulong userId);

    Task<ReservationFactorSiteSideViewModel?> ShowInvoiceBeforeRedirectToBankProtable(ulong reservationDateTimeId, ulong userId);

    //Update Log For Reservation Date Times In Waiting For Payment State
    Task<bool> RemoveLogForReservationDateTimesInWaitingForPaymentState(ulong doctorReservationDateTimeId, ulong userId);

    //Get Reservation Log For Waiting Payment Admind Side DTO 
    Task<ReservationLogForWaitingPaymentAdmindSideDTO?> GetReservationLogForWaitingPaymentAdmindSideDTO(ulong id);

    //Log For Reservation Date Times In Waiting For Payment State
    Task<bool> LogForReservationDateTimesInWaitingForPaymentState(ulong doctorReservationDateTimeId, ulong userId);

    //Get Doctor Reservation Alert By Doctor User Id
    Task<string?> GetDoctorReservationAlertByDoctorUserId(ulong userId);

    //Get List Of Reservation Request That Pass A Day For Pay Reservation Tariff
    Task GetListOfReservationRequestThatPassADayForPayReservationTariff();

    //Get List Of Doctor Reservation Date And Doctor Reservation Date Time For Show Site Side 
    Task<List<ListOfReservationDateAndReservationDateTimeViewModel>?> GetListOfDoctorReservationDateAndDoctorReservationDateTimeForShowSiteSide(ulong doctorUserId);

    //List Of Future Doctor Days For Reservation 
    Task<List<DoctorReservationDate>> ListOfFutureDaysOfDoctorReservation(ulong doctorUserId);

    Task<bool> ChargeUserWallet(ulong userId, int price);

    Task<bool> PayReservationTariff(ulong userId, int price , ulong? requestId);

    Task<bool> ChargeUserWalletForZeroReservationPrice(ulong userId, int price, ulong? requestId);

    //Get Reservation Date By Reservation Date And User Id
    Task<DoctorReservationDate?> GetDoctorReservationDateByReservationDateAndUserId(DateTime reservationDate, ulong userId);

    //Get Reservation Date By Reservation Date And User Id
    Task<DoctorReservationDate?> GetDoctorReservationDateByReservationDateAndUserId(string reservationDate, ulong userId);

    //Get Reservation Date Time By Reservation Date And User Id
    Task<List<DoctorReservationDateTime>?> GetDoctorReservationDateByReservationDateTimeAndUserId(string loggedreservationDate, ulong userId);

    //Get Reservation Date Time To User Patient
    Task<bool> GetReservationDateTimeToUserPatient(ChooseTypeOfReservationViewModel model, ulong patientId);

    //Cancel Payment From User And Make Reservation Time Free 
    Task<bool> CancelPaymentFromUserAndMakeReservationTimeFree(ulong reservationDateId , ulong userId);

    //Cancel Payment From Admin And Make Reservation Time Free 
    Task<bool> CancelPaymentFromAdminAndMakeReservationTimeFree(ulong reservationDateId);

    //Reserve Doctor Reservation Date Time After Success Payment
    Task ReserveDoctorReservationDateTimeAfterSuccessPayment(ulong reservationDateTimeId);

    //Pay Doctor Reservation Payed Share Percentage
    Task<bool> PayDoctorReservationPayedSharePercentage(ulong doctorUserId, int price, ulong requestId, bool isUserInDoctorPopulationCovered, DoctorReservationType doctorReservationType);

    //Fill Reservation Factor Site Side View Model
    Task<ReservationFactorSiteSideViewModel?> FillReservationFactorSiteSideViewModel(ReservationFactorSiteSideViewModel model , ulong workAddressId , ulong PatientUserId);

    //Fill Reservation Factor User Side View Model
    Task<ReservationFactorUserSideViewModel?> FillReservationFactorUserSideViewModel(ulong reservationId, ulong userId);

    //Get Doctor Reservation Date Time Doctor Selected Reservation Type
    Task<DoctorReservationType> GetDoctorReservationDateTimeDoctorSelectedReservationType(ulong doctorReservationDateTimeId);

    //Get Patient User Informations For Get Reservation Time From Doctors
    Task<UserInfoForGetReservation?> GetPatientUserInformationsForGetReservationTimeFromDoctors(ulong userId);

    #endregion
}
