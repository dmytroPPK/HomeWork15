using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score_60
{
    class DataStoreClass : CoreDataClass
    {
        protected DataSet ds;
        protected DataTable resultTable;
        public DataStoreClass(string stringConn) 
            : base(stringConn)
        {
            ds = new DataSet();
        }
        public void FillDataSet()
        {
            string sqlCmd = @"Select * From [dbo].[Product]; Select * From [dbo].[Order]";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd,this.connect);
            this.connect.Open();
            adapter.Fill(this.ds);
            this.connect.Close();
        }
        public void ShowProducts()
        {
            DataTable product = ds.Tables[0];
            
            Console.WriteLine($"{product.Columns[0].ColumnName,-5}{product.Columns[1].ColumnName,-10}{product.Columns[2].ColumnName,-15}");

            foreach (DataRow row in product.Rows)
            {
                Console.WriteLine($"{row["Id"],-5}{row["Name"],-10}{row["Price"],-15}");
            }
        }
        public void ShowOrders()
        {
            DataTable order = ds.Tables[1];
            
            Console.WriteLine($"{order.Columns[0].ColumnName,-5}{order.Columns[1].ColumnName,-10}{order.Columns[2].ColumnName,-10}{order.Columns[3].ColumnName,-15}");

            foreach (DataRow row in order.Rows)
            {
                Console.WriteLine($"{row["Id"],-5}{row["IdProduct"],-10}{row["Quantity"],-10}{row["TotalPrice"],-15}");
            }
        }

        // Получение данных в 1 запрос
        public void FillResultTable()
        {
            this.resultTable = new DataTable("ResultTable");

            string sqlCmd = @"Select [Order].Id,[Product].Name,[Order].TotalPrice 
                                From   [dbo].[Order]
                                inner join [dbo].[Product]
                              ON [Product].Id = [Order].IdProduct";

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd, this.connect);
            this.connect.Open();
            adapter.Fill(this.resultTable);
            this.connect.Close();
        }
        public void ShowResultTable()
        {
            DataTable tb = this.resultTable;
            Console.WriteLine($"{tb.Columns[0].ColumnName,-5}{tb.Columns[1].ColumnName,-10}{tb.Columns[2].ColumnName,-15}");

            foreach (DataRow row in tb.Rows)
            {
                Console.WriteLine($"{row["Id"],-5}{row["Name"],-10}{row["TotalPrice"],-15}");
            }
        }
        public void GetResultById(int index)
        {
            DataRow rowResult;
            var query = from row in this.resultTable.AsEnumerable()
                        where (int)row["Id"] == index
                        select row;
            var list = query.ToList();
            if(list.Count > 0)
            {
                rowResult = list[0];
                string msg = $"Search index - {index} returned ->\nId = {rowResult["Id"]}\nName = {rowResult["Name"]}\nTotalPrice = {rowResult["TotalPrice"]}";
                Console.WriteLine(msg);
            }else
            {
                Console.WriteLine($"Row with entered index - '{index}' isn't contained in ResultTable");
            }
        }

    }
}
