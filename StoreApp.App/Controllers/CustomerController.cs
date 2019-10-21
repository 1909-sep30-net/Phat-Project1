using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApp.App.Models;
using StoreApp.Data;
using StoreApp.Logic;

namespace StoreApp.App.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository _repository;

        public CustomerController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Home Page
        public ActionResult Index()
        {
            return View();
        }
        // POST: Home Page
        [HttpPost]
        public  ActionResult Index(int choice)
        {
            try
            {
      
                if (choice == 2)
                {
                    if (TempData["Username"] != null)
                    {
                        TempData.Keep();
                    }
                    return RedirectToAction(nameof(CustomerOrders));

                }
                else if (choice == 1)
                {
                    if (TempData["Username"] != null)
                    {
                        TempData.Keep();
                    }
                    return RedirectToAction(nameof(MakeAnOrder));
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("userChoice", ex.Message);
                return View();
            }
        }

        // GET: Customer/Create
        public async Task<ActionResult> Create()
        {
            return View();
            
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Models.Customer viewModel)
        {
            try
            {
               
                //Add a new customer
                var customer = new Logic.Customer
                {
                    userName = viewModel.Username,
                    firstName = viewModel.FirstName,
                    lastName = viewModel.LastName,
                    customerAddress = new Address
                    {
                        city = viewModel.customerAddress.city,
                        street = viewModel.customerAddress.street,
                        state = viewModel.customerAddress.state,
                        zip = viewModel.customerAddress.zip
                    }
                };

            
                await _repository.AddNewCustomerAsync(customer);
                return RedirectToAction(nameof(Login));


            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Username", ex.Message);

                var cust = await _repository.GetAllCustomers();
                return View(viewModel);
            }
            catch (Exception)
            {
                return View(viewModel);
            }
            
        }

   
        public ActionResult Login()
        {
            return View();
        }

        // POST: /LogIn
        [HttpPost]
        public ActionResult Login(string username)
        {
            try
            {
                var cuts = _repository.GetCustomerInformationByUserName(username);

                if (cuts.Result == null)
                {
                    throw new Exception("User Not Found");
                }
                else
                {
                    TempData["Username"] = username;

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Username", ex.Message);
                return View();
            }
        }
            // POST: Customer/CustomerOrders/5
            public async Task<ActionResult> CustomerOrders()
        {
            try
            {
                string username = null;
                if (TempData["Username"] != null)
                {
                    username = TempData["Username"].ToString();
                }

                Logic.Customer customer = await _repository.GetCustomerInformationByUserName(username);
                List<Logic.Order> orders = await _repository.GetAllOrdersFromCustomer(customer.customerId);

                var viewModel = new CustomerOrdersViewModel
                {
                    Username = customer.userName,
                    FirstName = customer.firstName,
                    LastName = customer.lastName,
                    CustomerOrders = orders
                };
                    return View(viewModel);

            }
            catch (InvalidOperationException)
            {

                return RedirectToAction(nameof(InvalidCustomer));
            }
        }
        public ActionResult InvalidCustomer(int inputCustomerID)
        {
            return View(inputCustomerID);
        }

        // GET: Customer/PlaceAnOrder/
        public ActionResult MakeAnOrder()
        {
            return View();
        }

        // POST: Customer/PlaceAnOrder/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakeAnOrder(Models.CustomerMakeAnOrder order)
        {
            try
            {
                int StoreId =order.StoreId;
                string username = null;
                if (TempData["Username"] != null)
                    username = TempData["Username"].ToString();


                if (TempData["StoreId"] != null)
                    StoreId = (int)TempData["StoreId"];

                Logic.Customer cust = await _repository.GetCustomerInformationByUserName(username);
                Logic.Store store = await _repository.GetStoreInformation(StoreId);

                Logic.Order ord = new Logic.Order()
                {
                    customer = new Logic.Customer
                    {
                        customerId = cust.customerId
                    },
                    storeLocation = new Logic.Store
                    {
                        storeId = store.storeId
                    },
                    cartItems = new Product
                    {
                        NumberofAriel = order.NumberofAriel,
                        NumberofDownie = order.NumberofDownie,
                        NumberofSuavitel = order.NumberofSuavitel,
                    }
                };

                await _repository.MakeAnOrder(StoreId, ord);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}