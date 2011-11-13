using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Core.Data;
using Inventory.Core.Entities;
using Inventory.WebUI.Infrastructure;

namespace Inventory.WebUI.Controllers
{
    public class BOMsController : Controller
    {
        private readonly IBOMsRepository m_Repository;
        private readonly IUserService m_UserService;
        private readonly int m_PageSize = 25;

        public BOMsController(IBOMsRepository repository, UserService user_service)
        {
            m_Repository = repository;
            m_UserService = user_service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<BOM> boms = m_Repository.Get(0, m_PageSize);
            return View(boms);
        }

    }
}
