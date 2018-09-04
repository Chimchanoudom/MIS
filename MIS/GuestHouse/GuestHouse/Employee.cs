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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            
        }
        public static DataTable dataTable { get; set; }
        private void pictureBox1_Click(object sender, EventArgs e)
        { 
           this.Close();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            txtEmpId.Text = EmpClass.GetData.getIdFromDB();
            dataTable = new DataTable();
            UserLoginDetail.position = "admin";
            EmpClass.dataTableHeader = new List<string>();
            for (int i = 0; i < dataEmployee.ColumnCount - 1; i++)
            {
                String st = dataEmployee.Columns[i].Name;
                dataTable.Columns.Add(st);
                EmpClass.dataTableHeader.Add(dataEmployee.Columns[i].HeaderText);
            }
            dataTable.Columns.Add("Status", typeof(bool));
            EmpClass.dataTableHeader.Add(dataEmployee.Columns[12].HeaderText);
            dataEmployee.Columns.Clear();
            dataEmployee.DataSource = dataTable;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                dataEmployee.Columns[i].HeaderText = EmpClass.dataTableHeader[i];
            }

            try
            {
                dataCon.Con.Open();
                string sqlCmd = "";
                if (UserLoginDetail.position == "admin")
                    sqlCmd = "SELECT Employee.EmpID,format(DateEmployed,'yyyy/MM/dd') as DateEmployed,FName,LName,Gender,format(DOB,'yyyy/MM/dd') as DOB,Phone,Address,Position,Salary,UserAcc.Username,Password,Active FROM UserAcc JOIN Employee ON UserAcc.EmpID=Employee.EmpID;";
                SqlDataReader dr = dataCon.ExecuteQry(sqlCmd);
                while (dr.Read())
                {
                    dataTable.Rows.Add(dr["EmpID"], dr["DateEmployed"], dr["FName"], dr["LName"], dr["Gender"], dr["DOB"], dr["Phone"], dr["Address"], dr["Position"], dr["Salary"], dr["Username"], dr["Password"], dr["Active"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Connect to Database!");
            }
            dataCon.Con.Close();  
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                txtPassword_Leave(txtPassword, null);
                txtPassword_Leave(txtUserName, null);
                txtPassword_Leave(txtSalary, null);
                string empID = txtEmpId.Text;
                string address = (txtAddress.Text == "") ? "" : txtAddress.Text;
                string firstname = txtFirstName.Text;
                string lastname = txtLastName.Text;
                string gender = (rndMale.Checked) ? "Male" : "Female";
                string DOB = dTPickerBirthDate.Value.Year + "/" + dTPickerBirthDate.Value.Month + "/" + dTPickerBirthDate.Value.Day.ToString();
                string Tel = (txtPhoneNumber.Text == "") ? "" : txtPhoneNumber.Text;
                string position = cbxPosition.SelectedItem.ToString();
                string salary = txtSalary.Text;
                string username = txtUserName.Text;
                string password = txtPassword.Text;
                bool status = CheckActive.Checked;
                string dateEmployed = dTPickerIn.Value.Year + "/" + dTPickerIn.Value.Month + "/" + dTPickerIn.Value.Day.ToString();
                dataTable.Rows.Add(empID, dateEmployed, firstname, lastname, gender, DOB, Tel, address, position, salary, username, password, status);
                txtEmpId.Text = (((Convert.ToInt32(txtEmpId.Text) + 1).ToString()).Length == 2) ? "0" + (Convert.ToInt32(txtEmpId.Text) + 1) : "00" + (Convert.ToInt32(txtEmpId.Text) + 1);
                clearTextBox();
                isSaved = false;
            }
            catch (Exception){MessageBox.Show("Fill-in All Information required!");}
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataEmployee.SelectedRows.Count > 0)
            {
                txtPassword_Leave(txtPassword, null);
                txtPassword_Leave(txtUserName, null);
                txtPassword_Leave(txtSalary, null);
                int index = dataEmployee.SelectedRows[0].Index;

                dataEmployee.Rows[index].Cells[0].Value = txtEmpId.Text;
                dataEmployee.Rows[index].Cells[7].Value = txtAddress.Text;
                dataEmployee.Rows[index].Cells[2].Value = txtFirstName.Text;
                dataEmployee.Rows[index].Cells[3].Value = txtLastName.Text;
                dataEmployee.Rows[index].Cells[4].Value = (rndMale.Checked) ? "Male" : "Female";
                dataEmployee.Rows[index].Cells[5].Value = dTPickerBirthDate.Value.Year + "/" + dTPickerBirthDate.Value.Month + "/" + dTPickerBirthDate.Value.Day.ToString();
                dataEmployee.Rows[index].Cells[6].Value = txtPhoneNumber.Text;
                dataEmployee.Rows[index].Cells[8].Value = cbxPosition.SelectedItem.ToString();
                dataEmployee.Rows[index].Cells[9].Value = txtSalary.Text;
                dataEmployee.Rows[index].Cells[10].Value = txtUserName.Text;
                dataEmployee.Rows[index].Cells[11].Value = txtPassword.Text;
                dataEmployee.Rows[index].Cells[12].Value = CheckActive.Checked;
                dataEmployee.Rows[index].Cells[1].Value = dTPickerIn.Value.Year + "/" + dTPickerIn.Value.Month + "/" + dTPickerIn.Value.Day.ToString();
                isSaved = false;
            }
        }

        private void dataEmployee_SelectionChanged(object sender, EventArgs e)
        {
            //btnDelete.Enabled = btnEdit.Enabled = (dataEmployee.SelectedRows.Count > 0);
            
            if (dataEmployee.SelectedRows.Count > 0)
            {
                int index = dataEmployee.SelectedRows[0].Index;

                txtEmpId.Text = dataEmployee.Rows[index].Cells[0].Value.ToString();
                txtAddress.Text = dataEmployee.Rows[index].Cells[7].Value.ToString();
                txtFirstName.Text = dataEmployee.Rows[index].Cells[2].Value.ToString();
                txtLastName.Text = dataEmployee.Rows[index].Cells[3].Value.ToString();
                rndFemale.Checked=!(rndMale.Checked = (dataEmployee.Rows[index].Cells[4].Value.ToString().ToLower().Trim() == "Male".ToLower()));
                string[] stDob = (dataEmployee.Rows[index].Cells[5].Value + "").Split('/');
                DateTime dob = new DateTime(int.Parse(stDob[0]), int.Parse(stDob[1]), int.Parse(stDob[2]), 0, 0, 0);
                dTPickerBirthDate.Value = dob;
                txtPhoneNumber.Text = dataEmployee.Rows[index].Cells[6].Value.ToString();
                cbxPosition.SelectedItem = dataEmployee.Rows[index].Cells[8].Value.ToString()[0].ToString().ToUpper()+ dataEmployee.Rows[index].Cells[8].Value.ToString().Substring(1);
                txtSalary.Text = dataEmployee.Rows[index].Cells[9].Value.ToString();
                txtUserName.Text = dataEmployee.Rows[index].Cells[10].Value.ToString();
                txtPassword.Text = dataEmployee.Rows[index].Cells[11].Value.ToString();
                CheckActive.Checked = !(dataEmployee.Rows[index].Cells[12].Value.ToString()=="UnChecked");
                string[] stDE = (dataEmployee.Rows[index].Cells[1].Value + "").Split('/');
                DateTime dE = new DateTime(int.Parse(stDE[0]), int.Parse(stDE[1]), int.Parse(stDE[2]), 0, 0, 0);
                dTPickerIn.Value = dE;
            }
            else
            {
                clearTextBox();
                CheckActive.Checked = false;               
                dTPickerIn.Value= System.DateTime.Now;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataEmployee.SelectedRows.Count > 0)
            {
                while (dataEmployee.SelectedRows.Count > 0)
                {
                    int index = dataEmployee.SelectedRows[0].Index;
                    dataEmployee.Rows.RemoveAt(index);
                }
                dataEmployee.ClearSelection();
                isSaved = false;
            }
        }

        public void clearTextBox()
        {
            txtAddress.Text = "";
            //txtEmpId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPassword.Text = "";
            txtPhoneNumber.Text = "";
            txtSalary.Text = "";
            txtUserName.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                string filter = (rndSearchFname.Checked ? "FName='" + txtSearch.Text + "'" : rndSearchLname.Checked ? "LName='" + txtSearch.Text + "'" : rndSearchID.Checked ? "EmpID='" + txtSearch.Text + "'" : rndSearchPosition.Checked ? "Position='" + txtSearch.Text + "'" : rndSearchTelephone.Checked ? "Tel='" + txtSearch.Text + "'" : "");
                dataTable.DefaultView.RowFilter = filter;
            }
            else
            {
                MessageBox.Show("Please Enter any text to search!");
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            dataTable.DefaultView.RowFilter = string.Empty;
        }

        private void txtEmpId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        bool isSaved = true;
        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, Dictionary<string, string>> allData = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> row;

            for(int NumberOfrow = 0; NumberOfrow < dataEmployee.RowCount; NumberOfrow++)
            {
                row = new Dictionary<string, string>();
                for (int NumberOfColumn = 0; NumberOfColumn < dataEmployee.ColumnCount; NumberOfColumn++)
                {
                    string value = dataEmployee.Rows[NumberOfrow].Cells[NumberOfColumn].Value.ToString();
                    string keys = dataEmployee.Columns[NumberOfColumn].Name;
                    row.Add(keys, value);
                }
                allData.Add(dataEmployee.Rows[NumberOfrow].Cells[0].Value.ToString(), row);
            }

            List<int> errorIndex = new List<int>();
            EmpClass.GetData.getAllEmployeeData();
            foreach(string tempKeys in allData.Keys)
            {
                if (EmpClass.GetData.empData.ContainsKey(tempKeys))
                {
                    bool error = false;
                    //try { dataCon.Con.Open(); } catch (Exception) { MessageBox.Show("Unable to Connect to Database!"); }
                    string sqlCmd = @"UPDATE Employee 
                                        SET FName='"+(allData[tempKeys])["FName"].ToString()+"'," +
                                        "LName='"+(allData[tempKeys])["LName"].ToString()+"'," +
                                        "Phone='"+ (allData[tempKeys])["Tel"].ToString() + "'," +
                                        "Gender='"+ (allData[tempKeys])["Gender"].ToString() + "'," +
                                        "DOB='"+(allData[tempKeys])["DOB"].ToString() + "'," +
                                        "Address='"+ (allData[tempKeys])["Address"].ToString() + "'," +
                                        "Position='"+ (allData[tempKeys])["Position"].ToString() + "'," +
                                        "Salary="+ (allData[tempKeys])["Salary"] + "," +
                                        "DateEmployed='"+ (allData[tempKeys])["DateEmployed"].ToString() + "'," +
                                        "Active='"+ (allData[tempKeys])["Status"].ToString() + "'" +
                                        " WHERE EmpID='" +tempKeys+"';"
                                        ;
                    string sqlCmd1 = @"UPDATE UserAcc 
                                        SET Username='" + (allData[tempKeys])["Username"].ToString() + "'," +
                                        "Password='" + (allData[tempKeys])["Password"].ToString() + "' " +
                                        "WHERE EmpID='" + tempKeys + "';"
                                        ;
                    dataCon.ExecuteActionQry(sqlCmd, ref error);
                    dataCon.ExecuteActionQry(sqlCmd1, ref error);
                    if (error)
                    {
                        int index = 0;
                        for (int i = 0; i < dataEmployee.RowCount; i++)
                            if (dataEmployee.Rows[i].Cells[0].Value.ToString() == tempKeys)
                                errorIndex.Add(index);                       
                    }
                }
                else
                {
                    bool error = false;
                    string sqlCmd = @"INSERT INTO Employee (FName,LName,Phone,Gender,DOB,Address,Position,Salary,DateEmployed,Active,EmpID) 
                                        VALUES ('" + (allData[tempKeys])["FName"].ToString() + "'," +
                                        "'" + (allData[tempKeys])["LName"].ToString() + "'," +
                                        "'" + (allData[tempKeys])["Tel"].ToString() + "'," +
                                        "'" + (allData[tempKeys])["Gender"].ToString() + "'," +
                                        "'" + (allData[tempKeys])["DOB"].ToString() + "'," +
                                        "'" + (allData[tempKeys])["Address"].ToString() + "'," +
                                        "'" + (allData[tempKeys])["Position"].ToString() + "'," +
                                        "" + (allData[tempKeys])["Salary"] + "," +
                                        "'" + (allData[tempKeys])["DateEmployed"].ToString() + "'," +
                                        "'" + (allData[tempKeys])["Status"].ToString() + "'," +
                                        "'" + tempKeys + "');";
                    //MessageBox.Show((allData[tempKeys])["Username"].ToString());
                    string sqlCmd1 = @"INSERT INTO UserAcc (EmpID,Username,Password) 
                                        VALUES ('" + tempKeys + "'," +
                                        "'" + (allData[tempKeys])["Username"].ToString() + "'," +
                                        "'" + (allData[tempKeys])["Password"].ToString() + "'" +
                                        ");"
                                        ;
                    dataCon.ExecuteActionQry(sqlCmd, ref error);
                    dataCon.ExecuteActionQry(sqlCmd1, ref error);
                    if (error)
                    {
                        int index = 0;
                        for (int i = 0; i < dataEmployee.RowCount; i++)
                            if (dataEmployee.Rows[i].Cells[0].Value.ToString() == tempKeys)
                                errorIndex.Add(index);
                    }
                }
            }

            foreach(string tempKeys in EmpClass.GetData.empData.Keys)
            {
                if (!allData.ContainsKey(tempKeys))
                {
                    bool error = false;
                    string sqlCmd = @"DELETE FROM Employee
                                    WHERE EmpID='" + tempKeys + "';";
                    dataCon.ExecuteActionQry(sqlCmd, ref error); 
                    if (error)
                    {
                        int index = 0;
                        for (int i = 0; i < dataEmployee.RowCount; i++)
                            if (dataEmployee.Rows[i].Cells[0].Value.ToString() == tempKeys)
                                errorIndex.Add(index);
                    }
                }
            }

            string errorString = "Error Founded on:\nRow:\t";
            for (int i = 0; i < errorIndex.Count; i++)
            {
                errorString+=errorIndex[i] + "\n\t";
            }
            if(errorIndex.Count>0)
                MessageBox.Show(errorString);
            MessageBox.Show("Successfully Saved!");
            isSaved = true;
        }

        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictionClass.restrictFromKeyboard.restrictNumberAndSigns(e);
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            string[] st = { "" };
            RestrictionClass.restrictFromKeyboard.restrictAlphabet(e,st);
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            string[] st = { "+" };
            RestrictionClass.restrictFromKeyboard.restrictAlphabet(e, st);
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictionClass.restrictFromKeyboard.restrictUnicodeAlphabets(e);
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            TextBox txb = (TextBox)sender;
            txb.Text = RestrictionClass.GetIntFromKhNumber(txb.Text);
        }

        private void Employee_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaved==false)
            {
                DialogResult dialog = MessageBox.Show("Do you want to save?", "Warning", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                    btnSave_Click(null, null);
            }
            
        }
    }
}
