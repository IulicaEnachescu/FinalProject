using SchoolDBModel.EntityTypes.ClassAndCourses;
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
    public class ClassStudentDataAccess : BaseDataAccess<ClassStudent>, IClassStudentRepository
    {
        /*  [Id]
        ,[ClassId]
        ,[StudentId]*/
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[ClassStudent]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[ClassId]=@ClassId, [StudentId]=@StudentId";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@ClassId, @StudentId)";
            }
        }
        public IList<ClassStudent> ClassMessagess { get; set; } = new List<ClassStudent>();

        protected override SqlParameter[] ReturnSqlParamAdd(ClassStudent entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[2];
            param[i++] = new SqlParameter("@ClassId", SqlDbType.Int) { Value = entity.ClassId};
            param[i++] = new SqlParameter("@StudentId", SqlDbType.Int) { Value = entity.StudentId};
            return param;
        }


        protected override ClassStudent CompleteEntity(int id, ClassStudent entity)
        {        //complete the object entity from database if has empty fields
            IList<ClassStudent> list = GetAll();
            ClassStudent copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return entity;
            if (entity.ClassId == 0) entity.ClassId = copy.ClassId;
            if (entity.StudentId == 0) entity.StudentId = copy.StudentId;
            return entity;

        }


        //get data from reader to list
        protected override IList<ClassStudent> ReadReader(SqlDataReader read)
        {
            IList<ClassStudent> entities = new List<ClassStudent>();
            while (read.Read())
            {

                var currentRow = read;
                var entity = ReadCurrentRow(currentRow);

                entities.Add(entity);
            }
            read.Close();
            return entities;
        }

        private static ClassStudent ReadCurrentRow(SqlDataReader currentRow)
        {
            ClassStudent entity = new ClassStudent();
            entity.Id = (int)currentRow["Id"];
            entity.ClassId = (int)currentRow["ClassId"];
            entity.StudentId = (int)currentRow["StudentId"];
            return entity;
        }

        //get data from reader for a row
        protected override ClassStudent ReadRow(SqlDataReader read)
        {
            ClassStudent entity = new ClassStudent();
            while (read.Read())
            {
                entity = ReadCurrentRow(read);
            }
            read.Close();
            return entity;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(ClassStudent entity)

        {
            return ReturnSqlParamAdd(entity);
        }

     
        public IList<ClassStudent> GetClassIdByStudentId(int id)
        {
            var list = this.GetAll();
            return list.Where(x => x.StudentId == id).ToList();
        }

        public IList<ClassStudent> GetStudentsIdByClassId(int id)
        {
            var list = this.GetAll();
            return list.Where(x => x.ClassId== id).ToList();
        }
    }
}
