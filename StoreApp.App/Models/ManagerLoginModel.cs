﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.App.Models
{
    public class ManagerLoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Is Required")]
        public int Pass { get; set; }
    }
}
