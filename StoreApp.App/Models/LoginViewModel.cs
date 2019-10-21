﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.App.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        public string Username { get; set; }
    }

}
