﻿using System;
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

        public ViewResult List(int page)
        {
            //create view model
            MaterialsListViewModel viewModel = new MaterialsListViewModel();

            //determine start index of page
            int count = m_Repository.Count;
            int start_index = (page - 1) * m_PageSize;
            
            //load materials
            viewModel.Materials = m_Repository.Get(start_index, count);

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
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Material material)
        {
            return View();
        }

        [HttpGet]
        public ViewResult Delete(int material_id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int material_id)
        {
            return View();
        }
    }
}
