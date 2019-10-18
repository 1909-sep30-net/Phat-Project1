using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Logic
{
    public class Order
    {
        public Store storeLocation = new Store();
        public Address ordererAddress = new Address();
        public Customer customer = new Customer();
        public Product cartItems = new Product();
        public int orderID { get; set; }

        
        public bool CheckOrderNotNull()
        {
            if (this.storeLocation.CheckLocationNotNull() == true &&
                this.ordererAddress.IsAddressNotNull() == true &&
                this.customer.IsCustomerNotNull() == true &&
                this.cartItems.IsNumberofProductValid() == true && 
                this.orderID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckOrdererAddress()
        {
            if (this.ordererAddress.IsAddressNotNull() == true)
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
