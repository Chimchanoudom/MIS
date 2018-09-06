﻿using System;
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
            public static void addDataToDB(string TableName, Dictionary<string, string> columnNameAndDataValues)
            {
                string cmdInsert = "INSERT INTO " + TableName + " ";
                string columns = "(";
                string values = " VALUES (";

                int countIndexOfDic = 0;
                foreach (string columnName in columnNameAndDataValues.Keys)
                {
                    columns += columnName + ",";
                    values += "'"+columnNameAndDataValues[columnName] + "',";
                    countIndexOfDic++;
                }
                columns = columns.Substring(0, columns.Length - 1) + ")";
                values = values.Substring(0, values.Length - 1) + ")";

                string sqlCmd =cmdInsert+ columns + values + ";";
                //MessageBox.Show(sqlCmd);
                bool error = false;
                dataCon.ExecuteActionQry(sqlCmd, ref error);
            }

            public static void updateDataToDB(string TableName, Dictionary<string, string> columnNameAndDataValues,string condition="")
            {
                string cmdUpdate = "update " + TableName + " SET ";
                string Operation = "";

                int countIndexOfDic = 0;
                foreach (string columnName in columnNameAndDataValues.Keys)
                {
                    Operation += columnName + "='"+columnNameAndDataValues[columnName] + "',";
                    countIndexOfDic++;
                }
                Operation = Operation.Substring(0, Operation.Length - 1) + " ";
                condition = (condition == String.Empty) ? ";" : ((condition[condition.Length - 1]).ToString() == ";") ? condition : condition + ";";

                string sqlCmd = cmdUpdate + Operation + condition;
                //MessageBox.Show(sqlCmd);
                bool error = false;
                dataCon.ExecuteActionQry(sqlCmd, ref error);
            }

            public static void deleteDataFromDB(string TableName,string condition="")
            {
                string cmdDelete = "DELETE FROM " + TableName + " ";

                condition = (condition == String.Empty) ? "WHERE 1=1;" : ((condition[condition.Length - 1]).ToString() == ";") ? condition : condition + ";";

                string sqlCmd = cmdDelete+ condition;
                //MessageBox.Show(sqlCmd);
                bool error = false;
                dataCon.ExecuteActionQry(sqlCmd, ref error);
            }
        }
    }

}
