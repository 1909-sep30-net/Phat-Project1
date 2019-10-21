using StoreApp.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.App.Models
{
    public class ManagerViewModel
    {
        [Required]
        public int ManagerID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }

    public class ManagerIndexViewModel
    {
        [Required]
        public int choice { get; set; }
        
    }

    public class SearchCustomerByName
    {
        [Required]
        public string Name { get; set; }
    }

    public class CustomerListDisplay
    {
        public List<Logic.Customer> CustomerList { get; set; }
    }

    public class SearchOrdersByStore
    {
        public int StoreId { get; set; }
        
    }

    public class OrdersListDisplay
    {
        public List<Logic.Order> OrdersList { get; set; }
    }


}
