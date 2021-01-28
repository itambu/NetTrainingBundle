using INetShop.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication17.Presenters
{
    public class ItemPresenter
    {
        private TextWriter writer;

        public ItemPresenter(TextWriter writer)
        {
            this.writer = writer;
        }
        
        public void Show(Item item)
        {
            writer.Write(String.Format("id:{0} name:{1} ", item.Id, item.Name));
        }
    }
}
