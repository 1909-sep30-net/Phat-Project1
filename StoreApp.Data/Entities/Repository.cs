using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Entities;
using StoreApp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data
{
    public class Repository : IRepository
    {
        private readonly StoreAppContext _context;

        public Repository(StoreAppContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Logic.Customer>> GetAllCustomers()
        {
            List<Entities.Customer> entities = await _context.Customer.ToListAsync();

            return entities.Select(e => new Logic.Customer
            {
                firstName = e.FirstName,
                lastName = e.LastName,
                customerId = e.CustomerId,
                userName = e.Username,
                customerAddress = new Address
                {
                    street = e.Street,
                    city = e.City,
                    state = e.State,
                    zip = e.Zip

                }
            });
        }

        public async Task AddNewCustomerAsync(Logic.Customer cust)
        {
            var entityCustomer = new Entities.Customer
            {
                FirstName = cust.firstName,
                LastName = cust.lastName,
                Username = cust.userName,
                Street = cust.customerAddress.street,
                State = cust.customerAddress.state,
                City = cust.customerAddress.city,
                Zip = cust.customerAddress.zip
            };

            //if username is existing
            if(await _context.Customer.AnyAsync(c => c.Username == cust.userName))
            {
                throw new InvalidOperationException("Use Name Already Exists. Please Choose Another");
            }

            _context.Add(entityCustomer);
            await _context.SaveChangesAsync();
        }

       
        
    }
}
