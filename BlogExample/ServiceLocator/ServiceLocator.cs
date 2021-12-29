using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLocator
{
    public class ServiceLocator : IServiceLocator
    {
        private IDictionary<Type, object> Items { get; set; }

        public ServiceLocator(IDictionary<Type, object> items)
        {
            this.Items = items;
        }

        public TKey Get<TKey>()
        {
            return (TKey)Items[typeof(TKey)];
        }

        public ServiceLocator Register<TSource>(object item)
        {
            Items.Add(typeof(TSource), item);
            return this;
        }
    }
}
