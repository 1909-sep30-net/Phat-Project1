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

        // GET: Customer
        public async Task<ActionResult> Index()
        {
            IEnumerable<Logic.Customer> customers = await _repository.GetAllCustomers();

            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                return RedirectToAction(nameof(Index));


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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult Login()
        {
            var viewModel = new CustomerOrdersViewModel();
            return View(viewModel);
        }

        // POST: Customer/CustomerInfo/5
        public async Task<ActionResult> CustomerOrders(string username)
        {
            try
            {
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}