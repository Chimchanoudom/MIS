using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GuestHouse
{
    class Dom_SqlClass:UserLoginDetail
    {
      static  SqlCommand SC = new SqlCommand();
       static SqlCommandBuilder SCB = new SqlCommandBuilder();
        
        public static void AddValue()
        {

        }
        public static void retriveData(String TableName,DataGridView Data)
        {
            try
            {
                dataCon.Con.Open();
                SC = new SqlCommand("Select * from "+TableName+";", dataCon.Con);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void Edite()
        {

        }
        public static void Delete()
        {

        }
        public static void Search()
        {

        }
    }
}
