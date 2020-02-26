using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.ViewModels
{
    public class AdminStudentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        [Required]
        [DataType (DataType.Text)]
        public string City { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Adress { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public UserCategoryTypes Category { get; set; }
        [Required]
       
        public bool StatusActive { get; set; }
        public int? ContactId { get; set; }
    }
}
