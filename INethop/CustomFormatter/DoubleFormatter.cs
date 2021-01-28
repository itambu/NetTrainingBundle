using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomFormatter
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, 
        AllowMultiple = false, Inherited = false)]
    public class DoubleFormatterAttribute : Attribute
    {
        public bool DivideByTriades { get; set; } = true;
        public Char Divider { get; set; } = '\'';

        //public DoubleFormatterAttribute(char divider)
        //{
        //    Divider = divider;
        //}
    }

    public class Formatter<T>
    {
        public string Format(Expression<Func<T, string>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
