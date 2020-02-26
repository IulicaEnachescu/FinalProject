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
    class AdminDataAccess:BaseDataAccess<Admin>, IAdminRepository
    {
        /*[UserId] [int] NOT NULL,
[Role] [nvarchar](20) NOT NULL,*/
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[Admin]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[UserId]=@UserId, [Role]=@Role";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@UserId, @Role)";
            }
        }
        public IList<Admin> Admins { get; set; } = new List<Admin>();

        protected override SqlParameter[] ReturnSqlParamAdd(Admin entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[2];
            param[i++] = new SqlParameter("@UserId", SqlDbType.Int) { Value = entity.UserId };
            param[i++] = new SqlParameter("@Role", SqlDbType.VarChar) { Value = entity.Role };
            return param;
           
        }


        protected override Admin CompleteEntity(int id, Admin entity)
        {        //complete the object entity from database if has empty fields
            IList<Admin> list = GetAll();
           Admin copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return null;
            if (entity.UserId == 0) entity.UserId = copy.UserId;
            if (String.IsNullOrEmpty(entity.Role)) entity.Role = copy.Role;

            return entity;

        }
       

        //get data from reader to list
        protected override IList<Admin> ReadReader(SqlDataReader read)
        {
            IList<Admin> entities = new List<Admin>();
            while (read.Read())
            {
               
               var entity=ReadCurrentRow(read);
                entities.Add(entity);
            }
            read.Close();
            return entities;
        }

        private static Admin ReadCurrentRow(SqlDataReader currentRow)
        {
            Admin entity = new Admin();
            entity.Id = (int)currentRow["Id"];
            entity.UserId = (int)currentRow["UserId"];
            entity.Role = currentRow["Role"].ToString();
            return entity;
        }

        //get data from reader for a row
        protected override Admin ReadRow(SqlDataReader read)
        {
            Admin entity = new Admin();
            while (read.Read())
            {
                entity = ReadCurrentRow(read);
            }
            read.Close();
            return entity;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(Admin entity)

        {
            return ReturnSqlParamAdd(entity);
        }

    }
}
