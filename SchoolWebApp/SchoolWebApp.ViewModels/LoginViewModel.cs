using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.ViewModels
{
    public class LoginViewModel
    {
        public int Id;

        [Display(Name = "Username")]
        [Required(ErrorMessage = "This field is required")]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [NotMapped]
        public string LoginErrorMessage { get; set; }

        [NotMapped]
        public UserCategoryTypes Category { get; set; }
    }
}

