using SchoolDBModel.EntityTypes;
using SchoolWebApp.Services.Interfaces;
using SchoolWebApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.StudentServices
{
    public class StudentPaymentService : IStudentPaymentService
    {
        private readonly IStudentRepository studentRepository;
        private readonly IClassRepository classRepository;
        private readonly IStudentPaymentRepository paymentRepository;



        public StudentPaymentService(IStudentRepository studentRepository, IClassRepository classRepository, IStudentPaymentRepository paymentRepository)
        {
            this.studentRepository = studentRepository;
            this.classRepository = classRepository;
            this.paymentRepository = paymentRepository;
        }


        public StudentStudentPayment GetStudentPaymentsByClassId(int stId, int classId)
        {
            var pay = this.paymentRepository.GetStudentPaymentsByStudentIdAndByClassId(stId, classId);
            StudentStudentPayment st = new StudentStudentPayment();
            st.Student = this.studentRepository.GetById(stId);
            st.Class = this.classRepository.GetById(classId);
            IList<DatePayment> datePay = new List<DatePayment>();
            foreach (var item in pay)
            {
                var n = new DatePayment { Date = item.PaymentDate, Ammount = item.Ammount };
                datePay.Add(n);
            }
            st.DatePayments = datePay;
            return st;
        }



        public double GetStudentTotalPaymentByClassId(int stId, int classId)
        {
            var pay = this.GetStudentPaymentsByClassId(stId, classId);
            double s = (double)pay.DatePayments.Sum(n => n.Ammount);
            return s;
        }


    }
    public class StudentStudentPayment
    {
        public Student Student { get; set; }
        public Class Class { get; set; }

        public IList<DatePayment> DatePayments { get; set; }

    }
    public class DatePayment
    {
        public DateTime Date { get; set; }
        public Decimal Ammount { get; set; }
    }
}
