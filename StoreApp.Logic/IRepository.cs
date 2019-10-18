using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Logic
{
    public interface IRepository
    {
        Task<IEnumerable<Logic.Customer>> GetAllCustomers();
        Task AddNewCustomerAsync(Customer customer);

    }
}
