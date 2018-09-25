﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

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

            public static void updateDataToDB(string TableName, Dictionary<string, string> columnNameAndDataValues, string condition = "")
            {
                string cmdUpdate = "update " + TableName + " SET ";
                string Operation = "";
                foreach (string columnName in columnNameAndDataValues.Keys)
                {
                    Operation += columnName + "=N'" + columnNameAndDataValues[columnName] + "'COLLATE Latin1_General_100_CI_AI,";
                }
                Operation = Operation.Substring(0, Operation.Length - 1) + " ";
                condition = (condition == String.Empty) ? ";" : ((condition[condition.Length - 1]).ToString() == ";") ? condition : condition + ";";

                string sqlCmd = cmdUpdate + Operation + condition;
                //MessageBox.Show(sqlCmd);
                bool error = false;
                dataCon.ExecuteActionQry(sqlCmd, ref error);
                MessageBox.Show("Successfully UPDATED!");
            }

            public static void CollateData(ref List<string> data)
            {
                List<string> temp = new List<string>();
                foreach (string st in data)
                {
                    temp.Add("N'" + st + "' COLLATE Latin1_General_100_CI_AI");
                }

                data = temp;
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

            

        }

        static Dictionary<string, DataTable> Price = new Dictionary<string, DataTable>();

        public static void GetPrice()
        {
            string sql = "select roomTypeDesc,HourType,RoomPrice,Fan,ac from roomType r join price p on r.roomTypeID=p.roomTypeID;";

            con.Open();
            SqlDataReader dataReader = ExecuteQry(sql);
            while (dataReader.Read())
            {
                String roomTypeDesc = dataReader.GetString(0);

                if (Price.Keys.Contains(roomTypeDesc))
                {
                    Price[roomTypeDesc].Rows.Add(dataReader.GetInt32(1), Convert.ToDouble(dataReader.GetValue(2)), Convert.ToDouble(dataReader.GetValue(3)), Convert.ToDouble(dataReader.GetValue(4)));
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("HourType", typeof(int));
                    dt.Columns.Add("RoomPrice", typeof(double));
                    dt.Columns.Add("Fan", typeof(double));
                    dt.Columns.Add("AC", typeof(double));

                    dt.Rows.Add(dataReader.GetInt32(1),Convert.ToDouble(dataReader.GetValue(2)), Convert.ToDouble(dataReader.GetValue(3)), Convert.ToDouble(dataReader.GetValue(4)));

                    Price.Add(roomTypeDesc, dt);
                }
            }

            con.Close();
        }

        static bool CalculatePrice(DateTime dtStart, DateTime dtEnd,string roomTypeDesc, ref double roomPrice, bool pickAc, ref double electricity, ref double subTotal)
        {
            TimeSpan dif = dtEnd.Date - dtStart.Date;

            if (dif.Hours < 0)
                return false;

            double overRoomPrice = 0, overElectricity = 0, overSubTotal = 0;

            if (dif.Hours > 24)
            {
                int overHour;
                
                overHour = dif.Hours % 24;

                GetSubTotal(overHour, roomTypeDesc,ref  overRoomPrice, pickAc,ref overElectricity, ref overSubTotal,1);

                int multiply = dif.Hours / 24;

                GetSubTotal(24, roomTypeDesc, ref overRoomPrice, pickAc, ref overElectricity, ref overSubTotal, multiply);
            }
            else
            {
                GetSubTotal(dif.Hours, roomTypeDesc, ref roomPrice, pickAc, ref electricity, ref subTotal,1);
            }

            

            roomPrice += overRoomPrice;
            electricity += overElectricity;
            subTotal += subTotal;

            return true;

        }

        static void GetSubTotal(int hour,string roomTypeDesc, ref double roomPrice,bool pickAc, ref double electricity, ref double subTotal,int multiply)
        {

            if (hour <= 3)
            {
                hour = 3;
            }else if(hour <= 6)
            {
                hour = 6;
            }else if (hour <= 12)
            {
                hour = 12;
            }else if (hour <= 24)
            {
                hour = 24;
            }


            DataRow[] dr = Price[roomTypeDesc].Select("HourType="+hour);

            roomPrice = (double)dr[0][1]*multiply;

            if(!pickAc)
                electricity = (double)dr[0][2];
            else
                electricity = (double)dr[0][3];

            subTotal = roomPrice + electricity;

        }
    }

}
