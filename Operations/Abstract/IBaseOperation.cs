using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS_OOP_Practice_V1.Operations.Abstract
{
    public interface IBaseOperation<T> where T : class,new()
    {
        IResult Add(T item);
        IResult Remove(T item);
        IResult Update(T item);
        IDataResult<T> Get(int id);
        IDataResult<List<T>> GetAll();
        IDataResult<int> GetNextId();
    }


}
