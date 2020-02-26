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
    public class UserDataAcces : BaseDataAccess<User>, IUserRepository
    {
        protected override string TableName
        {
            get
            { return "[SchoolApp].[dbo].[User]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[LastName]=@LastName,[FirstName]=@FirstName,[Password]=@Password,[DateBirth]=@DateBirth,[Category]=@Category,[CreateDate]=@CreateDate,[City]=@City,[Adress]=@Adress,[Phone]=@Phone,[Email]=@Email";
            }
        }
        protected override string AddCommand
        {
            get
            {   
                return "(@UserName, @LastName, @FirstName, @Password, @DateBirth, @Category, @CreateDate, @City, @Adress, @Phone, @Email)";
            }
        }
        public IList<User> Users { get; set; } = new List<User>();

        protected override SqlParameter[] ReturnSqlParamAdd(User entity)
        {
            //(@UserName, @LastName, @FirstName, @Password, @DateBirth, @Category, @CreateDate, @City, @Adress, @Phone, @Email)"
            SqlParameter parameterUserName = new SqlParameter("UserName", SqlDbType.VarChar);
            parameterUserName.Value = entity.UserName;
            SqlParameter[] param1 = ReturnSqlParamUpdate(entity);
            int j = param1.Length + 1;
            SqlParameter[] param = new SqlParameter[j];
            param[0] = parameterUserName;
            for (int i = 1; i < j; i++)
            {
                param[i] = param1[i - 1];
            }
            return param;
        }

        protected override SqlParameter[] ReturnSqlParamUpdate(User entity)
        {
            int i = 0;
            SqlParameter[] param = new SqlParameter[10];

            param[i++] = new SqlParameter("@LastName", SqlDbType.VarChar) { Value = entity.LastName };
            param[i++] = new SqlParameter("@FirstName", SqlDbType.VarChar) { Value = entity.FirstName };
            param[i++] = new SqlParameter("@Password", SqlDbType.VarChar) { Value = entity.Password };
            param[i++] = new SqlParameter("@DateBirth", SqlDbType.Date) { Value = entity.DateBirth };
            param[i++] = new SqlParameter("@Category", SqlDbType.VarChar) { Value = entity.Category };
            param[i++] = new SqlParameter("@CreateDate", SqlDbType.Date) { Value = entity.CreateDate };
            param[i++] = new SqlParameter("@City", SqlDbType.VarChar) { Value = entity.City };
            param[i++] = new SqlParameter("@Adress", SqlDbType.VarChar) { Value = entity.Adress };
            param[i++] = new SqlParameter("@Phone", SqlDbType.VarChar) { Value = entity.Phone };
            param[i++] = new SqlParameter("@Email", SqlDbType.VarChar) { Value = entity.Email };

            return param;
        }


        protected override User CompleteEntity(int id, User entity)
        {        //complete the object entity from database if has empty fields
            ////(@UserName, @LastName, @FirstName, @Password, @DateBirth, @Category, @City, @Adress, @Phone, @Email)"
            IList<User> list = GetAll();
            User copy = list.Where(x => x.Id == id).FirstOrDefault();
            if (copy == null) return null;
            // if (string.IsNullOrEmpty(entity.UserName)) entity.UserName = copy.UserName;
            if (string.IsNullOrEmpty(entity.LastName)) entity.LastName = copy.LastName;
            if (string.IsNullOrEmpty(entity.FirstName)) entity.FirstName = copy.FirstName;
            if (string.IsNullOrEmpty(entity.Password)) entity.Password = copy.Password;
            if (entity.DateBirth == DateTime.MinValue) entity.DateBirth = copy.DateBirth;
            if (object.Equals(entity.Category, null)) entity.Category = copy.Category;
            if (entity.CreateDate == DateTime.MinValue) entity.CreateDate = copy.CreateDate;
            if (string.IsNullOrEmpty(entity.City)) entity.City = copy.City;
            if (string.IsNullOrEmpty(entity.Adress)) entity.Adress = copy.Adress;
            if (string.IsNullOrEmpty(entity.Phone)) entity.Phone = copy.Phone;
            if (string.IsNullOrEmpty(entity.Email)) entity.Email = copy.Email;
            return entity;

        }


        //get data from reader to list
        protected override IList<User> ReadReader(SqlDataReader read)
        {
            IList<User> crs = new List<User>();
            while (read.Read())
            {
                var cr = ReadCurrentRow(read);
                crs.Add(cr);
            }
            read.Close();
            return crs;
        }
        //get data from reader for a row
        protected override User ReadRow(SqlDataReader read)
        {

            User cr = new User();
            while (read.Read())
            {
                cr = ReadCurrentRow(read);
            }
            read.Close();
            return cr;
        }

        private static User ReadCurrentRow(SqlDataReader currentRow)
        {

            User cr = new User();
            cr.Id = (int)currentRow["Id"];
            cr.UserName = currentRow["UserName"].ToString();
            cr.LastName = currentRow["LastName"].ToString();
            cr.FirstName = currentRow["FirstName"].ToString();
            cr.Password = currentRow["Password"].ToString();
            cr.DateBirth = (DateTime)currentRow["DateBirth"];
            cr.Category = (UserCategoryTypes)Enum.Parse(typeof(UserCategoryTypes), currentRow["Category"].ToString());
            cr.CreateDate = (DateTime)currentRow["CreateDate"];
            cr.City = currentRow["City"].ToString();
            cr.Adress = currentRow["Adress"].ToString();
            cr.Phone = currentRow["Phone"].ToString();
            cr.Email = currentRow["Email"].ToString();
            return cr;
        }
        public User GetUserByUserNameAndPassword(string name, string pass)
        {
            var tot = GetAll();
            return tot.FirstOrDefault(x => (x.UserName == name && x.Password == pass));
        }


        public User  FindByUserName(string userName)
        {//userName este gasit FindName return true
            var tot = GetAll();
            var name = tot.FirstOrDefault(x => (x.UserName == userName));
            return name;

        }

       
    }
}


