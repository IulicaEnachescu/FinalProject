using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.ViewModels
{
    public static class ChangeEntitiesFromDataToView
    {
        public static LoginViewModel LoginFromDataToView(User user)
        {
            LoginViewModel login = new LoginViewModel();
            login.Id = user.Id;
            login.UserName = user.UserName;
            login.Password = user.Password;
            login.Category = user.Category;
            return login;
        }
        public static AdminStudentViewModel UserFromDataToAminStudentView(Student student)
        {
            AdminStudentViewModel _view = new AdminStudentViewModel()
            {
                Id = student.Id,
                UserName = student.UserName,
                LastName = student.LastName,
                FirstName = student.FirstName,
                Password = student.Password,
                DateBirth = student.DateBirth,
                CreateDate = student.CreateDate,
                City = student.City,
                Adress = student.Adress,
                Phone = student.Phone,
                Email = student.Email,
                Category = student.Category,
                StatusActive = student.StatusActive,
            };  
                return _view; 
           
        }
    }

}

