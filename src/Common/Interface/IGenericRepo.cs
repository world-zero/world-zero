using System.Collections.Generic;

namespace WorldZero.Common.Interface
{
    public interface IGenericRepo<ClassType, IdType>
        where ClassType : class
    {
        IEnumerable<ClassType> GetAll();
        ClassType GetById(IdType id);
        void Delete(IdType id);
        void Insert(ClassType obj);
        void Update(ClassType obj);
        void Save();
    }
}