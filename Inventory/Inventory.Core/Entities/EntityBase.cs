using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Core.Entities
{
    public abstract class EntityBase<TId>
    {
        public virtual TId Id { get; set; }
        public virtual int Version { get; set; }
        public virtual User User { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as EntityBase<TId>);
        }

        private static bool IsTransient(EntityBase<TId> obj)
        {
            return obj != null && Equals(obj.Id, default(TId));
        }

        private Type GetUnProxiedType()
        {
            return GetType();
        }

        protected virtual bool Equals(EntityBase<TId> other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals(other, this))
                return true;
            if (!IsTransient(this) && !IsTransient(other) && Equals(Id, other.Id))
            {
                var otherType = other.GetUnProxiedType();
                var thisType = GetUnProxiedType();
                return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(TId)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }
    }
    public abstract class EntityBase : EntityBase<int>
    {
    }
}
