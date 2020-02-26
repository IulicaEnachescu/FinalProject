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
   public class ClassMessagesDataAccess : BaseDataAccess<ClassMessages>, IClassMessagesRepository
    {
        /*ClassMessages]([Id] [int] IDENTITY(1,1) NOT NULL,

[ClassId][int] NOT NULL,
[Date][date] NOT NULL,
[MessageBody][varchar](500) NOT NULL,
*/
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[ClassMessages]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[ClassId]=@ClassId, [Date]=@Date, [MessageBody]=@MessageBody";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@ClassId, @Date, @MessageBody)";
            }
        }
        public IList<ClassMessages> ClassMessagess { get; set; } = new List<ClassMessages>();

        protected override SqlParameter[] ReturnSqlParamAdd(ClassMessages entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[3];
            param[i++] = new SqlParameter("@ClassId", SqlDbType.Int) { Value = entity.ClassId };
            param[i++] = new SqlParameter("@Date", SqlDbType.Date) { Value = entity.Date };
            param[i++] = new SqlParameter("@MessageBody", SqlDbType.VarChar) { Value = entity.MessageBody };

            return param;
        }


        protected override ClassMessages CompleteEntity(int id, ClassMessages entity)
        {        //complete the object entity from database if has empty fields
            IList<ClassMessages> list = GetAll();
            ClassMessages copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return entity;
            if (string.IsNullOrEmpty(entity.MessageBody)) entity.MessageBody = copy.MessageBody;
            if (entity.ClassId == 0) entity.ClassId = copy.ClassId;
            if (entity.Date == DateTime.MinValue) entity.Date = copy.Date;

            return entity;

        }


        //get data from reader to list
        protected override IList<ClassMessages> ReadReader(SqlDataReader read)
        {
            IList<ClassMessages> entities = new List<ClassMessages>();
            while (read.Read())
            {
                
                var currentRow = read;
               var  entity = ReadCurrentRow(currentRow);

                entities.Add(entity);
            }
            read.Close();
            return entities;
        }

        private static ClassMessages ReadCurrentRow(SqlDataReader currentRow)
        {
            ClassMessages entity = new ClassMessages();
            entity.Id = (int)currentRow["Id"];
            entity.ClassId = (int)currentRow["ClassId"];
            entity.Date = (DateTime)currentRow["Date"];
            entity.MessageBody = currentRow["MessageBody"].ToString();
            return entity;
        }

        //get data from reader for a row
        protected override ClassMessages ReadRow(SqlDataReader read)
        {
            ClassMessages entity = new ClassMessages();
            while (read.Read())
            {
                entity = ReadCurrentRow(read);
            }
            read.Close();
            return entity;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(ClassMessages entity)

        {
            return ReturnSqlParamAdd(entity);
        }

        public IList<ClassMessages> GetMessagesByClassId(int id)
        {
            var list = this.GetAll();
            return list.Where(x => x.ClassId == id).ToList();
        }
    }
}
