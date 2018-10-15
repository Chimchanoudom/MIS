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
using GuestHouse.ss;

namespace GuestHouse
{
    public partial class Booking : Form
    {
        public Booking()
        {
            InitializeComponent();

        }

        string newCustomerID;

        public void selectedCustomer(bool status)
        {
            string id = txtCusID.Text;
            if (status)
            {
                RestrictionClass.restrictFromKeyboard.restrictAnyKeyFromKeyboard(txtTel);
                radMale.Enabled = (radMale.Checked);
                radFemale.Enabled = !radMale.Enabled;
            }
            else
            {
                RestrictionClass.restrictFromKeyboard.DisablerestrictAnyKeyFromKeyboard(txtTel);
                radMale.Enabled = radFemale.Enabled = true;
            }
        }

        public void refreshCustomers()
        {
            CustomerDetail.getData();
            foreach (string tem in CustomerDetail.cusDetail.Keys)
            {
                cbxCustName.Items.Add(CustomerDetail.cusDetail[tem][0] + " " + CustomerDetail.cusDetail[tem][1]);
            }
        }

        public static string getBookIdFromDB()
        {
            string id = "";
            try
            {
                dataCon.Con.Open();
                string sqlCmd = "GetAutoID BookID,'_',Book;";
                SqlDataReader dr = dataCon.ExecuteQry(sqlCmd);
                while (dr.Read())
                {
                    id = dr.GetInt32(0) + "";
                }
            }
            catch (Exception)
            { System.Windows.Forms.MessageBox.Show("Unable to perform the action!"); }
            dataCon.Con.Close();
            return (id.Length == 2) ? "B_0" + id : "B_00" + id;
        }

        public static string getCusIdFromDB()
        {
            string id = "";
            try
            {
                dataCon.Con.Open();
                string sqlCmd = "GetAutoID CusID,'_',Customer;";
                SqlDataReader dr = dataCon.ExecuteQry(sqlCmd);
                while (dr.Read())
                {
                    id = dr.GetInt32(0) + "";
                }
            }
            catch (Exception)
            { System.Windows.Forms.MessageBox.Show("Unable to perform the action!"); }
            dataCon.Con.Close();
            return (id.Length == 2) ? "Cus_0" + id : "Cus_00" + id;
        }

