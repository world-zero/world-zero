using System.Collections.Generic;

namespace WorldZero.Common.Interface
{
    public interface IGenericRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Delete(object id);
        void Insert(T obj);
        void Update(T obj);
        void Save();
    }
}