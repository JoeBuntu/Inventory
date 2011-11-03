using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventory.Core.Entities;

namespace Inventory.Core.Data
{
    public interface IRepository<T> : IEnumerable<T> where T : EntityBase
    {
        void Add(T item);
        bool Contains(T item);
        int Count { get; }
        bool Remove(T item);
    }
}
