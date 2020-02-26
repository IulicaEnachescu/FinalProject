using SchoolDBModel.EntityTypes;
using SchoolWebApp.Services;
using SchoolWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Data
{
    public class StudentPaymentDataAccess : BaseDataAccess<StudentPayment>, IStudentPaymentRepository
    {/*[StudentId][int] NOT NULL,
[ClassId] [int] NOT NULL,
[PaymentDate] [date] NOT NULL,
[Ammount][decimal](5,2) NOT NULL,*/

        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[StudentPayment]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[StudentId]=@StudentId, [ClassId]=@ClassId, [PaymentDate]=@PaymentDate, [Ammount]=@Ammount";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "( @StudentId, @ClassId, @PaymentDate, @Ammount)";
            }
        }

        public IList<StudentPayment> StudentPayments { get; set; } = new List<StudentPayment>();

        protected override SqlParameter[] ReturnSqlParamAdd(StudentPayment entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[4];
            param[i++] = new SqlParameter("@StudentId", SqlDbType.Int) { Value = entity.StudentId };
            param[i++] = new SqlParameter("@ClassId", SqlDbType.Int) { Value = entity.ClassId };
            param[i++] = new SqlParameter("@PaymentDate", SqlDbType.Date) { Value = entity.PaymentDate };
            param[i++] = new SqlParameter("@Ammount", SqlDbType.Decimal) { Value = entity.Ammount };
            return param;
        }


        protected override StudentPayment CompleteEntity(int id, StudentPayment entity)
        {        //complete the object entity from database if has empty fields
            IList<StudentPayment> list = GetAll();
            StudentPayment copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return null;
            if (entity.ClassId == 0) entity.ClassId = copy.ClassId;
            if (entity.StudentId == 0) entity.StudentId = copy.StudentId;
            if (entity.Ammount == 0) entity.Ammount = copy.Ammount;
            if (entity.PaymentDate == DateTime.MinValue) entity.PaymentDate = copy.PaymentDate;
            return entity;

        }

        protected override IList<StudentPayment> ReadReader(SqlDataReader read)
        {
            IList<StudentPayment> entities = new List<StudentPayment>();
            while (read.Read())
            {
                var currentRow = read;
                var entity = ReadCurrentRow(currentRow);
                entities.Add(entity);
            }
            read.Close();
            return entities;
        }

        private static StudentPayment ReadCurrentRow(SqlDataReader currentRow)
        {
            StudentPayment entity = new StudentPayment();
            entity.Id = (int)currentRow["Id"];
            entity.StudentId = (int)currentRow["StudentId"];
            entity.ClassId = (int)currentRow["ClassId"];
            entity.PaymentDate = (DateTime)currentRow["PaymentDate"];
            entity.Ammount = (decimal)currentRow["Ammount"];
            return entity;
        }

        //get data from reader for a row
        protected override StudentPayment ReadRow(SqlDataReader read)
        {
            StudentPayment entity = new StudentPayment();
            while (read.Read())
            {
                entity = ReadCurrentRow(read);
            }
            read.Close();
            return entity;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(StudentPayment entity)

        {
            return ReturnSqlParamAdd(entity);
        }

        public IList<StudentPayment> GetStudentPaymentsByStudentIdAndByClassId(int studId, int clId)
        {
            var list = this.GetAll();
            return list.Where(x => (x.StudentId == studId && x.ClassId == clId)).ToList();
        }
        public IList<StudentPayment> GetStudentPaymentsByClassId(int clId)
        {
            var list = this.GetAll();
            return list.Where(x => x.ClassId == clId).ToList();
        }
    }
}
