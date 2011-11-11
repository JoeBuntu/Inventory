using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventory.Core.Entities;

namespace Inventory.Core.Data
{
    public interface IRepository<T> where T : EntityBase
    {
        int Count { get; }
        T Get(int id);
        List<T> Get(int start_index, int count);
        IList<T> Get(int start_index, int count, string sort_column, bool sort_asc);
        int Add(T item);
        void Update(T item);
        void Delete(int id);
        void Delete(T item);
    }
}
