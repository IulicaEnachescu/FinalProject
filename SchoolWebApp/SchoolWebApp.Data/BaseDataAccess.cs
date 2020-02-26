using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDBModel;
using SchoolWebApp.Services;

namespace SchoolWebApp.Data
{
    public abstract class BaseDataAccess<T> : IBaseRepository<T> where T : SchoolDBModel.EntityTypes.EntityBase
    {
        protected virtual string TableName { get; }
        protected virtual string UpdateCommand { get; }
        protected virtual string AddCommand { get; }
        protected abstract T ReadRow(SqlDataReader read);
        protected abstract IList<T> ReadReader(SqlDataReader read);
        protected abstract SqlParameter[] ReturnSqlParamAdd(T entity);
        protected abstract SqlParameter[] ReturnSqlParamUpdate(T entity);
        protected abstract T CompleteEntity(int id, T entity);

        public int Add(T entity)
        {
            string commandText = $"Insert into{TableName} {ColumnInsertDefinition()} values {AddCommand}; select scope_identity();";
            SqlParameter[] param = ReturnSqlParamAdd(entity);
            var nr = Convert.ToInt32(SqlHelper.ExecuteScalar(commandText, param));
           
            return nr;
        }

        public virtual string ColumnInsertDefinition()
        {
            return "";
        }


        //ok select item by Id

        public int Update(int id, T cou)

        {
            T entity = CompleteEntity(id, cou);
            //update command
            string commandText = $"Update {TableName} SET {UpdateCommand} where [Id]=@Id";
            
            SqlParameter[] param1 = ReturnSqlParamUpdate(entity);
            SqlParameter parameterId = new SqlParameter("Id", SqlDbType.Int);
            parameterId.Value = id;
            int j = param1.Length;
            SqlParameter[] param = new SqlParameter[j + 1];
            for (int i = 0; i < j; i++)
            {
                param[i] = param1[i];
            }

            param[j] = parameterId;
           
            SqlHelper.ExecuteNonQuery(commandText, param);
            return entity.Id;
        }

        public int Save(T entity)
        {

            int rez;
            if (entity.Id == 0)
                rez = Add(entity);
            else
                rez = Update(entity.Id, entity);

            return rez;
        }
        //ok delete item
        public void Delete(T entity)
        {
            if (entity.Id == 0)
            {
                return;
            }
            string commandText = $"DELETE FROM {TableName} WHERE [Id] = @Id";
            SqlParameter parameterId = new SqlParameter("Id", SqlDbType.Int);
            parameterId.Value = entity.Id;
            SqlParameter[] param = new SqlParameter[1] { parameterId };
            SqlHelper.ExecuteNonQuery(commandText, param);
         
        }


        //get all to list-ok
        public IList<T> GetAll()
        {
            string commandText = $"Select * from {TableName}";
            SqlDataReader rows = SqlHelper.ExecuteReader(commandText);
            var lista = ReadReader(rows);
            Console.WriteLine(lista);
            return lista;
        }
        public T GetById(int id)
        {
            string commandText = $"SELECT * FROM {TableName} WHERE [Id] = @Id";
            SqlParameter parameterId = new SqlParameter("Id", SqlDbType.Int);
            parameterId.Value = id;
            SqlParameter[] param = new SqlParameter[1] { parameterId };
            SqlDataReader row = SqlHelper.ExecuteReader(commandText, param);
            return ReadRow(row);
        }

    }
}
