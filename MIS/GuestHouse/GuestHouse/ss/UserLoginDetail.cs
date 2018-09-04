using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestHouse
{
    static class UserLoginDetail
    {
        public static string position { get; set; }
        public static string empID { get; set; }
        public static string fName { get; internal set; }
        public static string lName { get; set; }

        public static bool isActive(string username)
        {
            bool isActive=false;
            try
            {
                dataCon.Con.Open();
                string sqlCmd = "select UserAcc.EmpID,FName,LName,Position,Active FROM UserAcc join Employee on UserAcc.EmpID = Employee.EmpID WHERE Username ='"+username+"';";
                SqlDataReader dr=dataCon.ExecuteQry(sqlCmd);
                while (dr.Read())
                {
                    empID = dr.GetString(0);
                    fName = dr.GetString(1);
                    lName = dr.GetString(2);
                    position = dr.GetString(3);
                    //System.Windows.Forms.MessageBox.Show(dr[4].ToString());
                    isActive = Convert.ToBoolean(dr[4]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            dataCon.Con.Close();
            return isActive;
        }
    }
}
