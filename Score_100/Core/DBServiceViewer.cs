using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score_100
{
    public static class DBServiceViewer
    {
        static DBServiceViewer()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ShowOrders()
        {
            StrColorHelper("\t--Orders List--\n", ConsoleColor.Cyan);
            foreach (var item in DBStore.GetOrderItems())
            {
                string msg = $"ID: {item.Id,-3} IdProduct: {item.IdProduct,-3} Count: {item.Quantity,-5} TotalPrice: {item.TotalPrice,-10}";
                Console.WriteLine(msg);
            }
        }
        public static void ShowProducts()
        {
            StrColorHelper("\t--Products List--\n", ConsoleColor.Cyan);
            foreach (var item in DBStore.GetProductItems())
            {
                string msg = $"ID: {item.Id,-3} Name: {item.Name,-18} Price: {item.Price,-10}";
                Console.WriteLine(msg);
            }
        }
        public static void ShowFullOrders()
        {
            StrColorHelper("\t--Orders List: Full Info--\n", ConsoleColor.Cyan);
            foreach (var item in DBStore.OrderAllInfo())
            {
                string msg = $"ID: {item.IdOrder,-3} ProductName: {item.ProductName,-18} Count: {item.Count,-5} TotalPrice: {item.TotalPrice,-10}";
                Console.WriteLine(msg);
            }
        }
        public static void ProductById()
        {
            StrColorHelper("\t--Product By ID--\n", ConsoleColor.Cyan);
            try
            {
                Console.Write("Please enter the id of product: ");
                int id = Int32.Parse(Console.ReadLine());
                Product pr = DBStore.GetProductById(id);
                if (pr == null) throw new Exception("Invalid id product !!!");

                string msg = $"ID: {pr.Id,-3} Name: {pr.Name,-15} Price: {pr.Price,-6}";
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ProductById();
            }
        }
        public static void OrderById()
        {
            Console.WriteLine();
            StrColorHelper("\t--Order By ID--\n", ConsoleColor.Cyan);
            try
            {
                Console.Write("Please enter the id of order: ");
                int id = Int32.Parse(Console.ReadLine());
                Order or = DBStore.GetOrderById(id);
                if (or == null) throw new Exception("Invalid id order !!!");

                string msg = $"ID: {or.Id,-3} IdOfProduct: {or.IdProduct,-3} Count: {or.Quantity,-5} TotalPrice: {or.TotalPrice,-6}";
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ProductById();
            }
        }
        public static void NewProduct()
        {
            StrColorHelper("\t--Add New Product--\n", ConsoleColor.Cyan);
            try
            {
                Console.Write("Please enter name of product: ");
                string name = Console.ReadLine();
                if (name.Trim() == string.Empty) throw new Exception("Error. Empty Name!!!");

                Console.Write("Please enter price of product: ");
                double price = Double.Parse(Console.ReadLine());
                DBStore.AddNewProduct(name, price);
                Console.WriteLine(" ---> Product Added!!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                NewProduct();
            }
        }
        public static void NewOrder()
        {
            StrColorHelper("\t--Add New Order--\n", ConsoleColor.Cyan);
            try
            {
                Console.Write("Please enter ID of product: ");
                int idProduct = Int32.Parse(Console.ReadLine());

                Console.Write("Please enter quantity of product: ");
                int qnt = Int32.Parse(Console.ReadLine());
                DBStore.AddNewOrder(idProduct, qnt);
                Console.WriteLine(" ---> Order Added!!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                NewOrder();
            }
        }
        public static void UpdateProduct()
        {
            StrColorHelper("\t--Update product by ID--\n", ConsoleColor.Cyan);
            try
            {
                Console.Write("Please enter ID of product: ");
                int idProduct = Int32.Parse(Console.ReadLine());

                Console.Write("Please enter price of product: ");
                double price = Double.Parse(Console.ReadLine());

                Console.Write("Please enter new name of product: ");
                string name = Console.ReadLine();
                if (name.Trim() == string.Empty) throw new Exception("Error. Empty Name!!!");

                DBStore.UpdateProductById(idProduct, name, price);

                Console.WriteLine(" ---> Product Updated !!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UpdateProduct();
            }
        }
        public static void UpdateOrder()
        {
            StrColorHelper("\t--Update Order by ID--\n", ConsoleColor.Cyan);
            try
            {
                Console.Write("Please enter ID of Order: ");
                int idOrder = Int32.Parse(Console.ReadLine());

                Console.Write("Please enter id of product: ");
                int idProduct = Int32.Parse(Console.ReadLine());

                Console.Write("Please enter quantity of Order: ");
                int qnt = Int32.Parse(Console.ReadLine());

                DBStore.UpdateOrderById(idOrder, idProduct, qnt);

                Console.WriteLine(" ---> Order Updated !!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UpdateProduct();
            }
        }
        private static void StrColorHelper(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }


    }
}
