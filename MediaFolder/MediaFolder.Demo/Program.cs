using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MediaFolder.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 3;

            ArrayList list = new ArrayList();
            list.Add(4);
            list.Add(4.7);
            list.Add("dfdghfgdhf");

            i = (int)list[0];

            List<int> l2 = new List<int>() { 4 };

        }
    }
}
