using BlogISIT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlogISIT.ConsoleTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            BlogISIT.Entities.BlogModelContainer dc = new Entities.BlogModelContainer();

            //dc.Database.Log = Console.Out;

            //dc.Users.Add(new User() { Login = "Pupsik" });

            var user = dc.Users.FirstOrDefault(x => x.Login == "Pupsik");
            var entry =  dc.Entry<User>(user);
            if (user != null)
            {

            }

            dc.SaveChanges();
        }
    }
}
