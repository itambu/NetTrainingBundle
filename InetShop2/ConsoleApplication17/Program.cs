using INetShop.BL.Catalog;
using INetShop.BL.Unity;
using INetShop.Entities;
using INetShop.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.IO;
using System.Collections;

namespace ConsoleApplication17
{
    class CustomCollection : List<int>
    {
        internal IEnumerable<int> Backwards()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return this[i];
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {

            IEnumerable<int> list = (new CustomCollection() { 1, 2, 5, 6, 7 }).Backwards();

            foreach (int item in list)
            {
                Console.WriteLine(item);
            }

            //IEnumerator<int> iterator = list.GetEnumerator();
            //while (iterator.MoveNext())
            //{
            //    Console.WriteLine(iterator.Current);
            //}

            //for (int i = 0; i < list.Length; i++)
            //{
            //    Console.WriteLine(list[0]);
            //}
            
            
            Unity unity = new Unity(new ElementRepository(new List<IElement>()));

            unity.ElementRepository.Add( new Item(1, "Грабли") { Description = "с длинной шипастой ручкой" });
            unity.ElementRepository.Add( new Item(1, "Вилы") { Description = "ну очень острые" });

            
            

            //-----------------------------------------------
            //List<BasketItem> itemCollection = new List<BasketItem>()
            //{
            //    new BasketItem(items[0]) { Amount = 1, CostPerItem = 10 },
            //    new BasketItem(items[1]) { Amount = 2, CostPerItem = 14 }
            //};
            ////-----------------------------------------------


            //List<Basket> basketCollection = new List<Basket>()
            //{
            //    new RegularBasket(itemCollection), new DiscountBasket5Percentage(itemCollection)
            //};

            //NewMethod(basketCollection);


            ////Console.WriteLine("1:{0} 2:{1}", basket.TotalCost, 
            ////    ((DiscountBasket5Percentage)disBasket).TotalCost);

            ////DiscountBasket5Percentage db = disBasket as DiscountBasket5Percentage;
            ////if (db != null)
            ////{
                
            ////}


            //// ------------------------------------------------
 
            //basket.Items.Add( new BasketItem(items[0]) { Amount = 1, CostPerItem = 10 });
            //basket.Items.Add( new BasketItem(items[1]) { Amount = 2, CostPerItem = 14 });

            //Order order = new Order(basket, DateTime.Now);
            //Order order1 = new Order(disBasket, DateTime.Now);



            //System.IO.StreamWriter writer = new System.IO.StreamWriter(@"d:\m.txt");
            //Presenters.ItemPresenter ipresenter =
            //    new Presenters.ItemPresenter(writer);
           
            //ipresenter.Show(items[0]);
            //writer.Close();
        }

        //private static void NewMethod(List<Basket> basketCollection)
        //{
        //    foreach (RegularBasket item in basketCollection)
        //    {
        //        Console.WriteLine("TC:{0}", item.TotalCost);
        //    }
        //}


    }
}
