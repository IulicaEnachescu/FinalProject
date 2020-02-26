using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
    public class StudentPayment:EntityBase
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Ammount { get; set; }

       

    }
}
