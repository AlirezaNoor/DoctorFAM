﻿using DoctorFAM.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Domain.Entities.OnlineVisit
{
    public sealed class OnlineVisitDoctorsReservationDate : BaseEntity
    {
        #region properties

        //Navigation Property
        public ulong DoctorUserId { get; set; }

        public DateTime OnlineVisitShiftDate { get; set; }

        public int BusinessKey { get; set; }

        #endregion
    }
}
