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
    public partial class expensType : Form
    {
        public expensType()
        {
            InitializeComponent();
        }
        String ID;
        DataTable DT;
        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void clear()
        {
            txtTypeDes.Text = "";
            txtTypeDes.Focus();
            txtID.Text = ID;

        }
        private void expensType_Load(object sender, EventArgs e)
        {
            DT = Dom_SqlClass.retriveData("ExpenseType", dataTypeExpens);
            dataTypeExpens.DataSource = DT;
            dom_Design.GenerateColumHeader(new string[] {"លេខកូដសម្គាល់","ឈ្មោះប្រភេទចំណាយ" }, dataTypeExpens.ColumnCount, dataTypeExpens);
            ID = dom_Design.GenerateID(Dom_SqlClass.GetIDFromDB("ExpTypeID", "_", "ExpenseType"), "EXT_00");
            txtID.Text = ID;
            dataTypeExpens.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dataTypeExpens.SelectedRows.Count == 0)
            {
                if (txtID.Text != "" || txtTypeDes.Text != "")
                {
                    String iD = txtID.Text.Trim();
                    String Des = txtTypeDes.Text.Trim();
                    object[] Data = {iD,Des};
                    DT.Rows.Add(Data);
                    txtID.Text = dom_Design.SetID(6, ID, "EXT_00");
                    ID = txtID.Text;
                    clear();
                }
                else
                {
                    MessageBox.Show("Please ! Input Importan Data");
                }
                dataTypeExpens.ClearSelection();
            }
            else
            {
                DialogResult dialog = MessageBox.Show("You are selecting one or more rows!\nDo you want to clear selection brfore your add new Data?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    dataTypeExpens.ClearSelection();
                    txtTypeDes.Focus();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataTypeExpens.SelectedRows.Count == 1)
            {
                int i = dataTypeExpens.SelectedRows[0].Index;
                dataTypeExpens.Rows[i].Cells[0].Value = txtID.Text;
                dataTypeExpens.Rows[i].Cells[1].Value = txtTypeDes.Text;
            }
            else
            {
                MessageBox.Show("Please ! Select Any Rows in List");
            }
            dataTypeExpens.ClearSelection();
            clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataTypeExpens.SelectedRows.Count > 0)
            {
                DialogResult Dir = MessageBox.Show("តើអ្នកចង់លុបទិន្នន័យនេះឫទេ?", "ទិន្នន័យនឹងត្រូវបានលុប", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Dir == DialogResult.Yes)
                {
                    while (dataTypeExpens.SelectedRows.Count > 0)
                    {
                        int i = dataTypeExpens.SelectedRows[0].Index;
                        dataTypeExpens.Rows.RemoveAt(i);
                    }
                }
                dataTypeExpens.ClearSelection();
                clear();
            }
            else
            {
                MessageBox.Show("Please ! Select Any Rows in List");
            }
        }

        private void dataTypeExpens_SelectionChanged(object sender, EventArgs e)
        {
            if (dataTypeExpens.SelectedRows.Count > 0)
            {

                int i = dataTypeExpens.SelectedRows[0].Index;
                txtID.Text = dataTypeExpens.Rows[i].Cells[0].Value.ToString();
                txtTypeDes.Text = dataTypeExpens.Rows[i].Cells[1].Value.ToString();
            }
            else
            {
                clear();
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Dom_SqlClass.UpdateDate(DT);
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
