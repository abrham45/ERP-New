using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        ICollection<T> FindAll();
        T FindById(int id);
        bool isExists(int id);
        bool Create(T entitiy);
        bool Update(T entitiy);
        bool Delete(T entitiy);
        bool Save();
    }
}
