using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuestHouse
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        DataTable DT = new DataTable();
        String ID;
        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtFname_KeyPress(object sender, KeyPressEventArgs e)
        {
            dom_Design.CharaterOnly(e);
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            dom_Design.NumberOnly(e);
        }
        private void clear()
        {
            txtFname.Text = "";
            txtLname.Text = "";
            rndMale.Checked = true;
            txtIDnum.Text = "";
            txtTel.Text = "";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!(txtFname.Text == "" && txtLname.Text == "" && (rndFemale.Checked == false || rndMale.Checked == false) && txtIDnum.Text == "" && txtTel.Text == ""))
            {
                String ID = txtID.Text;
                String FName = txtFname.Text;
                String Lname = txtLname.Text;
                String Gender = rndMale.Checked == true ? rndMale.Text : rndFemale.Checked ? rndFemale.Text : "";
                String IDNum = txtIDnum.Text;
                String Tel = txtTel.Text;
                object[] Data = { ID, FName, Lname, Gender, IDNum, Tel };
                DT.Rows.Add(Data);
                ID= dom_Design.GenerateID(ID.Substring(6), "Cus_00");
                txtID.Text= ID;
                clear();
            }
            else
            {
                MessageBox.Show("Please ! Input Importan Data");
            }
            dataCustomer.ClearSelection();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            string[] columnHeaderName = {"លេខកូដសម្គាល់","នាមត្រកូល","គោត្តនាម-នាមខ្លួន","ភេទ","លេខអត្តសញ្ញាណប័ណ្ណ","លេខទូរស័ព្ទ" };
            DT= Dom_SqlClass.retriveData("Customer", dataCustomer);
            dataCustomer.DataSource = DT;
            dom_Design.GenerateColumHeader(columnHeaderName, dataCustomer.ColumnCount, dataCustomer);
            Dom_SqlClass.GetIDFromDB("cusID", "_", "customer");
            ID=dom_Design.GenerateID(Dom_SqlClass.GetIDFromDB("cusID", "_", "customer"), "Cus_00");
            txtID.Text = ID;
            dataCustomer.ClearSelection();
        }

        private void dataCustomer_SelectionChanged(object sender, EventArgs e)
        {
            if (dataCustomer.SelectedRows.Count > 0)
            {
                int i = dataCustomer.SelectedRows[0].Index;
                txtID.Text= dataCustomer.Rows[i].Cells[0].Value.ToString();
                txtFname.Text = dataCustomer.Rows[i].Cells[1].Value.ToString();
                txtLname.Text = dataCustomer.Rows[i].Cells[2].Value.ToString();
                rndFemale.Checked = !(rndMale.Checked = (dataCustomer.Rows[i].Cells[3].Value.ToString().ToLower().Trim() == "ប្រុស".ToLower().Trim()));
                txtIDnum.Text = dataCustomer.Rows[i].Cells[4].Value.ToString();
                txtTel.Text = dataCustomer.Rows[i].Cells[5].Value.ToString();
            }
            else
            {
                clear();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataCustomer.SelectedRows.Count == 1)
            {
                int i = dataCustomer.SelectedRows[0].Index;
                dataCustomer.Rows[i].Cells[0].Value= txtID.Text;
                dataCustomer.Rows[i].Cells[1].Value = txtFname.Text;
                dataCustomer.Rows[i].Cells[2].Value = txtLname.Text;
                dataCustomer.Rows[i].Cells[3].Value = rndMale.Checked == true ? "ប្រុស" : "ស្រី";
                dataCustomer.Rows[i].Cells[4].Value = txtIDnum.Text;
                dataCustomer.Rows[i].Cells[5].Value = txtTel.Text;
            }
            else
            {
                MessageBox.Show("Please ! Select Any Rows in List");
            }
            dataCustomer.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult Dir = MessageBox.Show("តើអ្នកចង់លុបទិន្នន័យនេះឫទេ?", "ទិន្នន័យនឹងត្រូវបានលុប", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (Dir==DialogResult.Yes)
            {
                while (dataCustomer.SelectedRows.Count>0)
                {
                    int i = dataCustomer.SelectedRows[0].Index;
                    dataCustomer.Rows.RemoveAt(i);
                }
            }
            dataCustomer.ClearSelection();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Dom_SqlClass.UpdateDate(DT);
        }
    }
}
