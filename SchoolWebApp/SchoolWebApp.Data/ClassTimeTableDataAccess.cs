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
    public class ClassTimeTableDataAccess : BaseDataAccess<ClassTimetable>, IClassTimetableRepository
    {
        /*        [ClassId] [int] NOT NULL,
 [LessonNumber][int] NOT NULL,
 [ClassDate][date]
        NOT NULL,*/
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[ClassTimetable]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[ClassId]=@ClassId, [LessonNumber]=@LessonNumber, [ClassDate]=@ClassDate";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@ClassId, @LessonNumber, @ClassDate)";
            }
        }
        public IList<ClassTimetable> ClassTimetables { get; set; } = new List<ClassTimetable>();

        protected override SqlParameter[] ReturnSqlParamAdd(ClassTimetable entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[3];
            param[i++] = new SqlParameter("@ClassId", SqlDbType.Int) { Value = entity.ClassId };
            param[i++] = new SqlParameter("@LessonNumber", SqlDbType.Int) { Value = entity.LessonNumber };
            param[i++] = new SqlParameter("@ClassDate", SqlDbType.Date) { Value = entity.ClassDate };

            return param;
        }


        protected override ClassTimetable CompleteEntity(int id, ClassTimetable entity)
        {        //complete the object entity from database if has empty fields
            IList<ClassTimetable> list = GetAll();
            ClassTimetable copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return null;
            if (entity.LessonNumber == 0) entity.LessonNumber = copy.LessonNumber;
            if (entity.ClassId == 0) entity.ClassId = copy.ClassId;
            if (entity.ClassDate == DateTime.MinValue) entity.ClassDate = copy.ClassDate;
            return entity;
        }


        //get data from reader to list
        protected override IList<ClassTimetable> ReadReader(SqlDataReader read)
        {
            IList<ClassTimetable> entities = new List<ClassTimetable>();
            while (read.Read())
            {
                var currentRow = read;
                var entity = ReadCurrRow(currentRow);
                entities.Add(entity);
            }
            read.Close();
            return entities;
        }

        private static ClassTimetable ReadCurrRow(SqlDataReader currentRow)
        {
            ClassTimetable entity = new ClassTimetable();
            entity.Id = (int)currentRow["Id"];
            entity.ClassId = (int)currentRow["ClassId"];
            entity.LessonNumber = (int)currentRow["LessonNumber"];
            entity.ClassDate = (DateTime)currentRow["ClassDate"];
            return entity;
        }

        //get data from reader for a row
        protected override ClassTimetable ReadRow(SqlDataReader read)
        {
            ClassTimetable entity = new ClassTimetable();
            while (read.Read())
            {
                var currentRow = read;
                entity = ReadCurrRow(currentRow);
            }
            read.Close();
            return entity;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(ClassTimetable entity)

        {
            return ReturnSqlParamAdd(entity);
        }

        public IList<ClassTimetable> GetClassTimetablesByClassId(int id)
        {
            var list = this.GetAll();
            return list.Where(x => x.ClassId == id).ToList();
        }
    }
}
