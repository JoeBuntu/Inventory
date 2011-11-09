using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventory.Core.Entities;

namespace Inventory.Core.Data
{
    public interface IMaterialsRepository
    {
        int Count { get; }
        Material Get(int id);
        List<Material> Get(int start_index, int count); 
        int Add(Material material); 
        void Update(Material material);
        void Delete(int id);
        void Delete(Material material);
    }
}
