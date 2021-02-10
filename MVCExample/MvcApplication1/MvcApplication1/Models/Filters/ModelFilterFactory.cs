using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models.Filters
{
    public class ModelFilterFactory
    {
        private IDictionary<Type, Type> _items = new Dictionary<Type, Type>();
        public ModelFilter<ViewModel> CreateInstance<ViewModel>()
        {
            return Activator.CreateInstance(_items[typeof(ViewModel)]) as ModelFilter<ViewModel>;
        }

        public void Register<ViewModel, Filter>() where Filter : new()
        {
            _items.Add(typeof(ViewModel), typeof(Filter));
        }
    }
}