using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Inventory.Core.Entities;
using Inventory.Core.Data.Exceptions;

namespace Inventory.Core.Data
{
    public class MaterialsRepository : Repository<Material>, IMaterialsRepository
    {
        public MaterialsRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {           
        }

        public Material Get(string part_number)
        {
            return Transact<Material>(() => Session.Query<Material>().SingleOrDefault(x => x.PartNumber == part_number));
        }

        protected override void OnAddException(Material item, Exception ex)
        {
            //possible issues:
            //  1. Duplicate part number

            Material current_item = Get(item.PartNumber);
            if (current_item != null)
            {
                string message = string.Format("The new material could not be created, Part Number '{0}' already exists", 
                                               current_item.PartNumber);
                RepositoryInsertException exception = new RepositoryInsertException(message, ex);
                exception.OldItem = item;
                exception.NewItem = current_item;
                exception.ErrorType = RepositoryInsertException.Error.Duplicate;
                throw exception;
            }

            // all other exceptions
            throw new RepositoryInsertException("Unknown error occured while creating material", ex);
        }

        protected override void OnUpdateException(Material item, Exception ex)
        {
            //possible issues:
            //  1. Another user has updated.
            //  2. Item was deleted

            Material current_item = Get(item.PartNumber);
            if (current_item != null && current_item.Version > item.Version) // #1
            {
                string message = string.Format("The material could not be updated, Part Number '{0}' has been updated by another user.",
                               current_item.PartNumber);
                RepositoryUpdateException exception = new RepositoryUpdateException(message, ex);
                exception.OldItem = item;
                exception.NewItem = current_item;                
                exception.ErrorType = RepositoryUpdateException.Error.Version;
                throw exception;
            }
            else if (current_item == null) // #2
            {
                string message = string.Format("The material could not be updated, Part Number '{0}' no longer exists.",
                                               item.PartNumber);
                RepositoryUpdateException exception = new RepositoryUpdateException(message, ex);
                exception.OldItem = item;
                exception.NewItem = current_item;
                exception.ErrorType = RepositoryUpdateException.Error.Deleted;
                throw exception;
            }

            //all other exceptions
            RepositoryUpdateException o = new RepositoryUpdateException("Unknown error occurred while updating material", ex);           
        }

        protected override void OnDeleteException(Material item, Exception ex)
        {
            //possible issues:
            //  1. Another user has updated. - let these pass
            //  2. Another user has deleted. - let these pass
            //  3. Material has dependencies

            Material current_item = Get(item.PartNumber);
            if (current_item != null && current_item.Version > item.Version) // #1
            {                
                //do nothing
            }
            else if (current_item == null) // #2
            {
                //do nothing
            }
            else if (current_item != null) // #3
            {
                string message = string.Format("Other entities have dependencies on Part Number '{0}'. Delete not allowed",
                                               item.PartNumber);
                RepositoryDeleteException exception = new RepositoryDeleteException(message, ex);
                exception.OldItem = item;
                exception.NewItem = current_item;
                exception.ErrorType = RepositoryDeleteException.Error.Dependencies;
                throw exception;
            }
        }
    }
}
