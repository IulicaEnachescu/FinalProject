using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SchoolWebApp.Services;
using System.Text;
using System.Threading.Tasks;
using SchoolWebApp.Services.Interfaces;

namespace SchoolWebApp.Data
{
    public class CourseDataAccess:BaseDataAccess<Course>, ICourseRepository
    {
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[Course]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[NumberOfLessons]=@NumberOfLessons, [Description]=@Description," +
                    " [Category]=@Category,[Language]=@Language, [Level]=@Level,[StatusActive]=@StatusActive";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@NumberOfLessons, @Description, @Category, @Language, @Level, @StatusActive)";
            }
        }
        public IList<Course> Courses { get; set; } = new List<Course>();
      
        protected override SqlParameter[] ReturnSqlParamAdd(Course entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[6];
            param[i++] = new SqlParameter("@NumberOfLessons", SqlDbType.Int) { Value = entity.NumberOfLessons };
            param[i++] = new SqlParameter("@Description", SqlDbType.VarChar) { Value = entity.Description };
            param[i++] = new SqlParameter("@Category", SqlDbType.VarChar) { Value = entity.Category };
            param[i++] = new SqlParameter("@Language", SqlDbType.VarChar) { Value = entity.Language };
            param[i++] = new SqlParameter("@Level", SqlDbType.VarChar) { Value = entity.Level };
            param[i++] = new SqlParameter("@StatusActive", SqlDbType.Bit) { Value = entity.StatusActive };
            return param;
        }


        protected override Course CompleteEntity(int id, Course entity)
        {        //complete the object entity from database if has empty fields
            IList<Course> list = GetAll();
            Course copyCourse = list.Where(x => x.Id == id).FirstOrDefault();
            if (copyCourse == null) return null;
            if (string.IsNullOrEmpty(entity.Description)) entity.Description = copyCourse.Description;
            if (object.Equals(entity.Language, null)) entity.Language = copyCourse.Language;
            if (object.Equals(entity.Level, null)) entity.Level = copyCourse.Level;
            if (object.Equals(entity.Category, null)) entity.Category = copyCourse.Category;
            return entity;

        }
        

        //get data from reader to list
        protected override IList<Course> ReadReader(SqlDataReader read)
        {
            IList<Course> courses = new List<Course>();
            while (read.Read())
            {
               var course=ReadCurrentRow(read);
               courses.Add(course);
            }
            read.Close();
            return courses;
        }

        private static Course ReadCurrentRow(SqlDataReader currentRow)
        {
            Course course = new Course();
            course.Id = (int)currentRow["Id"];
            course.NumberOfLessons = (int)currentRow["NumberOfLessons"];
            course.Description = currentRow["Description"].ToString();

            course.Category = (CategoryTypes)Enum.Parse(typeof(CategoryTypes), currentRow["Category"].ToString());
            course.Language = (LanguageTypes)Enum.Parse(typeof(LanguageTypes), currentRow["Language"].ToString());
            course.Level = (LevelTypes)Enum.Parse(typeof(LevelTypes), currentRow["Level"].ToString());
            course.StatusActive = (bool)currentRow["StatusActive"];
            return course;
        }

        //get data from reader for a row
        protected override Course ReadRow(SqlDataReader read)
        {

            Course course = new Course();
            while (read.Read())
            {
                course = ReadCurrentRow(read);

            }
            read.Close();
            return course;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(Course entity)
       
        {
            return ReturnSqlParamAdd(entity);
        }
    }
        
}
