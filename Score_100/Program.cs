using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score_100
{
    class Program
    {
        static void Main(string[] args)
        {
            DBServiceViewer.ProductById();
            Console.ReadKey();

            DBServiceViewer.OrderById();
            Console.ReadKey();

            DBServiceViewer.NewProduct();
            Console.ReadKey();

            DBServiceViewer.NewOrder();
            Console.ReadKey();

            DBServiceViewer.UpdateOrder();
            Console.ReadKey();

            DBServiceViewer.UpdateProduct();
            Console.ReadKey();

            DBServiceViewer.ShowFullOrders();
            Console.ReadKey();

            DBServiceViewer.ShowOrders();
            Console.ReadKey();

            DBServiceViewer.ShowProducts();
            Console.ReadKey();
        }
    }
}
