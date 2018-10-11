using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestHouse.ss
{
    class getRoomDetail
    {
        
        public static List<string> getFreeRoom(DateTime dateIn,DateTime dateOut,int roomType)
        {
            List<string> allRoom = new List<string>();
            List<string> freeRoom = new List<string>();
            List<string> result = new List<string>();
            try
            {
                dataCon.Con.Open();
                string sqlCmd = "getFreeRoom '"+dateIn+"','"+dateOut+"','';";
                SqlDataReader sqlDR = dataCon.ExecuteQry(sqlCmd);
                while (sqlDR.Read())
                {
                    freeRoom.Add(sqlDR.GetString(0));
                }
                sqlDR.Close();

                sqlCmd = @"SELECT RoomID
                        FROM Room
                        WHERE Room.RoomTypeID = "+roomType+"; ";
                sqlDR = dataCon.ExecuteQry(sqlCmd);
                while (sqlDR.Read())
                {
                    allRoom.Add(sqlDR.GetString(0).ToString());
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                dataCon.Con.Close();
            }

            
            foreach(string tem in freeRoom)
            {
                foreach(string a in allRoom)
                    if (tem == a)
                        result.Add(tem);
            }

            return result;
        }
    }
}
