using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score_60
{
    class DataSqlClass : CoreDataClass
    {
        public DataSqlClass(string stringConn) 
            : base(stringConn)
        {
        }
        public void InsertUpdateCreateTables(string sqlStr)
        {
            try
            {
                var cmd = this.connect.CreateCommand();
                cmd.CommandText = sqlStr;
                this.connect.Open();
                int res = cmd.ExecuteNonQuery();
                Console.WriteLine($"{res} rows affected.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connect.Close();
            }
            
        }
        public void NewRowINOrder(int IdProduct,int Quantity)
        {

            try
            {
                double price = GetPriceProduct(IdProduct);
                var cmdin = this.connect.CreateCommand();
                cmdin.CommandText = @"INSERT INTO [dbo].[Order]
                                    (IdProduct,Quantity,TotalPrice) 
                                    VALUES
                                    (@IDP,@QNT,@TOTAL)";
                cmdin.Parameters.AddWithValue("IDP",IdProduct);
                cmdin.Parameters.AddWithValue("QNT",Quantity);
                cmdin.Parameters.AddWithValue("TOTAL",(price*Quantity));
                this.connect.Open();
                int res = cmdin.ExecuteNonQuery();
                Console.WriteLine($"{res} rows affected.");

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connect.Close();

            }
        }
        public void UpdateRowINOrder(int RowID, int IdProduct,int Quantity)
        {
            try
            {
                double price = GetPriceProduct(IdProduct);

                var cmd = this.connect.CreateCommand();
                cmd.CommandText = @"UPDATE [dbo].[ORDER] 
                                    SET IdProduct = @IDProduct, Quantity = @QNT, TotalPrice = @Total
                                    WHERE Id = @RowID";
                cmd.Parameters.AddWithValue("IDProduct", IdProduct);
                cmd.Parameters.AddWithValue("QNT", Quantity);
                cmd.Parameters.AddWithValue("Total", (price * Quantity));
                cmd.Parameters.AddWithValue("RowID",RowID);
                this.connect.Open();
                int res = cmd.ExecuteNonQuery();
                Console.WriteLine($"{res} rows affected.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            finally
            {
                
                this.connect.Close();
            }
        }
        private double GetPriceProduct(int IdProduct)
        {

            double price = 0;
            try
            {
                var cmdsel = this.connect.CreateCommand();
                cmdsel.CommandText = @"SELECT [Price] FROM Product
                                    WHERE Id = @IdProduct";
                cmdsel.Parameters.AddWithValue("IdProduct", IdProduct.ToString());
                this.connect.Open();
                price = Convert.ToDouble(cmdsel.ExecuteScalar());

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connect.Close();

            }
            return price;
        }
        
    }
}
