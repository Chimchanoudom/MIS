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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            UserLoginDetail.position = "admin";
            try
            {
                dataCon.Con.Open();
                string sqlCmd = "";
                if (UserLoginDetail.position == "admin")
                    sqlCmd = "SELECT Employee.EmpID,format(DateEmployed,'yyyy/MM/dd') as DateEmployed,FName,LName,Gender,format(DOB,'yyyy/MM/dd') as DOB,Phone,Address,Position,Salary,UserAcc.Username,Password,Active FROM UserAcc JOIN Employee ON UserAcc.EmpID=Employee.EmpID;";
                SqlDataReader dr = dataCon.ExecuteQry(sqlCmd);
                while (dr.Read())
                {
                    dataEmployee.Rows.Add(dr["EmpID"], dr["DateEmployed"], dr["FName"], dr["LName"], dr["Gender"], dr["DOB"], dr["Phone"], dr["Address"], dr["Position"], dr["Salary"], dr["Username"], dr["Password"], dr["Active"]);
                }
            }
            catch (Exception)
            {

              // throw;
            }
            dataCon.Con.Close();
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string empID = txtEmpId.Text;
            string address = txtAddress.Text;
            string firstname = txtFirstName.Text;
            string lastname = txtLastName.Text;
            string gender = (rndMale.Checked) ? "Male" : "Female";
            string DOB = dTPickerBirthDate.Value.Year +"/"+ dTPickerBirthDate.Value.Month+ "/"+ dTPickerBirthDate.Value.Day.ToString();
            string Tel = txtPhoneNumber.Text;
            string position = cbxPosition.SelectedItem.ToString();
            string salary = txtSalary.Text;
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            bool status = CheckActive.Checked;
            string dateEmployed = dTPickerIn.Value.Year + "/" + dTPickerIn.Value.Month + "/" + dTPickerIn.Value.Day.ToString();

            //string[] dt = DOB.Split(',');
            DateTime dt = new DateTime();
            
            dataEmployee.Rows.Add(empID, dateEmployed, firstname, lastname, gender, DOB, Tel, address, position, salary, username, password, status);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataEmployee.SelectedRows.Count > 0)
            {
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
            }
            
        }

        private void dataEmployee_SelectionChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = btnEdit.Enabled = (dataEmployee.SelectedRows.Count > 0);
            btnDelete.TextAlign = btnEdit.TextAlign = btnAdd.TextAlign;
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
                cbxPosition.SelectedItem = dataEmployee.Rows[index].Cells[8].Value.ToString();
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
            }
        }

        public void clearTextBox()
        {
            txtAddress.Text = "";
            txtEmpId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPassword.Text = "";
            txtPhoneNumber.Text = "";
            txtSalary.Text = "";
            txtUserName.Text = "";
        }
    }
}
