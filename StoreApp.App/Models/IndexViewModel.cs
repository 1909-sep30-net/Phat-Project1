using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.App.Models
{
    public class IndexViewModel
    {
        [Required]
        public int Choice { get; set; }
        public string Username { get; set; }
    }
}
