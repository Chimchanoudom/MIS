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
        
        Dictionary<int, string> RoomType = new Dictionary<int, string>();
        bool error;
        DataTable dtRoom=new DataTable();
        bool CancelSelection = false;

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
            
            dataCon.ExecuteActionQry(sql,ref error);
            if (!error)
            {
                dtRoom.Rows.Add(id, floor, roomType, "Free");
                MessageBox.Show("New room inserted","Inserted successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
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


            sql = "select RoomID as លេខកូដសម្គាល់,Floor as ជាន់,case RoomTypeID when 1 then 'Single'when 2 then 'Double'when 3 then 'VIP'end as ប្រភេទបន្ទប់,Status as ស្ថានភាព from room;";

            dataRoom.Columns.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(sql, dataCon.Con);
            adapter.Fill(dtRoom);
            dataRoom.DataSource = dtRoom;

            btnAdd.ButtonText = "បញ្ចូល";
            dataRoom.ClearSelection();


        }

        void ClearControls()
        {
            txtID.Text = "";
            CMFloor.SelectedIndex = -1;
            rndSingle.Checked = true;
            
        }

        private void dataRoom_SelectionChanged(object sender, EventArgs e)
        {
            if (dataRoom.SelectedRows.Count != 1)
            {
                ClearControls();
                return;
            }
            if (CancelSelection)
            {
                return;
            }

            int selectedIndex = dataRoom.SelectedRows[0].Index;

            txtID.Text = dataRoom.Rows[selectedIndex].Cells[0].Value + "";
            CMFloor.SelectedItem = dataRoom.Rows[selectedIndex].Cells[1].Value + "";

            switch (dataRoom.Rows[selectedIndex].Cells[2].Value + "")
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
            string roomID =txtID.Text;
            string floor = CMFloor.Text;
            string roomTypeDesc = GetRoomTypeDesc();
            int roomTypeID = RoomType.FirstOrDefault(x => x.Value == roomTypeDesc).Key;

            sql = "update Room set roomID='" + roomID + "',roomTypeID=" + roomTypeID + ",floor=" + floor + " where roomID='" + dataRoom.Rows[selectedIndex].Cells[0].Value + "" + "'";

            error = false;
            dataCon.ExecuteActionQry(sql,ref error);

            if (!error)
            {
                dtRoom.Rows[selectedIndex][0] = roomID;
                dtRoom.Rows[selectedIndex][1] = floor;
                dtRoom.Rows[selectedIndex][2] = roomTypeDesc;
                MessageBox.Show("A row has edited", "Successfully edited", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dataRoom.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to delete selected row(s)?", "Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (dialog == DialogResult.No)
                return;

            string roomToCheck = "";
            for(int i = 0; i < dataRoom.SelectedRows.Count; i++)
            {
                int selectedIndex = dataRoom.SelectedRows[i].Index;
                roomToCheck += "'"+dataRoom.Rows[selectedIndex].Cells[0].Value + "',";
            }

            roomToCheck = roomToCheck.Substring(0, roomToCheck.Length - 1);

            sql = "select count(*) from bookDetail where roomId in(" + roomToCheck + ") and status!='Checked Out';";

            dataCon.Con.Open();
            dataReader = dataCon.ExecuteQry(sql);
            dataReader.Read();
            if (dataReader.GetInt32(0) > 0)
            {
                MessageBox.Show("Some rooms have been booked or busy", "Cannot delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            dataCon.Con.Close();

            sql = "delete from room where roomid in(" + roomToCheck + ")" ;
            dataCon.ExecuteActionQry(sql,ref error);
            if (!error)
            {
                CancelSelection = true;
                int selectedRowCount = dataRoom.SelectedRows.Count;
                for(int i = 0; i < selectedRowCount; i++)
                {
                   
                    int selectedIndex = dataRoom.SelectedRows[0].Index;
                    dtRoom.Rows.RemoveAt(selectedIndex);
                    
                }

                dataRoom.ClearSelection();
                btnAdd.ButtonText = "បញ្ចូល";
                CancelSelection = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filter = "";
            if (rndSearchRoomNum.Checked) filter = "លេខកូដសម្គាល់=";
            else if (rndSearchRoomType.Checked) filter = "ប្រភេទបន្ទប់=";
            else if (rndSearchFloor.Checked) filter = "ជាន់=";
            else filter = "ស្ថានភាព=";

            filter += "'" + txtSearch.Text + "'";

            dtRoom.DefaultView.RowFilter = filter;
            btnCancelSearch.Visible = true;
            dataRoom.ClearSelection();
        }

        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            dtRoom.DefaultView.RowFilter = string.Empty;
            btnCancelSearch.Visible = false;
            dataRoom.ClearSelection();
        }
    }
}