        public void getroom()
        {
            int roomType = radOneBed.Checked ? 1 : radTwoBed.Checked ? 2 : 3;
            List<string> room = getRoomDetail.getFreeRoom(new DateTime(dateIn.Value.Year, dateIn.Value.Month, dateIn.Value.Day, dateIn.Value.Hour, dateIn.Value.Minute, 0), new DateTime(dateOut.Value.Year, dateOut.Value.Month, dateOut.Value.Day, dateOut.Value.Hour, dateOut.Value.Minute, 0), roomType);
            cbxRoomNumber.Items.Clear();
            cbxCustName.DataSource = new ComboBox().DataSource;
            //cbxRoomNumber.Text = String.Empty;
            if(dvgDetail.SelectedRows.Count<1)
                for(int i = 0; i < dvgDetail.RowCount; i++)
                {
                    string bookedRoom = dvgDetail.Rows[i].Cells[5].Value!=null? dvgDetail.Rows[i].Cells[5].Value.ToString():"";
                    for(int j= room.Count-1; j>=0;j--)
                    {
                        if (room[j] == bookedRoom)
                            room.RemoveAt(j);
                    }
                }
            foreach (string tem in room)
            {
                cbxRoomNumber.Items.Add(tem);
            }
            cbxRoomNumber.SelectedIndex = cbxRoomNumber.Items.Count>0?0:-1;

            if (cbxRoomNumber.Items.Count == 0)
            {
                txtRoomCost.Text = String.Empty;
                txtTotalCost.Text = string.Empty;
                txtFanAndAirConCost.Text = string.Empty;
                cbxRoomNumber.Text = string.Empty;
            }
            else
            {
                cbxCustName.SelectedIndex = 0;
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnback_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        Timer timerForBookingDate;

        private void Booking_Load(object sender, EventArgs e)
        {
            timerForBookingDate = new Timer();
            timerForBookingDate.Tick += TimerForBookingDate_Tick;
            timerForBookingDate.Interval = 1000;
            timerForBookingDate.Start();
            
            RestrictionClass.restrictFromKeyboard.restrictAnyKeyFromKeyboard(txtCusID);
            RestrictionClass.restrictFromKeyboard.restrictAnyKeyFromKeyboard(txtBookID);
            RestrictionClass.restrictFromKeyboard.restrictAnyKeyFromKeyboard(txtEmpID);
            RestrictionClass.restrictFromKeyboard.restrictAnyKeyFromKeyboard(txtTotalCost);
            RestrictionClass.restrictFromKeyboard.restrictAnyKeyFromKeyboard(txtTotalCostOfAllRooms);
            RestrictionClass.restrictFromKeyboard.restrictAnyKeyFromKeyboard(txtRoomCost);     
            txtEmpID.Text = UserLoginDetail.empID!=null? UserLoginDetail.empID:"";

            cbxRoomNumber.KeyPress += cbxRoomNumber_KeyPress;

            refreshCustomers();
            dateOut.Value = dateIn.Value.AddHours(3);
            dateOut_ValueChanged(null, null);
            DefaultBookID = getBookIdFromDB();
            txtBookID.Text = DefaultBookID;
            getroom();
            radTime_CheckedChanged(null, null);
        }

        private void TimerForBookingDate_Tick(object sender, EventArgs e)
        {
            if (dgvMaster.SelectedRows.Count < 1)
            {
                dateBook.MaxDate = DateTime.Now.AddSeconds(1);
                dateBook.MinDate = DateTime.Now;
                dateBook.Value = DateTime.Now;

            }
            else
            {
                DateTime dt = Convert.ToDateTime(dgvMaster.Rows[0].Cells[1].Value.ToString());
                dateBook.MinDate = dt;
                dateBook.MaxDate = dt.AddSeconds(1);
                dateBook.Value = dt;
            }
            dateBook.KeyPress += DateBook_KeyPress;
        }

        private void DateBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbxRoomNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        string DefaultBookID;
        bool isFirstInsert = true;


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtRoomCost.Text == String.Empty)
                btnRoomCost_Click(null, null);
            if (txtRoomCost.Text == String.Empty)
                return;
            DateTime dateCIn = new DateTime(dateIn.Value.Year, dateIn.Value.Month, dateIn.Value.Day, dateIn.Value.Hour, dateIn.Value.Minute,0);
            DateTime dateCout = new DateTime(dateOut.Value.Year, dateOut.Value.Month, dateOut.Value.Day, dateOut.Value.Hour, dateOut.Value.Minute, 0);

            //newCustomer
            if (!CustomerDetail.cusDetail.ContainsKey(txtCusID.Text))
            {
                DialogResult dialog = MessageBox.Show("Do you want to add a new customer?", "Warning", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    string[] name = cbxCustName.Text.Split(' ');
                    string lastname = name.Length == 2 ? name[1] : "";
                    string firstname = name[0];
                    string tel = txtTel.Text;
                    string gender = radMale.Checked ? "Male" : "Female";
                    string[] data = { txtCusID.Text, firstname, lastname, gender, "", tel };

                    dataCon.exActionQuery.insertDataToDB("Customer", data);
                    refreshCustomers();
                    cbxCustName.SelectedItem = (firstname + " " + lastname).ToString();
                }
                else
                    return;
            }

            if (isFirstInsert)
                dgvMaster.Rows.Add(txtBookID.Text, dateBook.Text, txtCusID.Text, txtEmpID.Text, txtTotalCostOfAllRooms.Text);

            string duration = rad3.Checked ? rad3.Text : rad6.Checked ? rad6.Text : rad12.Checked ? rad12.Text : rad24.Checked ? rad24.Text : (dateCout-dateCIn).TotalDays+"";
            string coolerSelection = radFan.Checked ? radFan.Text : radAirCon.Text;
            string room = radOneBed.Checked ? radOneBed.Text : radTwoBed.Checked ? radTwoBed.Text : radVIP.Text;


            dvgDetail.Rows.Add(dateIn.Text, dateOut.Text, duration, coolerSelection, room, cbxRoomNumber.SelectedItem, txtRoomCost.Text, txtFanAndAirConCost.Text, txtTotalCost.Text);

            
            

            isFirstInsert = false;
            getroom();
        }

