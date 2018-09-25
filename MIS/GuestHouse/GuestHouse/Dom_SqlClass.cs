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
        static DataTable DT = new DataTable();
        public static string GetIDFromDB(String column,string seperater,String TableName)
        {
            object ID="";
            try
            {
                dataCon.Con.Open();
                SC = new SqlCommand();
                SC.CommandText = @"getAutoID  " + " '" + column + "'," + " '" + seperater + "'," + " '" + TableName + "'";
                SC.Connection = dataCon.Con;
                ID=SC.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
               MessageBox.Show(e.Message);
            }
            finally
            {
                dataCon.Con.Close();
            }
            if (ID==null||ID.Equals(""))
            {
                ID ="";
            }
            else
            {
                int num = int.Parse(ID+"") -1;
                ID = num + "";
            }
            return ID+"";
           
        }
        public static void UpdateDate(DataTable Datatable)
        {
            DT=new DataTable();
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
        public static DataTable retriveData(String TableName,String Condition,String[]ColumnName)
        {
             DT = new DataTable();
            try
            {
                String Select = "SELECT ";
                for (int i = 0; i < ColumnName.Length; i++)
                {
                    Select += ColumnName[i] + ",";
                }
                Select = Select.TrimEnd(',') + " From ";
                //MessageBox.Show(Select);
                dataCon.Con.Open();
                SC = new SqlCommand(Select+TableName+" "+Condition+" ;", dataCon.Con);
                SDA = new SqlDataAdapter(SC);
                SCB = new SqlCommandBuilder(SDA);
                SDA.Fill(DT);
               
            }
            catch (Exception e )
            {
                if (DT.Rows.Count <= 0)
                    MessageBox.Show("NO Data "+ e.Message);
            }
            finally
            {
                dataCon.Con.Close();
            }
            return DT;
        }
        public static Boolean InsertData(String TableName,object[]Value)
        {
            bool insert = false;
            try
            {
                String commandInsert = @"INSERT INTO "+TableName+" ";
                String Values = " Values(";
                for (int i = 0; i < Value.Length; i++)
                {
                    Values += "N'" + Value[i] + "'COLLATE Latin1_General_100_CI_AI,";
                }
                Values = Values.Substring(0, Values.Length - 1) + ");";
                string sqlCmd = commandInsert+ Values;
                dataCon.Con.Open();
                SC = new SqlCommand(sqlCmd, dataCon.Con);
                SC.ExecuteNonQuery();
                insert = true;
                commandInsert = Values = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                dataCon.Con.Close();
            }
            return insert;
        }
        public static void  FillItemToCombobox(String StatemenSelect ,String Valuemember,String DisplayMember,ComboBox cm)
        {
            try
            {
                dataCon.Con.Open();
                SC = new SqlCommand(StatemenSelect, dataCon.Con);
                SqlDataReader SDR = SC.ExecuteReader();
                while (SDR.Read())
                {
                    cm.Items.Add(SDR[DisplayMember].ToString());
                    cm.ValueMember = SDR[Valuemember].ToString();
                    cm.DisplayMember = SDR[DisplayMember].ToString();
                }
                SDR.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                dataCon.Con.Close();
            }
        }
        public static Boolean Edit(object Edit)
        {
            bool edit = false;
            try
            {
                dataCon.Con.Open();
                SC = new SqlCommand(Edit.ToString(), dataCon.Con);
                SC.ExecuteNonQuery();
                edit = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                dataCon.Con.Close();
            }
            return edit;
        }
        public static Boolean Delete(object Delete)
        {
            bool delete = false;
            try
            {
                dataCon.Con.Open();
                SC = new SqlCommand(Delete.ToString(), dataCon.Con);
                SC.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return delete;
        }
        public static DataTable retriveDataMultiTable(String SelectStatement)
        {
            try
            {
                dataCon.Con.Open();
                SC = new SqlCommand(SelectStatement, dataCon.Con);
                 SDA = new SqlDataAdapter(SC);
                SCB = new SqlCommandBuilder(SDA);
                SDA.Fill(DT);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                dataCon.Con.Close();
            }
            return DT;
        }
        public static bool SQLMultiTable(String[]Statement)
        {
            bool success = false;
            dataCon.Con.Open();
            SqlTransaction tran = dataCon.Con.BeginTransaction();
            try
            {

                for (int i = 0; i < Statement.Length; i++)
                {
                    SC = new SqlCommand(Statement[i], dataCon.Con, tran);
                    SC.ExecuteNonQuery();
                }
                tran.Commit();
                success = true;
            }
            catch (SqlException e)
            {
                success = false;
                MessageBox.Show(e.Message);
                tran.Rollback();
            }
            finally
            {
                dataCon.Con.Close();
            }
            return success;
        }
        public static DataTable Search(String Select)
        {
            dataCon.Con.Open();
            SC = new SqlCommand(Select, dataCon.Con);
            SDA = new SqlDataAdapter(SC);
            SCB = new SqlCommandBuilder(SDA);
            SDA.Fill(DT);
            return DT;
        }
    }
}
