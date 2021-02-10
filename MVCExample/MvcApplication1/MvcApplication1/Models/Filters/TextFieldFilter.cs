using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models.Filters
{
    public class TextFieldFilter
    {
        public string TextField { get; set; }
        public TextFieldCriteria Criteria { get; set; }
    }
}