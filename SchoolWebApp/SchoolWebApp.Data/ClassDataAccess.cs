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
  public class ClassDataAccess : BaseDataAccess<Class>, IClassRepository
    {
        /*[CourseId] [int] NOT NULL,
[TeacherId][int] NOT NULL,
[Name][varchar](20) NOT NULL,
[Description][varchar](300) NOT NULL,
[StartDate][date] NOT NULL,
[EndDate] [date] NOT NULL,*/
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[Class]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[CourseId]=@CourseId, [TeacherId]=@TeacherId," +
                    " [Name]=@Name,[ClassDescription]=@ClassDescription, [StartDate]=@StartDate,[EndDate]=@EndDate,[Price]=@Price";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@CourseId, @TeacherId, @Name, @ClassDescription, @StartDate, @EndDate, @Price)";
            }
        }
        public IList<Class> Classes { get; set; } = new List<Class>();

        protected override SqlParameter[] ReturnSqlParamAdd(Class entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[7];
            param[i++] = new SqlParameter("@CourseId ", SqlDbType.Int) { Value = entity.CourseId };
            param[i++] = new SqlParameter("@TeacherId", SqlDbType.Int) { Value = entity.TeacherId };
            param[i++] = new SqlParameter("@Name", SqlDbType.VarChar) { Value = entity.Name };
            param[i++] = new SqlParameter("@ClassDescription", SqlDbType.VarChar) { Value = entity.ClassDescription };
            param[i++] = new SqlParameter("@StartDate", SqlDbType.Date) { Value = entity.StartDate };
            param[i++] = new SqlParameter("@EndDate", SqlDbType.Date) { Value = entity.EndDate };
            param[i++] = new SqlParameter("@Price", SqlDbType.Decimal) { Value = entity.Price };
            return param;
        }


        protected override Class CompleteEntity(int id, Class entity)
        {        //complete the object entity from database if has empty fields
            IList<Class> list = GetAll();
            Class copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return null;
            if (entity.CourseId == 0) entity.CourseId = copy.CourseId;
            if (entity.TeacherId == 0) entity.TeacherId = copy.TeacherId;
            if (string.IsNullOrEmpty(entity.Name)) entity.Name = copy.Name;
            if (string.IsNullOrEmpty(entity.ClassDescription)) entity.ClassDescription = copy.ClassDescription;
            if (entity.StartDate == DateTime.MinValue) entity.StartDate = copy.StartDate;
            if (entity.EndDate == DateTime.MinValue) entity.EndDate = copy.EndDate;
            if (entity.Price == 0) entity.Price = copy.Price;
            return entity;

        }

        protected override IList<Class> ReadReader(SqlDataReader read)
        {
            IList<Class> courses = new List<Class>();
            while (read.Read())
            {
                var currentRow = read;
                var course = ReadCurrentRow(currentRow);
                courses.Add(course);
            }
            read.Close();
            return courses;
        }

        private static Class ReadCurrentRow(SqlDataReader currentRow)
        {
            Class course = new Class();
            course.Id = (int)currentRow["Id"];
            course.CourseId = (int)currentRow["CourseId"];
            course.TeacherId = (int)currentRow["TeacherId"];
            course.Name = currentRow["Name"].ToString();
            course.ClassDescription = currentRow["ClassDescription"].ToString();
            course.StartDate = (DateTime)currentRow["StartDate"];
            course.EndDate = (DateTime)currentRow["EndDate"];
            course.Price = (Decimal)currentRow["Price"];
            return course;
        }

        //get data from reader for a row
        protected override Class ReadRow(SqlDataReader read)
        {

            Class course = new Class();
            while (read.Read())
            {
                course = ReadCurrentRow(read);

            }
            read.Close();
            return course;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(Class entity)

        {
            return ReturnSqlParamAdd(entity);
        }
        public IList<Class> GetClassesByCourseId(int id)
        {
            var list = this.GetAll();
            return list.Where(x => x.CourseId == id).ToList();
        }



        public IList<Class> GetClassesByTeacherId(int id)
        {
            var list = this.GetAll();
            return list.Where(x => x.TeacherId == id).ToList();
        }
    }

}

