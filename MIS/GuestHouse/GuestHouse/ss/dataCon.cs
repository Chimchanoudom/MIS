 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GuestHouse
{
    public class dataCon
    {
        public static SqlConnection Con {
            get { return con; }
            set { con=value; }
        }

        static SqlConnection con=new SqlConnection("Server=localhost;Database=gh;Trusted_Connection=true;");

        public static string FormatDateTime(DateTime dt)
        {
            return dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year + " " + dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00");
        }

        public static SqlDataReader ExecuteQry(string qry)
        {
            SqlDataReader dataReader=null;
            SqlCommand sql = new SqlCommand(qry, con);
            dataReader = sql.ExecuteReader();
            return dataReader;
        }

        

        public static DateTime ConvertStringToDateTime(string st)
        {
            int day = int.Parse(st.Substring(0, 2));
            int month = int.Parse(st.Substring(3, 2));
            int year = int.Parse(st.Substring(6, 4));
            int hour = int.Parse(st.Substring(11, 2));
            int minute = int.Parse(st.Substring(14, 2));

            DateTime dt = new DateTime(year, month, day, hour, minute, 0);

            return dt;
        }

        public static void ExecuteActionQry(string qry,ref bool error)
        {
            error = false;
            SqlTransaction trans=null;
            try
            {
                con.Open();
                trans=con.BeginTransaction();          
                SqlCommand sql = new SqlCommand(qry, con, trans);
                sql.ExecuteNonQuery();
                trans.Commit();
                error = false;
            }catch(Exception ex)
            {
                trans.Rollback();
                MessageBox.Show(ex.Message);
                error = true;
            }
            finally
            {
                con.Close();
            }
        }

        public static class exActionQuery
        {
            public static void insertDataToDB(string TableName, Dictionary<string, string> columnNameAndDataValues)
            {
                ///Example: INSERT INTO Employee (ColumnName) VALUES (dataToInsert);
                string cmdInsert = "INSERT INTO " + TableName + " ";
                string columns = "(";
                string values = " VALUES (";
          
                foreach (string columnName in columnNameAndDataValues.Keys)
                {
                    columns += columnName + ",";
                    values += "N'"+columnNameAndDataValues[columnName] + "'COLLATE Latin1_General_100_CI_AI,";
                }
                columns = columns.Substring(0, columns.Length - 1) + ")";
                values = values.Substring(0, values.Length - 1) + ")";

                string sqlCmd =cmdInsert+ columns + values + ";";
                //MessageBox.Show(sqlCmd);
                bool error = false;
                dataCon.ExecuteActionQry(sqlCmd, ref error);
                MessageBox.Show("Successfully ADDED!");
            }

            public static void insertDataToDB(string TableName, string[] dataToInsert)
            {
                ///Example: INSERT INTO Employee VALUES (dataToInsert);
                string cmdInsert = "INSERT INTO " + TableName + " ";
                string values = " VALUES (";

                for(int i = 0; i < dataToInsert.Length; i++)
                {
                    values+= "N'"+dataToInsert[i]+ "'COLLATE Latin1_General_100_CI_AI,"; 
                }
                values = values.Substring(0, values.Length - 1) + ");";
                string sqlCmd = cmdInsert + values;
                bool error = false;
                dataCon.ExecuteActionQry(sqlCmd, ref error);
                MessageBox.Show("Successfully ADDED!");
            }

            public static void updateDataToDB(string TableName, Dictionary<string, string> columnNameAndDataValues,string condition="")
            {
                string cmdUpdate = "update " + TableName + " SET ";
                string Operation = "";

<<<<<<< HEAD
        public static void CollateData(ref List<string> data)
        {
            List<string> temp = new List<string>();
            foreach (string st in data)
            {
                temp.Add("N'" + st + "' COLLATE Latin1_General_100_CI_AI");
            }

            data = temp;
=======
                foreach (string columnName in columnNameAndDataValues.Keys)
                {
                    Operation += columnName + "=N'"+columnNameAndDataValues[columnName] + "'COLLATE Latin1_General_100_CI_AI,";
                }
                Operation = Operation.Substring(0, Operation.Length - 1) + " ";
                condition = (condition == String.Empty) ? ";" : ((condition[condition.Length - 1]).ToString() == ";") ? condition : condition + ";";

                string sqlCmd = cmdUpdate + Operation + condition;
                //MessageBox.Show(sqlCmd);
                bool error = false;
                dataCon.ExecuteActionQry(sqlCmd, ref error);
                MessageBox.Show("Successfully UPDATED!");
            }

            public static void deleteDataFromDB(string TableName,string condition="")
            {
                string cmdDelete = "DELETE FROM " + TableName + " ";

                condition = (condition == String.Empty) ? "WHERE 1=1;" : ((condition[condition.Length - 1]).ToString() == ";") ? condition : condition + ";";

                string sqlCmd = cmdDelete+ condition;
                //MessageBox.Show(sqlCmd);
                bool error = false;
                dataCon.ExecuteActionQry(sqlCmd, ref error);
                MessageBox.Show("Successfully DELETED!");
            }
>>>>>>> e7909f4493068a49520336c5d03503728a254f97
        }
    }

}
