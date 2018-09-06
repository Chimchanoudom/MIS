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
            txtID.Text = ID;
            txtFname.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dataCustomer.SelectedRows.Count == 0)
            {
                                    if (!(txtFname.Text == "" && txtLname.Text == "" && (rndFemale.Checked == false || rndMale.Checked == false) && txtIDnum.Text == "" && txtTel.Text == ""))
                                    {
                                        String ID = txtID.Text;
                                        String FName = txtFname.Text;
                                        String Lname = txtLname.Text;
                                        String Gender = rndMale.Checked == true ? "Male" : rndFemale.Checked ? "Female" : "";
                                        String IDNum = txtIDnum.Text;
                                        String Tel = txtTel.Text;
                                        object[] Data = { ID, FName, Lname, Gender, IDNum, Tel };
                                        DT.Rows.Add(Data);
                                        ID = dom_Design.SetID(6, ID, "Cus_00");
                                         txtID.Text = ID;
                                        dataCustomer.ClearSelection();
                                        clear();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Please ! Input Importan Data");
                                    }
                                    dataCustomer.ClearSelection();
            }
            else
            {
                DialogResult dialog = MessageBox.Show("You are selecting one or more rows!\nDo you want to clear selection brfore your add new Data?", "Warning", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    dataCustomer.ClearSelection();
                    txtFname.Focus();
                }
            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            string[] columnHeaderName = {"លេខកូដសម្គាល់","នាមត្រកូល","គោត្តនាម-នាមខ្លួន","ភេទ","លេខអត្តសញ្ញាណប័ណ្ណ","លេខទូរស័ព្ទ" };
            DT= Dom_SqlClass.retriveData("Customer","Where 1=1",new string[] { "*"});
            dataCustomer.DataSource = DT;
            String[] Name = { "CusID", "FName", "LName", "Gender", "IDNum", "Tel" };
            dom_Design.ColumnName(dataCustomer, 6, Name);
            dom_Design.GenerateColumHeader(columnHeaderName, dataCustomer.ColumnCount, dataCustomer);
            Dom_SqlClass.GetIDFromDB("cusID", "_", "customer");
            ID=dom_Design.GenerateID(Dom_SqlClass.GetIDFromDB("cusID", "_", "customer"), "Cus_00");
            txtID.Text = ID;
            dataCustomer.ClearSelection();
            txtFname.Focus();
        }

        private void dataCustomer_SelectionChanged(object sender, EventArgs e)
        {
            if (dataCustomer.SelectedRows.Count > 0)
            {
           
                int i = dataCustomer.SelectedRows[0].Index;
                txtID.Text= dataCustomer.Rows[i].Cells[0].Value.ToString();
                txtFname.Text = dataCustomer.Rows[i].Cells[1].Value.ToString();
                txtLname.Text = dataCustomer.Rows[i].Cells[2].Value.ToString();
                rndFemale.Checked = !(rndMale.Checked = (dataCustomer.Rows[i].Cells[3].Value.ToString().ToLower().Trim() == "Male".ToLower().Trim()));
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
                dataCustomer.Rows[i].Cells[3].Value = rndMale.Checked == true ? "Male" : "Female";
                dataCustomer.Rows[i].Cells[4].Value = txtIDnum.Text;
                dataCustomer.Rows[i].Cells[5].Value = txtTel.Text;
                
               
            }
            else
            {
                MessageBox.Show("Please ! Select Any Rows in List");
            }
            //ID = dom_Design.GenerateID(ID.Substring(6), "Cus_00");
            //txtID.Text = ID;
            
            dataCustomer.ClearSelection();
            clear();
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
            //ID = dom_Design.GenerateID(ID.Substring(6), "Cus_00");
            //txtID.Text = ID;
            ID = dom_Design.SetID(6, ID, "Cus_00");
            txtID.Text = ID;
            dataCustomer.ClearSelection();
            clear();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Dom_SqlClass.UpdateDate(DT);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                string filter = (rndSearchFname.Checked ? "FName='" + txtSearch.Text + "'" : rndSearchLname.Checked ? "LName='" + txtSearch.Text + "'" : rndSearchID.Checked ? "CusID='" + txtSearch.Text + "'" : rndSearchIDNum.Checked ? "IDNum='" + txtSearch.Text + "'" : rndSearchTelephone.Checked ? "Tel='" + txtSearch.Text + "'" : "");
                DT.DefaultView.RowFilter = filter;
                dataCustomer.ClearSelection();
            }
            else
            {
                MessageBox.Show("Please Enter any text to search!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DT.DefaultView.RowFilter = "";
            dataCustomer.ClearSelection();
            txtSearch.Text = "";
        }
    }
    
}
