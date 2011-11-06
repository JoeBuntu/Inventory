using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Core.Data;
using Inventory.Core.Entities;
using Inventory.WebUI.Models;

namespace Inventory.WebUI.Controllers
{
    public class MaterialsController : Controller
    {
        private MaterialsRepository m_Repository;
        private int m_PageSize = 25;

        public MaterialsController(MaterialsRepository repository)
        {
            m_Repository = repository;
        }

        public ViewResult List(int page)
        {
            MaterialsListViewModel viewModel = new MaterialsListViewModel();             
            return View(viewModel);
        }

    }
}
