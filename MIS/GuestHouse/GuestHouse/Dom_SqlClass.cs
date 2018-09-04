using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace GuestHouse
{
    class Dom_SqlClass:UserLoginDetail
    {
      static  SqlCommand SC = new SqlCommand();
       static SqlCommandBuilder SCB = new SqlCommandBuilder();
        static SqlDataAdapter SDA = new SqlDataAdapter();

        
        public static void AddValue()
        {

        }
        public static DataTable retriveData(String TableName,DataGridView Data)
        {
            DataTable DT = new DataTable();
            try
            {
                dataCon.Con.Open();
                SC = new SqlCommand("Select * from "+TableName+";", dataCon.Con);
                SDA = new SqlDataAdapter(SC);
                SCB = new SqlCommandBuilder(SDA);
                SDA.Fill(DT);
               
            }
            catch (Exception e )
            {
                if (Data.RowCount <= 0)
                    MessageBox.Show("NO Data "+ e.Message);
            }
            finally
            {
                dataCon.Con.Close();
            }
            return DT;
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
