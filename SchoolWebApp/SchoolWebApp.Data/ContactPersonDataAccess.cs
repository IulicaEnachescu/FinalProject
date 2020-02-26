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
   public class ContactPersonDataAccess:BaseDataAccess<ContactPerson>, IContactPersonRepository
    {
        /*[Name] [varchar](50) NOT NULL,
[Adress][varchar](200) NOT NULL,
[Phone][varchar](20) NOT NULL,
[Email][varchar](30) NOT NULL,*/
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[ContactPerson]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[Name]=@Name, [Adress]=@Adress, [Phone]=@Phone, [Email]=@Email";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@Name, @Adress, @Phone, @Email)";
            }
        }
        public IList<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();


        protected override SqlParameter[] ReturnSqlParamAdd(ContactPerson entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[4];
            param[i++] = new SqlParameter("@Name", SqlDbType.VarChar) { Value = entity.Name };
            param[i++] = new SqlParameter("@Adress", SqlDbType.VarChar) { Value = entity.Adress };
            param[i++] = new SqlParameter("@Phone", SqlDbType.VarChar) { Value = entity.Phone };
            param[i++] = new SqlParameter("@Email", SqlDbType.VarChar) { Value = entity.Email };
            return param;
        }


        protected override ContactPerson CompleteEntity(int id, ContactPerson entity)
        {        //complete the object entity from database if has empty fields

            ContactPerson copy = GetById(id); 
            if (copy == null) return null;
            if (String.IsNullOrEmpty(entity.Name)) entity.Name = copy.Name;
            if (String.IsNullOrEmpty(entity.Adress)) entity.Adress = copy.Adress;
            if (String.IsNullOrEmpty(entity.Phone)) entity.Phone = copy.Phone;
            if (String.IsNullOrEmpty(entity.Email)) entity.Email = copy.Email;
            return entity;

        }


        //get data from reader to list
        protected override IList<ContactPerson> ReadReader(SqlDataReader read)
        {
            IList<ContactPerson> entities = new List<ContactPerson>();
            while (read.Read())
            {
                var entity = ReadCurrentRow(read);
                entities.Add(entity);
            }
            read.Close();
            return entities;
        }
        //get data from reader for a row
        protected override ContactPerson ReadRow(SqlDataReader read)
        {
            ContactPerson entity = new ContactPerson();
            while (read.Read())
            {
                entity = ReadCurrentRow(read);
            }
            read.Close();
            return entity;
        }
        private static ContactPerson ReadCurrentRow(SqlDataReader currentRow)
        {

            ContactPerson cr = new ContactPerson();
            cr.Name = currentRow["Name"].ToString();
            cr.Adress = currentRow["Adress"].ToString();
            cr.Phone = currentRow["Phone"].ToString();
            cr.Email = currentRow["Email"].ToString();
            return cr;
        }


        protected override SqlParameter[] ReturnSqlParamUpdate(ContactPerson entity)

        {
            return ReturnSqlParamAdd(entity);
        }
    }
}
