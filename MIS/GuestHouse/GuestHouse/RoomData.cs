using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GuestHouse
{
    public partial class RoomData : Form
    {
        public RoomData()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string sql = "";
        SqlDataReader dataReader;
        List<Room> rooms = new List<Room>();
        Dictionary<int, string> RoomType = new Dictionary<int, string>();
        bool error;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.ButtonText == "Cancel")
            {
                dataRoom.ClearSelection();
                btnAdd.ButtonText = "បញ្ចូល";
                ClearControls();
                return;
                
            }

            if (CheckIfControlsHasEmptyData(panel5))
            {
                MessageBox.Show("សូមបំពេញព័តមានអោយបានគ្រប់គ្រាន់","Please fill information",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

                

            string id = txtID.Text;
            string floor =CMFloor.Text;
            string roomType = GetRoomTypeDesc();
            



            int roomTypeID = RoomType.FirstOrDefault(x => x.Value == roomType).Key;

            
            
            sql = "insert into room values('" + id + "'," + roomTypeID + "," + floor + ",'Free');";
            error = false;
            dataCon.ExecuteActionQry(sql,ref error);
            if (!error)
            {
                MessageBox.Show("New room inserted","Inserted successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataRoom.DataSource = null;
                rooms.Add(new Room(id, floor, roomType, "Free"));
                dataRoom.DataSource = rooms;
                btnAdd_Click(null, null);
            }
            
        }

        string GetRoomTypeDesc()
        {
            foreach (RadioButton temp in groupBox9.Controls.OfType<RadioButton>())
            {
                if (temp.Checked)
                {
                    return temp.Text;
                }
            }
            return "";
        }

        bool CheckIfControlsHasEmptyData(Panel p)
        {
            foreach(Control temp in p.Controls.OfType<Control>())
            {
                if (temp.Text == "")
                    return true;
            }

            return false;
        }

        private void RoomData_Load(object sender, EventArgs e)
        {
            sql = "select * from RoomType;";
            dataCon.Con.Open();
            dataReader = dataCon.ExecuteQry(sql);
            while (dataReader.Read())
            {
                RoomType.Add(dataReader.GetInt32(0), dataReader.GetString(1));
            }
            dataCon.Con.Close();


            sql = "select RoomID as លេខកូដសម្គាល់,RoomTypeID as ប្រភេទបន្ទប់,Floor as ជាន់,Status as ស្ថានភាព from room;";
            

            dataCon.Con.Open();
            dataReader = dataCon.ExecuteQry(sql);
            
            if (dataReader.HasRows)
            {
                dataRoom.Columns.Clear();
            }

            while (dataReader.Read())
            {
                string id = dataReader.GetString(0);
                string roomType=RoomType[dataReader.GetInt32(1)];

                
                string floor = dataReader.GetInt32(2)+"";
                string status = dataReader.GetString(3);

                rooms.Add(new Room(id, floor,roomType, status));

            }
            dataCon.Con.Close();
            dataRoom.DataSource = rooms;
            if(btnAdd.Text=="Cancel")
                btnAdd_Click(null, null);

        }

        void ClearControls()
        {
            txtID.Text = "";
            CMFloor.SelectedIndex = -1;
            rndSingle.Checked = true;
        }

        private void dataRoom_SelectionChanged(object sender, EventArgs e)
        {
            if (dataRoom.SelectedRows.Count == 0)
                return;
            if (dataRoom.SelectedRows.Count > 1)
            {
                ClearControls();
                return;
            }

            int selectedIndex = dataRoom.SelectedRows[0].Index;
            txtID.Text = rooms[selectedIndex].ID;
            CMFloor.SelectedItem = rooms[selectedIndex].Floor+"";

            switch (rooms[selectedIndex].RoomType)
            {
                case "Single":
                    rndSingle.Checked = true;
                    break;
                case "Double":
                    rndDouble.Checked = true;
                    break;
                case "VIP":
                    rndVip.Checked = true;
                    break;
            }

            txtID.Text = rooms[dataRoom.SelectedRows[0].Index].ID;

            btnAdd.ButtonText = "Cancel";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataRoom.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select a row", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int selectedIndex = dataRoom.SelectedRows[0].Index;
            string roomID = rooms[selectedIndex].ID;
            string floor = CMFloor.Text;
            string roomTypeDesc = GetRoomTypeDesc();
            int roomTypeID = RoomType.FirstOrDefault(x => x.Value == roomTypeDesc).Key;

            sql = "update Room set roomID='" + txtID.Text + "',roomTypeID="+roomTypeID+",floor="+floor+" where roomID='" + roomID + "'";

            error = false;
            dataCon.ExecuteActionQry(sql,ref error);

            if (!error)
            {
                MessageBox.Show("A row has edited", "Successfully edited", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataRoom.DataSource = null;
                rooms[selectedIndex].ID = txtID.Text;
                rooms[selectedIndex].Floor = floor;
                rooms[selectedIndex].RoomType = roomTypeDesc;
                dataRoom.DataSource = rooms;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<int> selectedIndexToDelete = new List<int>();
            sql = "delete from room where roomID in(";
            for (int i = 0; i < dataRoom.SelectedRows.Count; i++)
            {
                int index = dataRoom.SelectedRows[i].Index;
                sql += "'" + rooms[index].ID + "',";
                selectedIndexToDelete.Add(dataRoom.SelectedRows[i].Index);
            }

            sql = sql.Substring(0, sql.Length - 1) + ")";

            dataCon.ExecuteActionQry(sql,ref error);
            if (!error)
            {
                dataRoom.DataSource = null;
                while (selectedIndexToDelete.Count > 0)
                {
                    int lastIndexOfSelectedIndexToDelete = selectedIndexToDelete.Count - 1;
                    rooms.RemoveAt(selectedIndexToDelete[lastIndexOfSelectedIndexToDelete]);
                    selectedIndexToDelete.RemoveAt(lastIndexOfSelectedIndexToDelete);
                }
                dataRoom.DataSource = rooms;
                MessageBox.Show("Selected rows has been deleted successfully", "Successfully deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnAdd_Click(null, null);
            }
            

        }
    }
}
