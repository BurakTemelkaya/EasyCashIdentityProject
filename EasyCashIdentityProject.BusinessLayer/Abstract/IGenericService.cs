using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class, new()
    {
        void TInsert(T t);

        void TDelete(T t);

        void TUpdate(T t);

        T TGetById(int id);

        List<T> TGetAll();
    }
}
