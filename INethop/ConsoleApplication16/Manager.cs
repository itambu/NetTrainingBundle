using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16
{
    public class Manager : User
    {
        public string Department { get; set; }

                public Manager(string firstName, string lastName, Gender gender)
                    :  base(firstName, lastName, gender)
        {
        }
    }
}
