using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.ViewModels
{
    public static class ChangeEntitiesFromViewToData
    {
        public static User StudentFromModelToUser(AdminStudentViewModel student)
        {
            User user = new User()
            {
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
                };
            return user;
        }
            public static Student StudentFromModelToStudent(AdminStudentViewModel student)
            {
            Student user = new Student()
            {
                StatusActive = student.StatusActive,
                ContactId = 0,
               
            };
               return user;

            }
    }
}
