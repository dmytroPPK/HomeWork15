using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score_60
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SqlConnectionStringBuilder cBuilder = new SqlConnectionStringBuilder();
            cBuilder.DataSource = @"(local)\TERRASOFTNAME";
            cBuilder.InitialCatalog = "HomeWork15";
            cBuilder.UserID = "HW15";
            cBuilder.Password = "1111";

            // DataSqlClass
            {
                DataSqlClass dsql = new DataSqlClass(cBuilder.ToString());
                dsql.NewRowINOrder(2, 10);
                dsql.UpdateRowINOrder(1, 1, 15);

                string sql = @"INSERT INTO [dbo].[Product] (Name,Price)
                                VALUES
                               ('Laptop2', 25000)
                              ,('SuperToy2',1000)";
                dsql.InsertUpdateCreateTables(sql);
            }
            Console.WriteLine(new string('_',30));
            
            // DataStoreClass
            {
                DataStoreClass dstr = new DataStoreClass(cBuilder.ToString());
                dstr.FillDataSet();
                Console.WriteLine("\n---- TABLE : Product\n");
                dstr.ShowProducts();

                Console.WriteLine("\n---- TABLE : Order\n");
                dstr.ShowOrders();

                dstr.FillResultTable();
                Console.WriteLine("\n---- TABLE : ResultTable\n");
                dstr.ShowResultTable();

                Console.WriteLine("\n---- TABLE : ResultById\n");
                dstr.GetResultById(10);
            }
            
            Console.ReadKey();

        }
    }
}
