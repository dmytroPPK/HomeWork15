using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score_60
{
    public abstract class CoreDataClass
    {
        protected SqlConnection connect;
        public CoreDataClass(string stringConn)
        {
            this.connect = new SqlConnection(stringConn);

        }
    }
}
