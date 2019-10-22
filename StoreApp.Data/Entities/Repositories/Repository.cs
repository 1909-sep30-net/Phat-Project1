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

        /// <summary>
        /// Get All Customers 
        /// Expected: List of customers
        /// </summary>
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

        /// <summary>
        /// Add New Customers
        ///</summary>
        ///<param name="Logic.Customer customer"></param>

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
        /// <summary>
        /// Get All Manager Info
        ///</summary>
        ///<param name="Logic.Manager password"></param>

        public async Task<Logic.Manager> GetManager(int pass)
        {

                var entityManager= await _context.Manager.FirstAsync(m => m.ManagerId == pass);
                

                if (entityManager == null)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    var CheckedManager = new Logic.Manager()
                    {
                        managerID = entityManager.ManagerId,
                        firstName = entityManager.FirstName,
                        lastName = entityManager.LastName,
                    };
                    return CheckedManager;
            }
        }
        /// <summary>
        /// Search Customer By Name
        ///</summary>
        ///<param name="string name"></param>
        public async Task<List<Logic.Customer>> GetCustomerInformationByName(string name)
        {
            try
            {
                var entityCustomer = await _context.Customer.Where(c => c.LastName == name || c.FirstName == name).ToListAsync();
                List<Logic.Customer> CustomerList = new List<Logic.Customer>();
                for(int i = 0; i < entityCustomer.Count; i++)
                {
                    Logic.Customer customer = new Logic.Customer()
                    {
                        firstName = entityCustomer[i].FirstName,
                        lastName = entityCustomer[i].LastName,
                        customerId = entityCustomer[i].CustomerId,
                        userName = entityCustomer[i].Username,
                        customerAddress = new Address
                        {
                            street = entityCustomer[i].Street,
                            city = entityCustomer[i].City,
                            state = entityCustomer[i].State,
                            zip = entityCustomer[i].Zip

                        }
                        
                    };
                    CustomerList.Add(customer);
                }
                return CustomerList;
            }
            catch (InvalidOperationException ex)
            {

                throw new Exception("Failed to retrieve customer information for customer : " + name + "\nException thrown: " + ex.Message);
            }
          
        }
        /// <summary>
        /// Search Customer By Id
        ///</summary>
        ///<param name="int id"></param>

        public async Task<Logic.Customer> GetCustomerInformationById(int id)
        {
            try
            {
                var entityCustomer = await _context.Customer.FirstAsync(c => c.CustomerId == id);
                var customer = new Logic.Customer
                {
                    firstName = entityCustomer.FirstName,
                    lastName = entityCustomer.LastName,
                    customerId = entityCustomer.CustomerId,
                    userName = entityCustomer.Username,
                    customerAddress = new Address
                    {
                        street = entityCustomer.Street,
                        city = entityCustomer.City,
                        state = entityCustomer.State,
                        zip = entityCustomer.Zip

                    }
                };
                return customer;
            }
            catch (InvalidOperationException ex)
            {

                throw new Exception("Failed to retrieve customer information " + ex.Message);
            }

        }

        /// <summary>
        /// Search Customer By Username
        ///</summary>
        ///<param name="string name"></param>
        public async Task<Logic.Customer> GetCustomerInformationByUserName(string username)
        {
            try
            {
                var entityCustomer = await _context.Customer.FirstAsync(c => c.Username == username);
                var customer = new Logic.Customer
                {
                    firstName = entityCustomer.FirstName,
                    lastName = entityCustomer.LastName,
                    customerId = entityCustomer.CustomerId,
                    userName = entityCustomer.Username,
                    customerAddress = new Address
                    {
                        street = entityCustomer.Street,
                        city = entityCustomer.City,
                        state = entityCustomer.State,
                        zip = entityCustomer.Zip

                    }
                };
                return customer;
            }
            catch (InvalidOperationException ex)
            {

                throw new Exception("Failed to retrieve customer information " + ex.Message);
            }
        }

        /// <summary>
        /// Get Orders of A customer
        ///</summary>
        ///<param name="id id"></param>
        public async Task<List<Logic.Order>> GetAllOrdersFromCustomer(int customerID)
        {
            try
            {
                
                List<Logic.Order> OrderList = new List<Logic.Order>();
                var contextOrders = await _context.Orders.Where(o => o.CustomerId == customerID).ToListAsync();
                for (int i = 0; i < contextOrders.Count(); i++)
                {
                    if(contextOrders[i].StoreId == 1)
                    {
                        Logic.Order customerOrders = new Logic.Order();
                        customerOrders.storeLocation.address.city = "Arlington, TX";
                        customerOrders.orderID = contextOrders[i].OrderId;
                        customerOrders.cartItems.NumberofAriel = contextOrders[i].Ariel;
                        customerOrders.cartItems.NumberofDownie = contextOrders[i].Downie;
                        customerOrders.cartItems.NumberofSuavitel = contextOrders[i].Suavitel;
                        OrderList.Add(customerOrders);
                    }
                    else if(contextOrders[i].StoreId == 5)
                    {
                        Logic.Order customerOrders = new Logic.Order();
                        customerOrders.storeLocation.address.city = "Houston, TX";
                        customerOrders.orderID = contextOrders[i].OrderId;
                        customerOrders.cartItems.NumberofAriel = contextOrders[i].Ariel;
                        customerOrders.cartItems.NumberofDownie = contextOrders[i].Downie;
                        customerOrders.cartItems.NumberofSuavitel = contextOrders[i].Suavitel;
                        OrderList.Add(customerOrders);
                    }
                }
                return OrderList;
            }
            catch
            {
                throw new Exception("Failed to retrieve order information for customer number: " + customerID);
            }
        }

        /// <summary>
        /// Get Store Information
        ///</summary>
        ///<param name="int storeId"></param>
        public async Task<Logic.Store> GetStoreInformation(int StoreID)
        {
            try
            {
                var entityStore = await _context.Store.FirstAsync(s => s.StoreId == StoreID);

                Logic.Store store = new Logic.Store
                {
                    storeId = entityStore.StoreId,
                    address = new Address
                    {
                        street = entityStore.Street,
                        city = entityStore.City,
                        state = entityStore.State,
                        zip = entityStore.Zip
                    },
                    storeInventory = new Inventory
                    {
                        items = new Product
                        {
                            NumberofAriel = entityStore.Ariel,
                            NumberofDownie = entityStore.Downie,
                            NumberofSuavitel = entityStore.Suavitel
                        }
                    }
                };
                return store;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Unable to get a store connected to the manager's ID");
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong in getting the store's information: " + e.Message);
            }

        }
        /// <summary>
        /// Orders of A Store
        ///</summary>
        ///<param name="int storeId"></param>
        public async Task<List<Logic.Order>> GetAllOrdersFromStore(int storeId)
        {
            try
            {
               
                List<Logic.Order> OrderList = new List<Logic.Order>();
                var contextOrders = await _context.Orders.Where(o => o.StoreId == storeId).ToListAsync();
                

                for (int i = 0; i < contextOrders.Count(); i++)
                {
                    Logic.Order StoreOrders = new Logic.Order();
                    StoreOrders.orderID = contextOrders[i].OrderId;
                    StoreOrders.customer.customerId = contextOrders[i].CustomerId;
                    StoreOrders.cartItems.NumberofAriel = contextOrders[i].Ariel;
                    StoreOrders.cartItems.NumberofDownie = contextOrders[i].Downie;
                    StoreOrders.cartItems.NumberofSuavitel = contextOrders[i].Suavitel;
                    OrderList.Add(StoreOrders);

                }

            

                return OrderList;
            }
            catch
            {
                throw new Exception("Failed to retrieve order information for store number: " + storeId);
            }
        }

        public async Task<List<Logic.Order>> GetAllOrdersFromStoreId(int storeId)
        {
            try
            {

                List<Logic.Order> OrderList = new List<Logic.Order>();
                var contextOrders = await _context.Orders.Where(o => o.StoreId == storeId).ToListAsync();
                for (int i = 0; i < contextOrders.Count(); i++)
                {
            
                        Logic.Order storeOrders = new Logic.Order();
                        storeOrders.customer.customerId = contextOrders[i].CustomerId;
                        storeOrders.orderID = contextOrders[i].OrderId;
                        storeOrders.cartItems.NumberofAriel = contextOrders[i].Ariel;
                        storeOrders.cartItems.NumberofDownie = contextOrders[i].Downie;
                        storeOrders.cartItems.NumberofSuavitel = contextOrders[i].Suavitel;
                        OrderList.Add(storeOrders);
        
                }
                return OrderList;
            }
            catch
            {
                throw new Exception("Failed to retrieve order information for store id: " + storeId);
            }
        }
        /// <summary>
        /// Make An Order
        ///</summary>
        ///<param name="int storeId"></param>
        ///<param name="Logic.Order ord"></param>
        public async Task MakeAnOrder(int storeID, Logic.Order ord)
        {
            try
            {
                var entityStore = await _context.Store.FirstAsync(s => s.StoreId == storeID);

                if (ord.cartItems.NumberofAriel > entityStore.Ariel || ord.cartItems.NumberofDownie > entityStore.Downie || ord.cartItems.NumberofSuavitel > entityStore.Suavitel)
                {
                    throw new Exception("Not Enough Items in The Store! Please Make Another Order Again");
                }
                if (ord.cartItems.NumberofAriel < 0 || ord.cartItems.NumberofDownie < 0 || ord.cartItems.NumberofSuavitel < 0)
                {
                    throw new InvalidOperationException("Cannot Be Negative, Please Input Number of Items Again");
                }
                else
                {
                    //send data from logic to entity
                    var entityOrder = new Entities.Orders
                    {
                        CustomerId = ord.customer.customerId,
                        StoreId = ord.storeLocation.storeId,
                        Ariel = ord.cartItems.NumberofAriel,
                        Downie = ord.cartItems.NumberofDownie,
                        Suavitel = ord.cartItems.NumberofSuavitel
                    };
                    //Update inventory of a store in entity
                    entityStore.Ariel -= ord.cartItems.NumberofAriel;
                    entityStore.Downie -= ord.cartItems.NumberofDownie;
                    entityStore.Suavitel -= ord.cartItems.NumberofSuavitel;

                    _context.Add(entityOrder);
                    _context.Store.Update(entityStore);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException)
            {
                throw;
            }
        }
        /// <summary>
        ///Update Inventory of A Store
        ///</summary>
        ///<param name="int storeId"></param>
        ///<param name="Logic.Store store"></param>
        public async Task UpdateStore(int storeID, Logic.Store store)
        {
            try
            {
                //send data from logic to entity
                var entityStore = await _context.Store.FirstAsync(s => s.StoreId == storeID);

                entityStore.Street = store.address.street;
                entityStore.City = store.address.city;
                entityStore.State = store.address.state;
                entityStore.Zip = store.address.zip;

                entityStore.Ariel = store.storeInventory.items.NumberofAriel;
                entityStore.Downie = store.storeInventory.items.NumberofDownie;
                entityStore.Suavitel = store.storeInventory.items.NumberofSuavitel;

                _context.Store.Update(entityStore);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Failed to retrieve order information for store number: " + storeID);
            }
        }

        
    }
}
