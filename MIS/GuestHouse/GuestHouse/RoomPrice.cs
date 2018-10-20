using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuestHouse
{
    public partial class RoomPrice : Form
    {
        public RoomPrice()
        {
            InitializeComponent();
        }

        SqlDataReader dataReader;
        bool update;
        private void RoomPrice_Load(object sender, EventArgs e)
        {

            GetDataFromRoomPrice();

            sql = "select RoomTypeDesc from roomType";
            dataCon.Con.Open();
            dataReader = dataCon.ExecuteQry(sql);

            while (dataReader.Read())
            {
                cmRoomType.Items.Add(dataReader.GetString(0));
            }
            dataCon.Con.Close();

            sql = "select distinct HourType from price;";
            dataCon.Con.Open();
            dataReader = dataCon.ExecuteQry(sql);

            while (dataReader.Read())
            {
                cmHourType.Items.Add(dataReader.GetInt32(0));
            }
            dataCon.Con.Close();

        }
        string sql;
        private void btnback_Click(object sender, EventArgs e)
        {
            if (update) dataCon.GetPrice();
            this.Close();
        }



        void GetDataFromRoomPrice()
        {
            dgvRoomPrice.Rows.Clear();
            sql = "select rt.RoomTypeDesc,HourType,RoomPrice,Fan,AC from price p join RoomType rt on p.RoomTypeID=rt.RoomTypeID;";

            dataCon.Con.Open();
            dataReader = dataCon.ExecuteQry(sql);

            while (dataReader.Read())
            {
                dgvRoomPrice.Rows.Add(dataReader.GetString(0), dataReader.GetInt32(1), dataReader.GetValue(2) + "", dataReader.GetValue(3) + "", dataReader.GetValue(4) + "");
            }
            dataCon.Con.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (cmHourType.SelectedIndex == -1 && cmRoomType.SelectedIndex == -1)
            {
                MessageBox.Show("សូមបំពេញព័តមានអោយបានគ្រប់គ្រាន់", "Please fill information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            

            sql = "Update Price set ";

            if (txtRoomPrice.Text != "") sql += "RoomPrice=" + txtRoomPrice.Text + ",";
            if (txtFan.Text != "") sql += "Fan=" + txtFan.Text + ",";
            if (txtAC.Text != "") sql += "AC=" + txtAC.Text + ",";


            int roomTypeID = cmRoomType.SelectedIndex + 1;

            sql = sql.Substring(0, sql.Length - 1) + " where RoomTypeID=" + roomTypeID + " and HourType=" + cmHourType.Text;

            bool error = false;
            dataCon.ExecuteActionQry(sql,ref error);
            if (!error)
            {
                
                MessageBox.Show("Update Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetDataFromRoomPrice();
                update = true;
            }
        }

        private void cmRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
                txtFan.Enabled = cmRoomType.Text.ToUpper() != "VIP";
        }
    }
}
