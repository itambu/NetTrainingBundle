using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcApplication1.Models.Filters
{
    public class CatFilter : ModelFilter<Cat>
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name= "Action")]
        public TextFieldCriteria NameCriteria{ get;set;}

        public override Expression<Func<Cat,bool>> ToExpression()
        {
            Expression<Func<Cat, bool>> result = null;
            if (Name != null && Name != "")
            {
                switch (NameCriteria)
                {
                    case TextFieldCriteria.Contains : 
                        result = (cat) => cat.Name.Contains(Name);
                        break;
                    case TextFieldCriteria.Ends:
                        result = (cat) => cat.Name.EndsWith(Name);
                        break;
                    case TextFieldCriteria.Starts:
                        result = (cat) => cat.Name.StartsWith(Name);
                        break;
                }
            }
            return result;  
        }
    }
}