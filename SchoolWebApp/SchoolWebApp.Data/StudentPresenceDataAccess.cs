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
    public class StudentPresenceDataAccess : BaseDataAccess<StudentPresence>, IStudentPresenceRepository
    {
        /*[ClassTimetableId][int] NOT NULL,
[StudentId][int] NOT NULL,
[Presence][bit] NOT NULL,*/
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[StudentPresence]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[ClassTimetableId]=@ClassTimetableId, [StudentId]=@StudentId, [Presence]=@Presence";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@ClassId, @StudentId, @Presence)";
            }
        }
        public IList<StudentPresence> StudentPresences { get; set; } = new List<StudentPresence>();

        protected override SqlParameter[] ReturnSqlParamAdd(StudentPresence entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[3];
            param[i++] = new SqlParameter("@ClassTimetableId", SqlDbType.Int) { Value = entity.ClassTimetableId };
            param[i++] = new SqlParameter("@StudentId", SqlDbType.Int) { Value = entity.StudentId };
            param[i++] = new SqlParameter("@Presence", SqlDbType.Bit) { Value = entity.Presence };
            
            return param;
        }


        protected override StudentPresence CompleteEntity(int id, StudentPresence entity)
        {        //complete the object entity from database if has empty fields
            IList<StudentPresence> list = GetAll();
            StudentPresence copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return null;
            if (entity.ClassTimetableId == 0) entity.ClassTimetableId = copy.ClassTimetableId;
            if (entity.StudentId == 0) entity.StudentId = copy.StudentId;
           return entity;

        }
       

        //get data from reader to list
        protected override IList<StudentPresence> ReadReader(SqlDataReader read)
        {
            IList<StudentPresence> entities = new List<StudentPresence>();
            while (read.Read())
            {
            
                var currentRow = read;
                var entity=ReadCurrentRow(currentRow);
                entities.Add(entity);
            }
            read.Close();
            return entities;
        }

        private static StudentPresence ReadCurrentRow(SqlDataReader currentRow)
        {
            StudentPresence entity = new StudentPresence();
            entity.Id = (int)currentRow["Id"];
            entity.ClassTimetableId = (int)currentRow["ClassTimetableId"];
            entity.StudentId = (int)currentRow["StudentId"];
            entity.Presence = (bool)currentRow["Presence"];
            return entity;
        }

        //get data from reader for a row
        protected override StudentPresence ReadRow(SqlDataReader read)
        {
            StudentPresence entity = new StudentPresence();
            while (read.Read())
            {
                entity = ReadCurrentRow(read);
            }
            read.Close();
            return entity;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(StudentPresence entity)

        {
            return ReturnSqlParamAdd(entity);
        }

      
        public IList<StudentPresence> GetPresenceByStudentId(int id)
        {
            var list = this.GetAll();
            return list.Where(x => x.StudentId == id).ToList();
        }

        public IList<StudentPresence> GetPresenceByClassTimetableId(int id)
        {
            var list = this.GetAll();
            return list.Where(x => x.ClassTimetableId == id).ToList();
        }
       

        public StudentPresence GetPresenceByStudentIdAndClassTimetableId(int stId, int clId)
        {
            var list = this.GetAll();
            return list.FirstOrDefault(x => (x.StudentId == stId && x.ClassTimetableId == clId));
        }
    }
}
