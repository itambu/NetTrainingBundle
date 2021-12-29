using PersistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Integration.SessionPersistance
{
    public interface IParametrizedFactory<Instance, Parameter>
    {
        Instance CreateInstance(Parameter parameter);
    }
}