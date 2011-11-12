using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Core.Data;
using Inventory.Core.Data.Exceptions;
using Inventory.Core.Entities;
using Inventory.WebUI.Infrastructure;
using Inventory.WebUI.Models;

namespace Inventory.WebUI.Controllers
{
    public class MaterialsController : Controller
    {
        private IMaterialsRepository m_Repository;
        private IUserService m_UserService;
        private int m_PageSize = 25;

        public MaterialsController(IMaterialsRepository repository, UserService user_service)
        {
            m_Repository = repository;
            m_UserService = user_service;
        }

        [HttpGet]
        public ViewResult List(int page = 1, string sort_col = "PartNumber", bool sort_asc = true)
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
            viewModel.Materials = m_Repository.Get(start_index, m_PageSize, sort_col, sort_asc);

            //populate paging info
            viewModel.PagingInfo = new PagingInfoViewModel()
            {
                 CurrentPage = page,
                 ItemsPerPage = m_PageSize,
                 TotalItems = count                 
            };

            //populate sorting info
            viewModel.SortingInfo = new SortingInfoViewModel()
            {
                Column = sort_col,
                IsAscending = sort_asc
            };
            viewModel.PagingInfo.RouteValues = viewModel.SortingInfo.GetRouteValues();
            viewModel.SortingInfo.RouteValues = viewModel.PagingInfo.GetRouteValues();
            
            // return result with view model
            return View(viewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View(new Material() );
        }

        [HttpPost]
        public ActionResult Create(Material material)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    material.User = m_UserService.GetCurrentUser();
                    m_Repository.Add(material);
                    TempData["confirmation_message"] = string.Format("Material '{0}' - '{1}' has been successfully created",
                                                                     material.PartNumber, material.Description);
                    return RedirectToAction("List", new { page = 1 });
                }
                catch (RepositoryInsertException ex)
                {
                    TempData["error"] = ex.Message;
                    return View(material);
                }
            }
            else //invalid model
            {
                return View(material);
            }
        }

        [HttpGet]
        public ViewResult Edit(int material_id)
        {
            //display material editor
            Material m = m_Repository.Get(material_id);          
            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(Material material)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    material.User = m_UserService.GetCurrentUser();
                    m_Repository.Update(material);
                    TempData["confirmation_message"] = string.Format("Material '{0}' - '{1}' has been successfully updated",
                                                                     material.PartNumber, material.Description);
                    return RedirectToAction("List", new { page = 1 });
                }
                catch (RepositoryUpdateException ex)
                {
                    TempData["error"] = ex.Message;
                    return View(material);
                }
            }
            else // invalid model
            {
                return View(material);
            }
        }

        [HttpGet]
        public ViewResult Delete(int material_id)
        {
            //display delete confirmation
            Material material = m_Repository.Get(material_id);
            return View(material);
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int material_id)
        {
            //get material
            Material material = m_Repository.Get(material_id);
            material.User = m_UserService.GetCurrentUser();
            try
            {                
                m_Repository.Delete(material);

                //set confirmation message
                TempData["confirmation_message"] = string.Format("Material '{0}' - '{1}' has been successfully deleted",
                                                                 material.PartNumber, material.Description);
                return RedirectToAction("List", new { page = 1 });
            }
            catch (RepositoryDeleteException ex)
            {
                TempData["error"] = ex.Message;
                return View("Delete", material);
            }
        }
    }
}
