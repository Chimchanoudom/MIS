using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestHouse.ss
{
    class EmpClass
    {
        public static List<string> dataTableHeader { get; set; }


        public static void AddDataToDB()
        {

        }




        class GetData
        {
            static Dictionary<string, Dictionary<string, string>> empData = new Dictionary<string, Dictionary<string, string>>();
      

            public static void getAllEmployeeData()
            {
                try
                {
                    dataCon.Con.Open();
                    string sqlCmd = "";
                    sqlCmd = "SELECT Employee.EmpID,DateEmployed,FName,LName,Gender,DOB,Phone,Address,Position,Salary,UserAcc.Username,Password,Active FROM UserAcc JOIN Employee ON UserAcc.EmpID=Employee.EmpID;";
                    SqlDataReader dr = dataCon.ExecuteQry(sqlCmd);
                    while (dr.Read())
                    {
                        Dictionary<string, string> rowOfEmpData = new Dictionary<string, string>();
                        rowOfEmpData.Add("EmpID",dr["EmpID"].ToString());
                        rowOfEmpData.Add("DateEmployed", dr["DateEmployed"].ToString());
                        rowOfEmpData.Add("FirstName", dr["FName"].ToString());
                        rowOfEmpData.Add("LastName", dr["LName"].ToString());
                        rowOfEmpData.Add("Gender", dr["Gender"].ToString());
                        rowOfEmpData.Add("DOB", dr["DOB"].ToString());
                        rowOfEmpData.Add("Phone", dr["Phone"].ToString());
                        rowOfEmpData.Add("Address",dr["Address"].ToString());
                        rowOfEmpData.Add("Position",dr["Position"].ToString());
                        rowOfEmpData.Add("Salary",dr["Salary"].ToString());
                        rowOfEmpData.Add("Username",dr["Username"].ToString());
                        rowOfEmpData.Add("Password",dr["Password"].ToString());
                        rowOfEmpData.Add("Active",dr["Active"].ToString());
                        empData.Add("EmpID", rowOfEmpData);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                dataCon.Con.Close();
            }
        }
    }
}
