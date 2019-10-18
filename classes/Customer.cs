using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
