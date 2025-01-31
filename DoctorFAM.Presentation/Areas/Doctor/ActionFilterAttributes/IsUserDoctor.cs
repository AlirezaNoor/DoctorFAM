﻿using DoctorFAM.Application.Extensions;
using DoctorFAM.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DoctorFAM.Web.Areas.Doctor.ActionFilterAttributes
{
    public class IsUserDoctor : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var service = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService))!;

            base.OnActionExecuting(context);

            var hasUserAnyRole = service.IsUserDoctor(context.HttpContext.User.GetUserId()).Result;

            if (!hasUserAnyRole)
            {
                context.HttpContext.Response.Redirect("/Doctor/Home/Index?employeeHasNotPermission=true");
            }
        }
    }
}
