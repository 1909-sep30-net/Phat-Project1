using StoreApp.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.App.Models
{
    public class Customer
    {
        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }
        [Required]
        public string Username { get; set; }
        public Address customerAddress { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        

    }

    public class CustomerOrdersViewModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Order> CustomerOrders { get; set; }
    }

    public class CustomerMakeAnOrder
    {
        public int StoreId { get; set; }
        public int NumberofAriel { get; set; }
        public int NumberofDownie { get; set; }
        public int NumberofSuavitel { get; set; }
        public int CustomerId { get; set; }
        public int orderID { get; set; }

    }
}
