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
    public class TeacherDataAccess:BaseDataAccess<Teacher>, ITeacherRepository
    {
        /*[UserId] [int] NOT NULL,
[RoleDescription][varchar](50) NOT NULL,
[StatusActive][bit] NOT NULL,*/
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[Teacher]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[UserId]=@UserId, [RoleDescription]=@RoleDescription, [StatusActive]=@StatusActive";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@UserId, @RoleDescription, @StatusActive)";
            }
        }
        public IList<Teacher> Teachers { get; set; } = new List<Teacher>();
        
        protected override SqlParameter[] ReturnSqlParamAdd(Teacher entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[3];
            param[i++] = new SqlParameter("@UserId", SqlDbType.Int) { Value = entity.UserId };
            param[i++] = new SqlParameter("@RoleDescription", SqlDbType.VarChar) { Value = entity.RoleDescription };
            param[i++] = new SqlParameter("@StatusActive", SqlDbType.Bit) { Value = entity.StatusActive };
            return param;
        }


        protected override Teacher CompleteEntity(int id, Teacher entity)
        {        //complete the object entity from database if has empty fields
            IList<Teacher> list = GetAll();
            Teacher copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return null;
            if (entity.UserId == 0) entity.UserId = copy.UserId;
            if (string.IsNullOrEmpty(entity.RoleDescription)) entity.RoleDescription = copy.RoleDescription;
            
            return entity;

        }
        
        //get data from reader to list
        protected override IList<Teacher> ReadReader(SqlDataReader read)
        {
            IList<Teacher> entities = new List<Teacher>();
            while (read.Read())
            {
                
               var entity = ReadCurrentRow(read);
                entities.Add(entity);
            }
            read.Close();
            return entities;
        }
        //get data from reader for a row
        protected override Teacher ReadRow(SqlDataReader read)
        {
            Teacher entity = new Teacher();
            while (read.Read())
            {
                entity = ReadCurrentRow(read);
                
            }
            read.Close();
            return entity;
        }

        private static Teacher ReadCurrentRow(SqlDataReader currentRow)
        {
            Teacher entity = new Teacher();
            entity.Id = (int)currentRow["Id"];
            entity.UserId = (int)currentRow["UserId"];
            entity.RoleDescription = currentRow["RoleDescription"].ToString();
            entity.StatusActive = (bool)currentRow["StatusActive"];
            return entity;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(Teacher entity)

        {
            return ReturnSqlParamAdd(entity);
        }


    }
}

