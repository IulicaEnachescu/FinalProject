using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolDBModel.EntityTypes;
using SchoolWebApp.Data;
using SchoolWebApp.Services.Interfaces;
using SchoolWebApp.ViewModels;

namespace SchoolWebApp.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        private IUserRepository _userManager;


        public LoginController()
        {
            this._userManager = new UserDataAcces();

        }


        public ActionResult Index()
        {
            return this.View();

        }

 

        [HttpPost]
        public ActionResult Login(LoginViewModel member)
        {
            var memberSql = this._userManager.GetUserByUserNameAndPassword(member.UserName, member.Password);

            if (memberSql == null)
            {
                member.LoginErrorMessage = "Invalid username or password";
                return View("Index", member);
            }
            else
            if (member.Category == UserCategoryTypes.Admin)
            {
                Session["MemberId"] = memberSql.Id;
                Session["MemberName"] = memberSql.UserName;
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            


            else
                if (member.Category == UserCategoryTypes.Student)
            {
                Session["MemberId"] = memberSql.Id;
                Session["MemberName"] = memberSql.UserName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["MemberId"] = memberSql.Id;
                Session["MemberName"] = memberSql.UserName;
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Details(int? id)
        {
            User member;
            if (id == null)
            {
                if (System.Web.HttpContext.Current.Session["MemberId"] != null)
                {
                    int id1 = Convert.ToInt32(Session["MemberId"].ToString());
                    member = this._userManager.GetById(id1);
                }
                else
                {
                    return View();
                }
            }
            else
                member = this._userManager.GetById(id ?? 0);

            var viewMember = ChangeEntitiesFromDataToView.LoginFromDataToView(member);

            return RedirectToAction("Index", "Home");
        }


    }
}
