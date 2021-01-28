using ConsoleApplication16.GenericRepository;
using ConsoleApplication16.Interfaces;
using ConsoleApplication16.Interfaces.ObservableList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication16
{
    class Program
    {
        static void Main(string[] args)
        {

            ObservableList<Order> orderCollection = new ObservableList<Order>();

            var orderRepo = new GenericRepository<Order>(orderCollection);  // ObservableList<Order>();
            
            // -------------------------------------------------------------
            
            
            // --------------------------------------------------------------
            
            Unity unity = new Unity(
                new List<IElement>()
                {
                    new Item(1, "Вилы", "очееень острые"),
                    new Item ( 2, "Грабли", "с длинной ручкой")
                },
                orderRepo,
                new List<User>()
                {
                    new User("вася", "Петров", Gender.Male),
                    new Manager("вася", "Петров", Gender.Male) { Department = "Охрана" }
                }
                );

            orderCollection.AddEvent += unity.OnOrderAdd;
            
            unity.Orders.Add(new Order( new RegularBasket(
                new List<ISaleItem>() 
                {
                    new SaleItem( unity.Items.ElementAt(0),  2, 3), 
                    new SaleItem( unity.Items.ElementAt(1),  3, 1)
                }), 
                DateTime.Now));

            unity.Orders.Add(new Order(
               new DiscountBasket(new List<ISaleItem>() 
                {
                    new SaleItem( unity.Items.ElementAt(0),  2, 3), 
                    new SaleItem( unity.Items.ElementAt(1),  3, 1)
                }),
                DateTime.Now));
            unity.Orders.Add(new Order(
               new BasketWithFreeItem(new List<ISaleItem>() 
                {
                    new SaleItem( unity.Items.ElementAt(0),  2, 3), 
                    new SaleItem( unity.Items.ElementAt(1),  3, 1)
                }),
                DateTime.Now));

            foreach (var order in unity.Orders.Get())
            {
                Console.WriteLine(order.Basket.Total);
            }
        }

    }
}