        private void cbxCustName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCustName.SelectedIndex != -1)
            {
                foreach (string tem in CustomerDetail.cusDetail.Keys)
                {
                    if (CustomerDetail.cusDetail[tem][0] + " " + CustomerDetail.cusDetail[tem][1] == cbxCustName.SelectedItem.ToString())
                    {
                        txtCusID.Text = tem;
                        txtTel.Text = CustomerDetail.cusDetail[tem][4];
                        radMale.Checked = !(radFemale.Checked = CustomerDetail.cusDetail[tem][2] == "Female");
                        selectedCustomer(true);
                        break;
                    }
                    else
                        selectedCustomer(false);
                }
            }
            else
                selectedCustomer(false);
        }

        private void dateOut_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateCIn = new DateTime(dateIn.Value.Year, dateIn.Value.Month, dateIn.Value.Day, dateIn.Value.Hour, dateIn.Value.Minute, 0);
            DateTime dateCout = new DateTime(dateOut.Value.Year, dateOut.Value.Month, dateOut.Value.Day, dateOut.Value.Hour, dateOut.Value.Minute, 0);
            if (dvgDetail.SelectedRows.Count==0&&(dateCout.Subtract(dateCIn)).TotalHours < 3)
            {
                MessageBox.Show("Incorrected Input");
                dateOut.Value = dateCIn.AddHours(3);
                rad3.Checked = true;
            }
            //int duration = rad3.Checked ? 3 : rad6.Checked ? 6 : rad12.Checked ? 12 : rad24.Checked ? 24 : (int)(dateCout - dateCIn).TotalDays;

            //if (duration < 0)
            //    isCorrectedDate = false;

            //if ((dateCout - dateCIn).TotalDays >0&&radOver24.Checked)
            //{
            //    radOver24.Checked = true;
            //    rad3.Enabled = rad6.Enabled = rad12.Enabled = rad24.Checked = false;
            //}
            //else
            //{
            //    rad3.Checked = true;
            //    rad3.Enabled = rad6.Enabled = rad12.Enabled = rad24.Enabled =  true;
            //}
            getroom();
        }


        private void btnRoomCost_Click(object sender, EventArgs e)
        {
            
            if (dvgDetail.SelectedRows.Count==0&&cbxRoomNumber.SelectedIndex == -1)
            {
                MessageBox.Show("No Room Available During that period!!");
                txtRoomCost.Text = String.Empty;
                txtTotalCost.Text = string.Empty;
                txtFanAndAirConCost.Text = string.Empty;
                return;
            }
            dataCon.GetPrice();
            DateTime dateCIn = new DateTime(dateIn.Value.Year, dateIn.Value.Month, dateIn.Value.Day, dateIn.Value.Hour, dateIn.Value.Minute, 0);
            DateTime dateCout = new DateTime(dateOut.Value.Year, dateOut.Value.Month, dateOut.Value.Day, dateOut.Value.Hour, dateOut.Value.Minute, 0);
            string room = radOneBed.Checked ? "Single" : radTwoBed.Checked ? "Double" : "VIP";
            int duration = rad3.Checked ? 3 : rad6.Checked ? 6 : rad12.Checked ? 12 : rad24.Checked ? 24 : (int)(dateCout - dateCIn).TotalDays;

            DataTable tblCost = dataCon.Price[room];

            if (radOver24.Checked)
            {
                object[] row24 = tblCost.Rows[tblCost.Rows.Count - 1].ItemArray;
                txtRoomCost.Text = (int)(dateCout - dateCIn).TotalDays * (double)row24[1] + "";
                txtFanAndAirConCost.Text = ((double)((radFan.Checked) ? row24[2] : row24[3]) * (int)(dateCout - dateCIn).TotalDays) + "";
            }
            else
            {
                for (int i = 0; i < tblCost.Rows.Count; i++)
                {
                    object[] row = tblCost.Rows[i].ItemArray;
                    if (Convert.ToInt32(row[0]) == duration)
                    {
                        txtRoomCost.Text = row[1].ToString();
                        txtFanAndAirConCost.Text = (radFan.Checked) ? row[2].ToString() : row[3].ToString();
                    }
                }
            }
            txtTotalCost.Text = (double.Parse(txtFanAndAirConCost.Text) + double.Parse(txtRoomCost.Text)).ToString();


            //Test Method
            //string roomTypeDesc=room;
            //double roomPrice=0;
            //bool pickAc=radAirCon.Checked;
            //double electricity=0;
            //double subTotal=0;

            //dataCon.CalculatePrice(dateCIn, dateCout, roomTypeDesc, ref roomPrice, pickAc, ref electricity, ref subTotal);
            //MessageBox.Show(roomPrice.ToString()+"\n"+electricity + "\n" +subTotal);
        }

        private void dvgDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (dvgDetail.SelectedRows.Count > 0)
            {
                int rowIndex = dvgDetail.SelectedRows[0].Index;
            
                DateTime datein = Convert.ToDateTime(dvgDetail.Rows[rowIndex].Cells[0].Value.ToString());
                DateTime dateout= Convert.ToDateTime(dvgDetail.Rows[rowIndex].Cells[1].Value.ToString());



                dateIn.Value=datein;
                dateOut.Value = dateout;

                radAirCon.Checked = !(radFan.Checked = (dvgDetail.Rows[rowIndex].Cells[3].Value.ToString() == radFan.Text));
                foreach (RadioButton rad in panel7.Controls.OfType<RadioButton>())
                    rad.Checked = (dvgDetail.Rows[rowIndex].Cells[2].Value.ToString() == rad.Text); 
                foreach (RadioButton rad in panel5.Controls.OfType<RadioButton>())
                    rad.Checked = (dvgDetail.Rows[rowIndex].Cells[4].Value.ToString() == rad.Text);
                cbxRoomNumber.SelectedItem = dvgDetail.Rows[rowIndex].Cells[5].Value.ToString();
                txtRoomCost.Text= dvgDetail.Rows[rowIndex].Cells[6].Value.ToString();
                txtFanAndAirConCost.Text= dvgDetail.Rows[rowIndex].Cells[7].Value.ToString(); 
                txtTotalCost.Text= dvgDetail.Rows[rowIndex].Cells[8].Value.ToString();
            }
            else
            {
                getroom();
                dateOut.Value = DateTime.Now.AddHours(3);
                dateIn.Value = DateTime.Now;                
                foreach (TextBox txt in groupBox1.Controls.OfType<TextBox>())
                    txt.Clear();
                foreach (ComboBox cbx in groupBox1.Controls.OfType<ComboBox>())
                    cbx.SelectedIndex = (cbx.Items.Count > 0) ? 0 : -1;
                rad3.Checked = radFan.Checked = radOneBed.Checked = true;

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dvgDetail.SelectedRows.Count == 1)
            {
                if (txtRoomCost.Text == String.Empty)
                    btnRoomCost_Click(null, null);
                if (txtRoomCost.Text == String.Empty)
                    return;
                int rowIndex = dvgDetail.SelectedRows[0].Index;

                DateTime dateCIn = new DateTime(dateIn.Value.Year, dateIn.Value.Month, dateIn.Value.Day, dateIn.Value.Hour, dateIn.Value.Minute, 0);
                DateTime dateCout = new DateTime(dateOut.Value.Year, dateOut.Value.Month, dateOut.Value.Day, dateOut.Value.Hour, dateOut.Value.Minute, 0);

                dgvMaster.SelectedRows[0].Cells[1].Value = dateBook.Text;
                dgvMaster.SelectedRows[0].Cells[0].Value = txtBookID.Text;
                dgvMaster.SelectedRows[0].Cells[2].Value = txtCusID.Text;
                dgvMaster.SelectedRows[0].Cells[3].Value = txtEmpID.Text;
                dgvMaster.SelectedRows[0].Cells[4].Value = txtTotalCostOfAllRooms.Text;

                string duration = rad3.Checked ? rad3.Text : rad6.Checked ? rad6.Text : rad12.Checked ? rad12.Text : rad24.Checked ? rad24.Text : (dateCout - dateCIn).TotalDays + "";
                string coolerSelection = radFan.Checked ? radFan.Text : radAirCon.Text;
                string room = radOneBed.Checked ? radOneBed.Text : radTwoBed.Checked ? radTwoBed.Text : radVIP.Text;

                dvgDetail.Rows[rowIndex].Cells[0].Value = dateIn.Text;
                dvgDetail.Rows[rowIndex].Cells[1].Value = dateOut.Text;
                dvgDetail.Rows[rowIndex].Cells[2].Value = duration;
                dvgDetail.Rows[rowIndex].Cells[3].Value = coolerSelection;
                dvgDetail.Rows[rowIndex].Cells[4].Value = room;
                dvgDetail.Rows[rowIndex].Cells[5].Value = cbxRoomNumber.SelectedItem.ToString();
                dvgDetail.Rows[rowIndex].Cells[6].Value = txtRoomCost.Text;
                dvgDetail.Rows[rowIndex].Cells[7].Value = txtFanAndAirConCost.Text;
                dvgDetail.Rows[rowIndex].Cells[8].Value = txtTotalCost.Text;

                getroom();
            }
            else
            {
                MessageBox.Show("Selected Row Not Founded!");
            }
        }

        bool IsClickedDeleted = false;
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dvgDetail.SelectedRows.Count > 0)
            {
                if (dvgDetail.RowCount > 1)
                {
                    while (dvgDetail.SelectedRows.Count > 1)
                    {
                        int index = dvgDetail.SelectedRows[0].Index;
                        dvgDetail.Rows.RemoveAt(index);
                    }
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Are you sure, you want to delete this record?", "Warning", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        
                        IsClickedDeleted = true;
                        btnNewRecord_Click(null, null);
                        IsClickedDeleted = false;
                    }
                        
                }
                getroom();
            }
        }

        private void radAirCon_CheckedChanged(object sender, EventArgs e)
        {
            getroom();
            if (dvgDetail.SelectedRows.Count == 1)
            {    
                btnRoomCost_Click(null, null);
            }
        }

        private void dgvMaster_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMaster.SelectedRows.Count < 1)
            {
                cbxCustName.SelectedIndex = -1;
                radMale.Checked = true;
                txtTel.Text = String.Empty;
                foreach (TextBox txt in groupBox3.Controls.OfType<TextBox>())
                {
                    if(txt.Name!= txtEmpID.Name)
                        txt.Clear();
                }
            }    
        }

        string bookID;
        bool isFirstSaveClicked = true;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dvgDetail.RowCount < 1)
            {
                if (!isFirstSaveClicked)
                {
                    dataCon.exActionQuery.deleteDataFromDB("Book", "WHERE BookID='" + bookID + "';");
                }
                return;
            }

            bookID = dgvMaster.Rows[0].Cells[0].Value.ToString();
            
            if (!isFirstSaveClicked)
            {
                dataCon.exActionQuery.deleteDataFromDB("Book", "WHERE BookID='" + bookID + "';");
            }

            //customer
            string cusID = dgvMaster.Rows[0].Cells[2].Value.ToString();

            string totalCost = dgvMaster.Rows[0].Cells[4].Value.ToString();
            string bookDate = dgvMaster.Rows[0].Cells[1].Value.ToString();
            string empID = dgvMaster.Rows[0].Cells[3].Value.ToString();

            string[] dataToBook = { bookID, bookDate, cusID, empID, totalCost };
            dataCon.exActionQuery.insertDataToDB("Book", dataToBook);

            //Detail
            
            for (int i = 0; i < dvgDetail.RowCount; i++)
            {
                string datein = dvgDetail.Rows[i].Cells[0].Value.ToString();
                string dateout = dvgDetail.Rows[i].Cells[1].Value.ToString();
                string duration = dvgDetail.Rows[i].Cells[2].Value.ToString().Trim('H', 'r');
                bool ac = (dvgDetail.Rows[i].Cells[3].Value.ToString() == "ម៉ាស៊ីនត្រជាក់") ? true : false;
                string room = (dvgDetail.Rows[i].Cells[4].Value.ToString() == "គ្រែមួយ") ? "Single" : (dvgDetail.Rows[i].Cells[4].Value.ToString() == "គ្រែពីរ") ? "Double" : "VIP";
                string roomNumber = dvgDetail.Rows[i].Cells[5].Value.ToString();
                string roomCost = dvgDetail.Rows[i].Cells[6].Value.ToString();
                string costOFAcandFan = dvgDetail.Rows[i].Cells[7].Value.ToString();
                string subtotal = dvgDetail.Rows[i].Cells[8].Value.ToString();

                string[] dataToBookDetail = { bookID, datein, dateout, roomNumber, "Pending", ac.ToString(), duration, room, roomCost, costOFAcandFan, subtotal };
                dataCon.exActionQuery.insertDataToDB("BookDetail", dataToBookDetail);

            }

            isFirstSaveClicked = false;

            MessageBox.Show("Successfully Saved!");
        }

        private void radTime_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dateCIn = new DateTime(dateIn.Value.Year, dateIn.Value.Month, dateIn.Value.Day, dateIn.Value.Hour, dateIn.Value.Minute, 0);
            DateTime dateCout = new DateTime(dateOut.Value.Year, dateOut.Value.Month, dateOut.Value.Day, dateOut.Value.Hour, dateOut.Value.Minute, 0);
            int duration = rad3.Checked ? 3 : rad6.Checked ? 6 : rad12.Checked ? 12 : 24;
            if (!radOver24.Checked)
                dateOut.Value = dateCIn.AddHours(duration);
            else
                dateOut.Value = dateCIn.AddDays((dateCout - dateCIn).TotalDays);

            getroom();
            if (dvgDetail.SelectedRows.Count == 1)
            {
                btnRoomCost_Click(null, null);
            }
        }

        private void dvgDetail_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            double total = 0;
            for (int i = 0; i < dvgDetail.Rows.Count; i++)
            {
                total += double.Parse(dvgDetail.Rows[i].Cells[8].Value.ToString());
            }
            txtTotalCostOfAllRooms.Text = total.ToString();
            if(dgvMaster.RowCount>0)
                dgvMaster.Rows[0].Cells[4].Value = txtTotalCostOfAllRooms.Text;
        }

        private void Booking_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isFirstSaveClicked&&dgvMaster.RowCount>0)
            {
                DialogResult dialog = MessageBox.Show("Do you want to save?", "Warning", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                    btnSave_Click(null, null);
            }
        }

        private void btnNewRecord_Click(object sender, EventArgs e)
        {
            if (dgvMaster.RowCount < 1)
                return;
            if (!isFirstSaveClicked&&IsClickedDeleted)
                dataCon.exActionQuery.deleteDataFromDB("Book", "WHERE BookID='" + bookID + "';");
            if (isFirstSaveClicked)
            {
                DialogResult dialog = MessageBox.Show("Do you want to save?", "Warning", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                    btnSave_Click(null, null);
            }

            while (dvgDetail.RowCount > 0)
            {
                dvgDetail.Rows.RemoveAt(0);
            }
            dgvMaster.Rows.RemoveAt(0);

            DefaultBookID = getBookIdFromDB();
            txtBookID.Text = DefaultBookID;
            isFirstInsert = true;
        }

        private void dvgDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(dvgDetail.RowCount>0)
                dvgDetail_RowStateChanged(null, null);
        }

        private void cbxRoomNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRoomCost_Click(null, null);
        }

        private void cbxCustName_TextChanged(object sender, EventArgs e)
        {
            if (cbxCustName.SelectedText == string.Empty)
            {
                txtCusID.Text = string.Empty;
                txtCusID_TextChanged(null, null);
                txtTel.Text = string.Empty;
            }

        }

        string defaultCusID;
        private void txtCusID_TextChanged(object sender, EventArgs e)
        {
            if (txtCusID.Text == string.Empty)
            {
                defaultCusID = getCusIdFromDB();
                txtCusID.Text = defaultCusID;
                selectedCustomer(false);
            }
        }
    }
}
