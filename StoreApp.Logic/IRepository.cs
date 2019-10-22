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
        Task<Manager> GetManager(int pass);
        Task<List<Logic.Customer>> GetCustomerInformationByName(string name);
        Task<Logic.Customer> GetCustomerInformationById(int id);
        Task<Logic.Customer> GetCustomerInformationByUserName(string username);

        Task MakeAnOrder(int StoreId, Order order);
        Task<List<Logic.Order>> GetAllOrdersFromCustomer(int customerID);
        Task<Logic.Store> GetStoreInformation(int StoreID);
        Task<List<Logic.Order>> GetAllOrdersFromStore(int storeId);
        Task UpdateStore(int storeID, Logic.Store store);
    }
}
