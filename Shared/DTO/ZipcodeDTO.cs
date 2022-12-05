﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNICKERS.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace SNICKERS.Shared.DTO
{
    public class ZipcodeDTO
    {
        [StringLength(5)]
        public string Zip { get; set; } = null!;
        [StringLength(25)]
        public string? City { get; set; }
        [StringLength(2)]
        public string? State { get; set; }
        [StringLength(30)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]
        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }

    }
}
