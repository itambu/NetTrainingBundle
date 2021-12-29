using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PersistanceService
{
    public class PropertyBinder<Entity, ViewModel, BindProperty>
    {
        public Func<ViewModel, BindProperty> ViewModelBindProperty { get; private set; }
        public Func<IPrincipal, BindProperty> PrincipalBindProperty { get; private set; }
        public Expression<Func<Entity, BindProperty>> EntityBindProperty { get; private set; }

        public PropertyBinder(
            Expression<Func<Entity, BindProperty>> entityBindProperty,
            Func<ViewModel, BindProperty> viewModelBindProperty,
            Func<IPrincipal, BindProperty> principalBindProperty
            )
        {
            EntityBindProperty = entityBindProperty;
            ViewModelBindProperty = viewModelBindProperty;
            PrincipalBindProperty = principalBindProperty;
        }

        public Expression<Func<Entity, bool>> CreateQuery(BindProperty principalBindValue)
        {
            var t = Expression.Equal(EntityBindProperty, Expression.Constant(principalBindValue, typeof(BindProperty)));
            Expression<Func<Entity, bool>> query = LambdaExpression.Lambda<Func<Entity, bool>>(
                t, EntityBindProperty.Parameters);
            return query;
        }
    }
}
