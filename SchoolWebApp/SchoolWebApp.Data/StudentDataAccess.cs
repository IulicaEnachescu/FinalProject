using SchoolDBModel.EntityTypes;
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
    public class StudentDataAccess : BaseDataAccess<Student>, IStudentRepository
    {
        /*[UserId] [int] NOT NULL,
[StatusActive][bit] NOT NULL,
[ContactId][int],*/
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[Student]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[UserId]=@UserId, [StatusActive]=@StatusActive, [ContactId]=@ContactId";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@UserId, @StatusActive)";
            }
        }
        public IList<Student> Students { get; set; } = new List<Student>();

        public override string ColumnInsertDefinition()
        {
            return " ([UserId],[StatusActive]) ";
        }

        protected override SqlParameter[] ReturnSqlParamAdd(Student entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[2];
            param[i++] = new SqlParameter("@UserId", SqlDbType.Int) { Value = entity.UserId };
            param[i++] = new SqlParameter("@StatusActive", SqlDbType.Bit) { Value = entity.StatusActive };
            //param[i++] = new SqlParameter("@ContactId", SqlDbType.Int) { Value = entity.ContactId };
            return param;
        }


        protected override Student CompleteEntity(int id, Student entity)
        {        //complete the object entity from database if has empty fields
            IList<Student> list = GetAll();
            Student copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return null;
            if (entity.UserId == 0) entity.UserId = copy.UserId;
            if (entity.ContactId == 0) entity.ContactId = copy.ContactId;

            return entity;

        }


        //get data from reader to list
        protected override IList<Student> ReadReader(SqlDataReader read)
        {
            IList<Student> entities = new List<Student>();
            while (read.Read())
            {

                var entity = ReadCurrentRow(read);
                entities.Add(entity);
            }
            read.Close();
            return entities;
        }
        //get data from reader for a row
        protected override Student ReadRow(SqlDataReader read)
        {
            Student entity = new Student();
            while (read.Read())
            {

                entity = ReadCurrentRow(read);

            }
            read.Close();
            return entity;
        }

        private static Student ReadCurrentRow(SqlDataReader currentRow)
        {
            Student entity = new Student();
            entity.Id = (int)currentRow["Id"];
            entity.UserId = (int)currentRow["UserId"];
            entity.StatusActive = (bool)currentRow["StatusActive"];
            var c = currentRow["ContactId"];
            if (!(c is DBNull))
            {
                entity.ContactId = (int)c;
            }

            return entity;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(Student entity)

        {
            return ReturnSqlParamAdd(entity);
        }



        public IList<Student> GetStudentsByContactPersonId(int id)
        {
            var list = this.GetAll();
            return list.Where(x => x.ContactId == id).ToList();
        }
    }
}
