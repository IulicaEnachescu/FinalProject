using SchoolDBModel.EntityTypes;
using SchoolWebApp.Data;
using SchoolWebApp.Services;
using SchoolWebApp.Services.Interfaces;
using SchoolWebApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolWebApp.Controllers
{
    public class AdminController : BaseController
    {
        
        
            // GET: AdminArea/Users
            private readonly IUserRepository _repo;
             private readonly IUserService _service;


            public AdminController()
            {
                this._repo = new UserDataAcces();
                this._service = new UserService(_repo);

            }

            // GET: Admin
            public ActionResult Index()
            {
                var allUsers = _repo.GetAll();



                return View(allUsers);
            }

            // GET: Admin/Details/5
            public ActionResult Details(int id)
            {

                var model = this._repo.GetById(id);

                return View(model);

            }

            public ActionResult Add()
            {

                return View();

            }



            // POST: Admin/Create
            [HttpPost]
            public ActionResult Add(User user)
            {

                if (!ModelState.IsValid)
                {
                    return View("Add", user);
                }

                if (!(this._service.UserNameNotFound(user.UserName)))
                {
                return View("Add", user);
                }
                else
                { var nr = _repo.Add(user);

                return RedirectToAction("Index");
                }

            }




            // GET: Admin/Edit/5
            public ActionResult Edit(int id)
            {
                var model = _repo.GetById(id);

                return View("Edit", model);
            }

            // POST: Admin/Edit/5

            [HttpPost]
            public ActionResult Edit(int id, User user)
            {
                if (!ModelState.IsValid)
                {
                    return View("Edit", id);
                }

                var nr = _repo.Update(id, user);
                return RedirectToAction("Index");


            }

            // GET: Admin/Delete
            [HttpGet]
            public ActionResult Delete(int id)
            {
                var user = _repo.GetById(id);
                return View(user);
            }

            // POST: Admin/Delete
            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteUser(int id)
            {
                var user = _repo.GetById(id);
                _repo.Delete(user);

                return RedirectToAction("Index");


            }
        }
    }
