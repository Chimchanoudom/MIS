using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestHouse.ss
{
    class CustomerDetail
    {
        public static Dictionary<string, string[]> cusDetail { get; set; }
        public static void getData()
        {
            cusDetail = new Dictionary<string, string[]>();
            
            try
            {   
                dataCon.Con.Open();
                string sqlCmd = "SELECT * FROM Customer;";
                SqlDataReader dr = dataCon.ExecuteQry(sqlCmd);
                while (dr.Read())
                {
                    string[] individualCusDetail = {dr["Fname"].ToString(), dr["Lname"].ToString(), dr["Gender"].ToString(), dr["IDNum"].ToString(), dr["Phone"].ToString()};
                    cusDetail.Add(dr["CusID"].ToString(), individualCusDetail);
                }
            }
            catch (Exception) { }
            finally { dataCon.Con.Close(); }
        }
    }
}
