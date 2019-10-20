using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.App.Models
{
    public class CustomerIndexViewModel
    {
        public Customer CurrentCustomer { get; set; }
      
        public bool LoggedIn { get; set; }
        public CustomerIndexViewModel()
        {
            LoggedIn = false;
        }
        public CustomerIndexViewModel(CustomerIndexViewModel other)
        {
            CurrentCustomer = other.CurrentCustomer;
            LoggedIn = other.LoggedIn;
        }
    }
}
