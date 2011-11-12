using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventory.Core.Entities;

namespace Inventory.WebUI.Infrastructure
{
    public interface IUserService
    {
        User GetCurrentUser();
    }
}
