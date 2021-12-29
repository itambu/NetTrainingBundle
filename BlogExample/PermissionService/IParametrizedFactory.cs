using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersistanceService
{
    public interface IParametrizedFactory<Instance, Parameter>
    {
        Instance CreateInstance(Parameter parameter);
    }
}