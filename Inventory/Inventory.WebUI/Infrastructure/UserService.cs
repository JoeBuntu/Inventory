using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory.Core.Entities;

namespace Inventory.WebUI.Infrastructure
{
    public class UserService : IUserService
    {
        public User GetCurrentUser()
        {
            return new User() { Id = 2, Name = "grussell", Version = 1 };
        }
    }
}