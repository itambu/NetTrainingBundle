using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Authorization
{
    public interface IAuthorizedUser
    {
        int Id { get; }
        string Nickname { get; }
        string LoginIndentifier { get; }
    }
}