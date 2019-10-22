using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Logic
{
    public class Customer
    {
        public const int maxNameLength = 20;
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

        /// <summary>
        /// When set, validates the customer's last name and capitalizes it.
        /// </summary>
        public string LastName
        {
            get => lastName;
            set
            {
                ValidateCustomerLastName(value);
                lastName = char.ToUpper(value[0]) + value.Substring(1).ToLower();
            }
        }

        /// <summary>
        /// Validates that the customer's first name is not empty, not too long, and is alphabetical.
        /// </summary>
        /// <param name="first">First name of the customer</param>
        private void ValidateCustomerFirstName(string first)
        {
            if (first.Length == 0) throw new CustomerException("[!] First name is empty");
            else if (first.Length > maxNameLength) throw new CustomerException($"[!] First name is longer than {maxNameLength} characters");
            else if (!first.All(Char.IsLetter)) throw new CustomerException("[!] First name is not alphabetical");
        }

        /// <summary>
        /// Validates that the customer's last name is not empty, not too long, and is alphabetical.
        /// </summary>
        /// <param name="last">Last name of the customer</param>
        private void ValidateCustomerLastName(string last)
        {
            if (last.Length == 0) throw new CustomerException("[!] Last name is empty");
            else if (last.Length > maxNameLength) throw new CustomerException($"[!] Last name is longer than {maxNameLength} characters");
            else if (!last.All(Char.IsLetter)) throw new CustomerException("[!] Last name is not alphabetical");
        }
    }
}
