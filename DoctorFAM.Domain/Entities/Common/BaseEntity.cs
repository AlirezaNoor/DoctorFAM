﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFAM.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public ulong Id { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public bool IsDelete { get; set; } = false;
    }
}
