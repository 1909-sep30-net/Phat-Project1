using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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

        // GET: Home Page
        public ActionResult Index()
        {
            return View();
        }
        // POST: Home Page
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                else if (choice == 2)
                {
                    return RedirectToAction(nameof(SearchCustomerByName));
                }
                else if (choice == 3)
                {
                    return RedirectToAction(nameof(SeachOrdersByStore));
                }
                else if (choice == 4)
                {
                    return RedirectToAction(nameof(UpdateStore));
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Invalid Input");
                ModelState.AddModelError("userChoice", ex.Message);
                return View();
            }
        }


        // GET: Login Page
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Login
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
                Log.Information("Manager Not Found");
                ModelState.AddModelError("Pass", "Manager Not Found");
                return View();
            }
        }

        // GET: Manager/Details/
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
                Log.Information("Incorrect Manager Password ");
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

        // GET: Manager/SearchCustomerByName
        public ActionResult SearchCustomerByName()
        {
            return View();
        }

        // POST: Manager/SearchCustomerByName
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
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return View();
            }
        }
        //GET: Manager/CustomerInfoDisplay
        public async Task<ActionResult> CustomerInfoDisplay()
        {
            string seachByName = null;
            if (TempData["CustomerName"] != null)
            {
                seachByName = (string)TempData["CustomerName"];
            }
            var customerInfo = await _repository.GetCustomerInformationByName(seachByName);
            List<Models.CustomerListDisplay> cut = new List<Models.CustomerListDisplay>();
            if (customerInfo.Count != 0)
            {
                foreach (var c in customerInfo)
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
        //GET: Manager/CustomerInfoDisplay
        public ActionResult SeachOrdersByStore()
        {
            return View();
        }

        // POST: Manager/SeachOrdersByStore
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
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return View();
            }
        }

        //GET: Manager/OrdersInfoDisplay
        public async Task<ActionResult> OrdersInfoDisplay()
        {
            try
            {
                int searchByStore = 0;
                if (TempData["StoreId"] != null)
                {
                    searchByStore = (int)TempData["StoreId"];
                }

                if (searchByStore != 1 && searchByStore != 5)
                {
                    throw new NullReferenceException();
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
            catch (NullReferenceException)
            {
                Log.Information("Invalid Store Id");
                ModelState.AddModelError("StoreId", "Invalid Location ID");
                return View();
            }
        }
        //GET: Manager/UpdateStore
        public ActionResult UpdateStore()
        {
            return View();
        }
        //POST: Manager/OrdersInfoDisplay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateStore(Models.UpdateStore st)
        {
            try
            {
                int sto = st.StorePicked;
                TempData["StorePicked"] = sto;
                var storeInfo = await _repository.GetStoreInformation(sto);

                return RedirectToAction(nameof(UpdatedStoreDisplay));

            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return View();
            }
        }
        //GET: Manager/UpdatedStoreDisplay
        public ActionResult UpdatedStoreDisplay()
        {
            return View();
        }
        //POST: Manager/UpdatedStoreDisplay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatedStoreDisplay(Models.UpdateStoreDisPlay viewModel)
        {
            try
            {
                int sto = 0;
                if (TempData["StorePicked"] != null)
                {
                    sto = (int)TempData["StorePicked"];
                }
                var storeInfo = await _repository.GetStoreInformation(sto);


                Logic.Store updatedStore = new Logic.Store()
                {
                    address = new Address
                    {
                        city = storeInfo.address.city,
                        street = storeInfo.address.street,
                        state = storeInfo.address.state,
                        zip = storeInfo.address.zip,
                    },
                    storeInventory = new Inventory
                    {
                        items = new Product
                        {
                            NumberofAriel = viewModel.Ariel,
                            NumberofDownie = viewModel.Downie,
                            NumberofSuavitel = viewModel.Suavitel
                        }
                    }
            };
                await _repository.UpdateStore(sto, updatedStore);
                return RedirectToAction("Thankyou");
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return View();
            }
        }

        public ActionResult Thankyou()
        {
            return View();
        }
    }
}