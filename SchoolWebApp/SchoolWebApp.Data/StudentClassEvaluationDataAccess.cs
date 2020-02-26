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
    public class StudentClassEvaluationDataAccess : BaseDataAccess<StudentClassEvaluation>, IStudentClassEvaluationRepository
    {
        /*  [dbo].[StudentClassEvaluation] ([Id][int] IDENTITY(1,1) NOT NULL,


[ClassId][int] NOT NULL,
[StudentId][int] NOT NULL,
[Date][date]
        NOT NULL,
[Grade][int] NOT NULL,*/
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[StudentClassEvaluation]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[ClassId]=@ClassId, [StudentId]=@StudentId, [Date]=@Date, [Grade]=@Grade";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@ClassId, @StudentId, @Date, @Grade)";
            }
        }
        public IList<StudentClassEvaluation> StudentClassEvaluations { get; set; } = new List<StudentClassEvaluation>();

        protected override SqlParameter[] ReturnSqlParamAdd(StudentClassEvaluation entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[4];
            param[i++] = new SqlParameter("@ClassId", SqlDbType.Int) { Value = entity.ClassId };
            param[i++] = new SqlParameter("@StudentId", SqlDbType.Int) { Value = entity.StudentId };
            param[i++] = new SqlParameter("@Date", SqlDbType.Date) { Value = entity.Date };
            param[i++] = new SqlParameter("@Grade", SqlDbType.Int) { Value = entity.Grade };
            return param;
        }


        protected override StudentClassEvaluation CompleteEntity(int id, StudentClassEvaluation entity)
        {        //complete the object entity from database if has empty fields
            IList<StudentClassEvaluation> list = GetAll();
            StudentClassEvaluation copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return entity;
            if (entity.ClassId == 0) entity.ClassId = copy.ClassId;
            if (entity.StudentId == 0) entity.StudentId = copy.StudentId;
            if (entity.Date == DateTime.MinValue) entity.Date = copy.Date;
            if (entity.Grade == 0) entity.Grade = copy.Grade;
            return entity;

        }


        //get data from reader to list
        protected override IList<StudentClassEvaluation> ReadReader(SqlDataReader read)
        {
            IList<StudentClassEvaluation> entities = new List<StudentClassEvaluation>();
            while (read.Read())
            {
                var currentRow = read;
                var entity = ReadCurrentRow(currentRow);
                entities.Add(entity);
            }
            read.Close();
            return entities;
        }

        private static StudentClassEvaluation ReadCurrentRow(SqlDataReader currentRow)
        {
            StudentClassEvaluation entity = new StudentClassEvaluation();
            entity.Id = (int)currentRow["Id"];
            entity.ClassId = (int)currentRow["ClassId"];
            entity.StudentId = (int)currentRow["StudentId"];
            entity.Date = (DateTime)currentRow["Date"];
            entity.Grade = (int)currentRow["Grade"];
            return entity;
        }

        //get data from reader for a row
        protected override StudentClassEvaluation ReadRow(SqlDataReader read)
        {
            StudentClassEvaluation entity = new StudentClassEvaluation();
            while (read.Read())
            {
                var currentRow = read;
                entity = ReadCurrentRow(currentRow);
            }
            read.Close();
            return entity;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(StudentClassEvaluation entity)

        {
            return ReturnSqlParamAdd(entity);
        }

        public IList<StudentClassEvaluation> GetStudentClassEvaluationByClassId(int id)
        {
            var list = this.GetAll();
            return list.Where(x => x.StudentId == id).ToList();
        }



        public IList<StudentClassEvaluation> GetStudentClassEvaluationByStudentIdAndByClassId(int idStudent, int idClass)
        {
            var list = this.GetAll();
            return list.Where(x => (x.ClassId == idClass && x.StudentId == idStudent)).ToList();
        }

        public IList<StudentClassEvaluation> GetStudentClassEvaluationById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
