using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
    public class User:EntityBase
    {

        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public UserCategoryTypes Category { get; set; }


        // public IList<User> UsersList { get; set; } = new List<User>();
        
    }
    public enum UserCategoryTypes

    {
        Admin, Student, Teacher
    }
}

