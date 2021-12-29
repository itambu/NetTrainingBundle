using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistanceService
{
    public interface ITransformation<Source, Target>
    {
        Target Transform(Source source);
    }

    //public interface ITransformation<Source1,Source2, Target>
    //{
    //    Target Transform(Source1 source1, Source2 source2);
    //}
}
