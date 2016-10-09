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
            //EFModelDB cntx = new EFModelDB();
            //var orders = cntx.Orders.ToList();
            //foreach (var item in orders)
            //{
            //    Console.WriteLine(item.Id+"    "+ item.IdProduct + "  "+ item.Product.Name);
                
            //}

            foreach (var item in DBStore.GetOrderItems())
            {
                Console.WriteLine(item.Id + "    " + item.IdProduct + "  " + item.Quantity + " "+ item.TotalPrice );

            }
            Console.WriteLine(new string('*',40));
            foreach (var item in DBStore.GetProductItems())
            {
                Console.WriteLine(item.Id + "    " + item.Name + "  " + item.Price);

            }
            Console.WriteLine(new string('*', 40));
            foreach (var item in DBStore.OrderAllInfo())
            {
                Console.WriteLine(item.IdOrder + "    " + item.ProductName + "  " + item.Count+ " "+ item.TotalPrice);

            }
            Console.WriteLine(new string('*', 40));
            Console.WriteLine($"Product: Name = {DBStore.GetProductById().Name}");
            Console.WriteLine(new string('*', 40));
            Console.WriteLine($"Order: TotalPrice = {DBStore.GetOrderById().TotalPrice}");
            Console.WriteLine(new string('*', 40));
            Console.WriteLine($"Add new Product");
            DBStore.AddNewProduct("Super Bike",134.45);
            Console.WriteLine(new string('*', 40));
            Console.WriteLine("New Order with PrId =2, QNT = 10");
            DBStore.AddNewOrder(5,1);
            Console.WriteLine(new string('*', 40));
            Console.WriteLine("Update Product with Id 5: Name Boss, Price 125.35");
            DBStore.UpdateProductById(5, "Boss", 125.35);
            Console.WriteLine(new string('*', 40));
            Console.WriteLine("Update Order with Id 3: IdProduct 10, Quantity 35");
            DBStore.UpdateOrderById(3, 10, 35);



            Console.ReadKey();
        }
    }

    public static class DBStore
    {
        public static List<Order> GetOrderItems()
        {
            List<Order> result = null;
            using (var context = new EFModelDB())
            {
                result = context.Orders.ToList();
            }
            return result;
        }
        public static List<Product> GetProductItems()
        {
            List<Product> result = null;
            using (var context = new EFModelDB())
            {
                result = context.Products.ToList();
            }
            return result;
        }
        public static IEnumerable<dynamic> OrderAllInfo()
        {
            IEnumerable<dynamic> result = null;
            using (var context = new EFModelDB())
            {
                var query = from order in context.Orders
                            select new
                            {
                                IdOrder = order.Id,
                                ProductName = order.Product.Name,
                                Count = order.Quantity,
                                TotalPrice = order.TotalPrice
                            };
                result = query.ToList();
            }
            return result;
        }
        public static Product GetProductById(int _ID=1)
        {
            Product result = null;
            using (var context = new EFModelDB())
            {
                result = context.Products.Find(_ID);
            }
            return result;
        }
        public static Order GetOrderById(int _ID = 1)
        {
            Order result = null;
            using (var context = new EFModelDB())
            {
                result = context.Orders.Find(_ID);
            }
            return result;
        }
        public static void AddNewProduct(string _NAME, double _PRICE)
        {
            using (var context = new EFModelDB())
            {
                Product newProduct = new Product
                {
                    Name = _NAME
                    ,Price = new decimal(_PRICE)
                };
                context.Products.Add(newProduct);
                context.SaveChanges();
            }
        }
        public static void AddNewOrder(int _IdProduct,int _QNT)
        {
            using (var context = new EFModelDB())
            {

                Product selected = context.Products.Find(_IdProduct);
                if (selected == null) throw new Exception("Product was not foud!!! Invalid Id"); 
                Order newOrder = new Order
                {
                    IdProduct = _IdProduct
                    ,Quantity = _QNT
                    ,TotalPrice = _QNT * selected.Price
                };
                context.Orders.Add(newOrder);
                context.SaveChanges();
            }
        }
        public static void UpdateProductById(int _idProduct,string _newName,double _newPrice)
        {
            using (var context = new EFModelDB())
            {
                Product selected = context.Products.Find(_idProduct);
                if (selected == null) throw new Exception("Product was not foud!!! Invalid Id");
                selected.Name = _newName;
                selected.Price = (decimal)_newPrice;
                context.SaveChanges();
            }
        }
        public static void UpdateOrderById(int _idOrder, int _idProduct, int _QNT)
        {
            using (var context = new EFModelDB())
            {
                Product product = context.Products.Find(_idProduct);
                if (product == null) throw new Exception("Invalid Id of Product !!!");

                Order selected = context.Orders.Find(_idOrder);
                if (selected == null) throw new Exception("Invalid Id of Order !!!");

                selected.IdProduct = product.Id;
                selected.Quantity = _QNT;
                selected.TotalPrice = _QNT * product.Price;

                context.SaveChanges();
            }
        }

    }
}
