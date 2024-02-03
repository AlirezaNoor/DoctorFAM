﻿using DoctorFAM.Application.Common.IUnitOfWork;
using DoctorFAM.Application.Extensions;
using DoctorFAM.Application.Security;
using DoctorFAM.Application.Services.Interfaces;
using DoctorFAM.Domain.Entities.Story;
using DoctorFAM.Domain.Interfaces;
using DoctorFAM.Domain.Interfaces.EFCore.Story;

namespace DoctorFAM.Application.CQRS.SiteSide.FocalPoint.Commands;

public record AddDoctorStoryCommandHandler : IRequestHandler<AddDoctorStoryCommand, AddDoctorStoryResultDTO>
{
    #region Ctor

    private readonly IStoryCommandRepository _storyCommandRepository;
    private readonly IStoryQueryRepository _storyQueryRepository;
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public AddDoctorStoryCommandHandler(IStoryCommandRepository storyCommandRepository,
                                        IStoryQueryRepository storyQueryRepository,
                                        IDoctorsRepository doctorsRepository , 
                                        IUnitOfWork unitOfWork ,
                                        IUserService userService)
    {
        _storyCommandRepository = storyCommandRepository;
        _storyQueryRepository = storyQueryRepository;
        _doctorsRepository = doctorsRepository;
        _unitOfWork = unitOfWork;
        _userService  = userService;
    }

    #endregion

    public async Task<AddDoctorStoryResultDTO> Handle(AddDoctorStoryCommand request, CancellationToken cancellationToken)
    {
        #region Initial Return Model 

        AddDoctorStoryResultDTO returnModel = new AddDoctorStoryResultDTO();

        #endregion

        #region Get User By User Id 

        var user = await _userService.GetUserByIdWithAsNoTracking(request.DoctorUserId);
        if (user == null) return new AddDoctorStoryResultDTO() { Result = false };

        #endregion

        #region Get Doctor By User Id

        var doctor = await _doctorsRepository.GetDoctorByUserId(request.DoctorUserId);
        if (doctor == null) return new AddDoctorStoryResultDTO() { Result = false };

        #endregion

        #region Create Story

        Story story = new()
        {
            UserId = request.DoctorUserId,
            Description = request.Description.SanitizeText()
        };

        if (request.StoryFile != null)
        {
            //Upload File To The Server
            var fileName = await request.StoryFile.SaveFile("wwwroot/Content/images/StoryFile/");

            story.File = fileName;
        }

        await _storyCommandRepository.AddAsync(story , cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        #endregion

        return new AddDoctorStoryResultDTO() { Result = true , Username = user.Username};
    }
}