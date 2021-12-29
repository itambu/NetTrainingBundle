using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExample.MvcClient.Models
{
    public class AvatarViewModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string LoginIndentifier { get; set; }
    }
}