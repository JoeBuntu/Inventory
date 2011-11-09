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
        private IMaterialsRepository m_Repository;
        private int m_PageSize = 25;

        public MaterialsController(IMaterialsRepository repository)
        {
            m_Repository = repository;
        }

        public ViewResult List(int page = 1)
        {
            //page should be >= 1
            if (page < 1)
            {
                page = 1;
            }

            //create view model
            MaterialsListViewModel viewModel = new MaterialsListViewModel();

            //determine start index of page
            int count = m_Repository.Count;
            int start_index = (page - 1) * m_PageSize;
            
            //load materials
            viewModel.Materials = m_Repository.Get(start_index, m_PageSize);

            //populate paging info
            viewModel.PagingInfo = new PagingInfo()
            {
                 CurrentPage = page,
                 ItemsPerPage = m_PageSize,
                 TotalItems = count                 
            };
            
            // return result with view model
            return View(viewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View("Edit", new Material());
        }

        [HttpGet]
        public ViewResult Edit(int material_id)
        {
            //get material
            Material m = m_Repository.Get(material_id);

            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(Material material)
        {
            if (ModelState.IsValid)
            {
                m_Repository.Update(material);

                return RedirectToAction("List", new { page = 1 });
            }
            else
            {
                throw new Exception();
            }
        }

        [HttpGet]
        public ViewResult Delete(int material_id)
        {
            return View("");
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int material_id)
        {
            return View();
        }
    }
}
