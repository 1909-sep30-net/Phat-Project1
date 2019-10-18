using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Logic
{
    public class Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public Address customerAddress = new Address();
        public int customerId { get; set; }
        public string userName { get; set; }

        public bool IsCustomerNotNull()
        {
            //doesnt check for customer ID in the event that a new customer is being added
            if (customerAddress.IsAddressNotNull() == true && this.firstName != null && this.lastName != null && this.userName != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
