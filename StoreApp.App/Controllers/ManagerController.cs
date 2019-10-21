using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApp.App.Models;
using StoreApp.Logic;

namespace StoreApp.App.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IRepository _repository;

        public ManagerController(IRepository repository)
        {
            _repository = repository;
        }


        public ActionResult Index(int choice)
        {
            try
            {


                if (choice == 1)
                {
                    if (TempData["ManagerId"] != null)
                    {
                        TempData.Keep();
                    }
                    return RedirectToAction(nameof(Details));
                }
                else if(choice == 2)
                {
                    return RedirectToAction(nameof(SearchCustomerByName));
                }
                else if(choice == 3)
                {
                    return RedirectToAction(nameof(SeachOrdersByStore));
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



        public ActionResult Login()
        {
            return View();
        }

        // POST: /LogIn
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(int pass)
        {
            try
            {
                var man = _repository.GetManager(pass);

                if (man.Result == null)
                {
                    throw new Exception();
                }
                else
                {
                    TempData["Password"] = pass;
                    TempData["ManagerId"] = man.Result.managerID;

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Pass", "Manager Not Found");
                return View();
            }
        }

        // GET: Manager/Details/5
        public async Task<ActionResult> Details()
        {
            try 
            {
                int ManagerId = 0;
                if (TempData["ManagerId"] != null)
                    ManagerId = (int)TempData["ManagerId"];
                var entityManager = await _repository.GetManager(ManagerId);
                var viewModel = new ManagerViewModel
                {
                    ManagerID = entityManager.managerID,
                    FirstName = entityManager.firstName,
                    LastName = entityManager.lastName
                };

                if (!ModelState.IsValid)
                {
                    return View(nameof(Login));

                }

                return View(viewModel);

            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("ManagerID", ex.Message);

                return View();
            }

        }

        // GET: Manager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Manager/Edit/5
        public ActionResult SearchCustomerByName()
        {
            return View();
        }

        // POST: Manager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchCustomerByName(Models.SearchCustomerByName UserName)
        {
            try
            {
                string username = UserName.Name;
                var customerInfo = await _repository.GetCustomerInformationByName(username);
                TempData["CustomerName"] = username;
                return RedirectToAction(nameof(CustomerInfoDisplay));
                
            }
            catch
            {             
                return View();
            }
        }

        public async Task<ActionResult> CustomerInfoDisplay()
        {
            string seachByName=null;
            if(TempData["CustomerName"] != null)
            {
                seachByName = (string)TempData["CustomerName"];
            }
            var customerInfo = await _repository.GetCustomerInformationByName(seachByName);
            List<Models.CustomerListDisplay> cut = new List<Models.CustomerListDisplay>();
            if(customerInfo.Count != 0)
            {
               foreach(var c in customerInfo)
                {
                    var viewModel = new Models.CustomerListDisplay()
                    {
                        CustomerList = customerInfo
                    };
                    cut.Add(viewModel);
                }
            }
            return View(cut);

        }

        public ActionResult SeachOrdersByStore()
        {
            return View();
        }

        // POST: Manager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SeachOrdersByStore(Models.SearchOrdersByStore store)
        {
            try
            {
                int checkedStore = store.StoreId;
                TempData["StoreId"] = checkedStore;

                return RedirectToAction(nameof(OrdersInfoDisplay));

            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> OrdersInfoDisplay()
        {
            int searchByStore = 0;
            if (TempData["StoreId"] != null)
            {
                searchByStore = (int)TempData["StoreId"];
            }
            var orderInfo = await _repository.GetAllOrdersFromStore(searchByStore);

            List<Models.OrdersListDisplay> ord = new List<OrdersListDisplay>();
            if (orderInfo.Count != 0)
            {
                foreach (var order in orderInfo)
                {
                    var viewModel = new Models.OrdersListDisplay()
                    {
                        OrdersList = orderInfo
                    };
                    ord.Add(viewModel);
                }
            }
            return View(ord);
        }
    }
}