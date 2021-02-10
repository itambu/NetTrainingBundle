using MvcApplication1.Models;
using MvcApplication1.Models.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Infrastucture
{
    public class StorageRouter
    {
        protected class RouteItem
        {
            public Func<Type,Controller, dynamic> Getter;
            public Action<object, Controller> Setter;
        }

        private Controller _controller;

        private static IDictionary<Type, RouteItem> _router = new Dictionary<Type, RouteItem>();

        public StorageRouter(Controller controller)
        {
            _controller = controller;
        }

        public static void Register(Type objType, Func<Type, Controller, dynamic> getter, Action<object, Controller> setter)
        {
            _router.Add(objType,
                            new RouteItem()
                            {
                                Getter = getter,
                                Setter = setter
                            });
        }

        public dynamic GetObject<ViewModel>()
        {
            return _router[typeof(ViewModel)].Getter.Invoke(typeof(ViewModel), _controller);
        }

        public void SetObject<TYPE>(TYPE obj)
        {
            if (obj != null)
            {
                _router[obj.GetType()].Setter.Invoke(obj, _controller);
            }
            else
            {
                if (_router.ContainsKey(typeof(TYPE)))
                {
                    _router[typeof(TYPE)].Setter.Invoke(obj, _controller);
                }
            }
        }

    }
}