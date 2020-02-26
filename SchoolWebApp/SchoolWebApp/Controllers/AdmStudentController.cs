using SchoolWebApp.Data;
using SchoolWebApp.Services;
using SchoolWebApp.Services.Interfaces;
using SchoolWebApp.Services.IServices;
using SchoolWebApp.Services.Services;
using SchoolWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;

namespace SchoolWebApp.Controllers
{
    public class AdmStudentController : BaseController
    {
        private readonly IUserRepository _userRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;


        public AdmStudentController()
        {
            this._userRepo = new UserDataAcces();
            this._studentRepo = new StudentDataAccess();
            this._studentService = new StudentService(this._studentRepo, this._userRepo);
            this._userService = new UserService(this._userRepo);

        }

        // GET: Admin
        public ActionResult Index()
        {
            var allStudentsFromData=this._studentService.GetAllStudentsWithUser();
            IList<AdminStudentViewModel> allStudents = new List<AdminStudentViewModel>();
            foreach (var item in allStudentsFromData)
            {
                AdminStudentViewModel entity = ChangeEntitiesFromDataToView.UserFromDataToAminStudentView(item);
                allStudents.Add(entity);
            }
            return View(allStudents);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            var student = this._studentService.GetStudentWithUser(id);
            var model = ChangeEntitiesFromDataToView.UserFromDataToAminStudentView(student);
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }


        // POST: Admin/Create
        [HttpPost]
        public ActionResult Add(AdminStudentViewModel studentView)
        {

            if (!ModelState.IsValid)
            {
                return View("Add", studentView);
            }

            var user = ChangeEntitiesFromViewToData.StudentFromModelToUser(studentView);
            if (!(this._userService.UserNameNotFound(user.UserName)))
            {
                return View("Add", studentView);
            }
            var student = ChangeEntitiesFromViewToData.StudentFromModelToStudent(studentView);
            using (var scope = new TransactionScope())
            {try
                {
                int nr = this._userRepo.Add(user);
                student.UserId = nr;
                int idStudent = this._studentRepo.Add(student);
                scope.Complete();
                }
                catch (TransactionException)
                {
                    scope.Dispose();
                }
            }
            return RedirectToAction("Index");

        }




        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var student = this._studentService.GetStudentWithUser(id);
            var model = ChangeEntitiesFromDataToView.UserFromDataToAminStudentView(student);
            return View("Edit", model);
        }

        // POST: Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, AdminStudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", id);
            }
            var _student = ChangeEntitiesFromViewToData.StudentFromModelToStudent(student);
            var _user = ChangeEntitiesFromViewToData.StudentFromModelToUser(student);
            int idUser = _student.UserId;
            using (var scope = new TransactionScope())
            {
                try
                 { 
                var nrStudent = _studentRepo.Update(id, _student);
                var nrUser = _userRepo.Update(idUser, _user);
                scope.Complete();
                }
                catch (TransactionException)
                {
                    scope.Dispose();
                }
            }
            return RedirectToAction("Index");


        }

        // GET: Admin/Delete
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var student = _studentService.GetStudentWithUser(id);
            var model = ChangeEntitiesFromDataToView.UserFromDataToAminStudentView(student);
            return View(model);
        }

        // POST: Admin/Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteUser(int id)
        {
            var student = _studentRepo.GetById(id);
            var user = _userRepo.GetById(student.UserId);
            using (var scope = new TransactionScope())
            {
                try
                {
                    _studentRepo.Delete(student);
                    _userRepo.Delete(user);
                    scope.Complete();
                }
                catch (TransactionException)
                {
                    scope.Dispose();
                }
            }
            return RedirectToAction("Index");

        }
    }
}