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
        public ActionResult Login(int pass)
        {
            try
            {
                var man = _repository.GetManager(pass);

                if (man.Result == null)
                {
                    throw new Exception("Manager Not Found");
                }
                else
                {
                    TempData["Password"] = pass;

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Username", ex.Message);
                return View();
            }
        }

        // GET: Manager/Details/5
        public async Task<ActionResult> Details(int ManagerID)
        {
            try 
            {
                var entityManager = await _repository.GetManager(ManagerID);
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Manager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manager/Delete/5
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