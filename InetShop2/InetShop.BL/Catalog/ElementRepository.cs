using INetShop.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INetShop.BL.Catalog
{
    public class ElementRepository
    {
        protected ICollection<IElement> Items { get; private set; }

        public ElementRepository(ICollection<IElement> items)
        {
            Items = items;
        }

        public void Add(IElement element)
        {
            ValidateCollection();

            if (element == null)
            {
                throw new ArgumentNullException("Element cannot be null");
            }

            if (Items.FirstOrDefault(x => x.Id == element.Id) == null)
            {
                Items.Add(element);
            }
            else
            {
                throw new Exception("Try to add the same item");
            }
        }

        public IElement GetElement(int id)
        {
            ValidateCollection();
            return Items.FirstOrDefault(x => x.Id == id);
        }

        protected void ValidateCollection()
        {
            if (Items == null)
            {
                throw new NullReferenceException("Items was initialized");
            }
        }

    }
}
