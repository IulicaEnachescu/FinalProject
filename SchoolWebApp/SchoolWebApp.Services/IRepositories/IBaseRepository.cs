using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services
{
    public interface IBaseRepository <T> where T:EntityBase
    {
        int Add(T entity);
        int Update(int id, T entity);
        void Delete(T entity);
        int Save(T entity); 
        IList<T> GetAll();
        T GetById(int id);

    }
}
