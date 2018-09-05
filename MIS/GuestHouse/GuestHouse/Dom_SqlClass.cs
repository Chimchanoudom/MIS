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
        static DataTable DT;

        public static string GetIDFromDB(String column,string seperater,String TableName)
        {
            object ID="";
            try
            {
                dataCon.Con.Open();
                SC = new SqlCommand();
                SC.CommandText = @"getAutoID  " + " '" + column + "'," + " '" + seperater + "'," + " '" + TableName + "'";
                SC.Connection = dataCon.Con;
                ID=SC.ExecuteScalar();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                dataCon.Con.Close();
            }
            if (ID==null)
            {
                int num = int.Parse(ID + "")+1 ;
                ID = num + "";
            }
            return ID+"";
        }
        public static void UpdateDate(DataTable Datatable)
        {
            DT = Datatable;
            try
            {
                SDA.Update(DT);
                MessageBox.Show("Update SuccessFully !");
            }
            catch (Exception e)
            {
                MessageBox.Show("Update Fails !" + e.Message);
            }
        }
        public static DataTable retriveData(String TableName,DataGridView Data)
        {
             DT = new DataTable();
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
