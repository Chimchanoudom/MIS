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
    public partial class Expens : Form
    {
        public Expens()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            dom_Design.NumberOnly(e);
        }
        DataTable DT1 = new DataTable();
        String ID;
        private void Clear()
        {
            dateNote.Value=datePay.Value= DateTime.Now;
            CMexpensType.SelectedIndex = -1;
            txtAmount.Text = "";
            txtNamePay.Text = "";
            txtID.Text = ID;

        }

        private void Expens_Load(object sender, EventArgs e)
        {
            Dom_SqlClass.FillItemToCombobox("SELECT ExpTypeID,ExpDesc  FROM ExpenseType", "ExpTypeID", "ExpDesc", CMexpensType);
            ID = dom_Design.GenerateID(Dom_SqlClass.GetIDFromDB("ExpID", "_", "Expense"), "EXP_00");
            txtID.Text = ID;
            dataExpens.ClearSelection();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dataExpens.SelectedRows.Count == 0)
            {
                if (CMexpensType.SelectedIndex >= 0 || txtAmount.Text != "" || txtNamePay.Text != "")
                {
                    String ExpID = txtID.Text;
                    DateTime NoteDate = dateNote.Value;
                    String ExpType = CMexpensType.ValueMember.ToString();
                    String ExpName = txtNamePay.Text;
                    DateTime PayDate = datePay.Value;
                    float Amount = float.Parse(txtAmount.Text);
                    bool Success = false;
                    Success = Dom_SqlClass.InsertData("Expense", new object[] { ExpID, NoteDate, });
                    Success = Dom_SqlClass.InsertData("ExpenseDetail", new object[] { ExpID,ExpType,ExpName,Amount, PayDate});
                    if (Success == true)
                    {
                        ExpType = CMexpensType.DisplayMember;
                        dataExpens.Rows.Add(new object[] {ExpID,NoteDate.ToShortDateString(),ExpType,ExpName,PayDate.ToShortDateString(),Amount });
                        ID = dom_Design.SetID(6, ID, "EXP_00");
                        dataExpens.ClearSelection();
                        Clear();
                    }
                 }
                else
                {
                    MessageBox.Show("Please! Input All importan Data");
                }
            }
            else
            {
                DialogResult dialog = MessageBox.Show("You are selecting one or more rows!\nDo you want to clear selection brfore your add new Data?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    dataExpens.ClearSelection();
                }
            }
        }

        private void dataExpens_SelectionChanged(object sender, EventArgs e)
        {
            if (dataExpens.SelectedRows.Count ==1)
            {
                int i = dataExpens.SelectedRows[0].Index;
                txtID.Text = dataExpens.Rows[i].Cells[0].Value.ToString();
                dateNote.Value = DateTime.Parse(dataExpens.Rows[i].Cells[1].Value.ToString());
                CMexpensType.SelectedItem= dataExpens.Rows[i].Cells[2].Value.ToString();
                txtNamePay.Text= dataExpens.Rows[i].Cells[3].Value.ToString();
                datePay.Value = DateTime.Parse(dataExpens.Rows[i].Cells[4].Value.ToString());
                txtAmount.Text = dataExpens.Rows[i].Cells[5].Value.ToString();
            }
            else
            {
                Clear();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (dataExpens.SelectedRows.Count == 1)
            {
                object edit = @"UPDATE Expense SET DateCreated='" + dateNote.Value.Date.ToShortDateString()+ "' WHERE ExpID=" + "'"+txtID.Text+ "'";
                object edit1 = @"UPDATE ExpenseDetail SET ExpTypeID='" + CMexpensType.ValueMember + "' , ExpDes='" + txtNamePay.Text + "' , Amount='" + txtAmount.Text + "' ,ExpDate='" + datePay.Value.Date.ToShortDateString() + "' WHERE ExpID=" + "'" + txtID.Text + "'";
                MessageBox.Show(edit + "\n" + edit1);
                if (Dom_SqlClass.Edit(edit) == true && Dom_SqlClass.Edit(edit1) == true)
                {
                    int i = dataExpens.Rows[0].Index;
                    dataExpens.Rows[i].Cells[0].Value = txtID.Text;
                    dataExpens.Rows[i].Cells[1].Value = dateNote.Value.ToShortDateString();
                    dataExpens.Rows[i].Cells[2].Value = CMexpensType.SelectedItem.ToString();
                    dataExpens.Rows[i].Cells[3].Value = txtNamePay.Text;
                    dataExpens.Rows[i].Cells[4].Value = datePay.Value.ToShortDateString();
                    dataExpens.Rows[i].Cells[5].Value = txtAmount.Text;
                }
            }
            else
            {
                MessageBox.Show("Please ! Select Any Rows in List");
            }
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataExpens.SelectedRows.Count > 0)
            {
                DialogResult Dir = MessageBox.Show("តើអ្នកចង់លុបទិន្នន័យនេះឫទេ?", "ទិន្នន័យនឹងត្រូវបានលុប", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Dir == DialogResult.Yes)
                {
                    object delete = @"DELETE FROM Expense WHERE ExpID in (";
                    for (int i = 0; i < dataExpens.SelectedRows.Count; i++)
                    {
                        delete += "'" + dataExpens.Rows[i].Cells[0].Value.ToString() + "',";
                    }
                    delete = delete.ToString().TrimEnd(',');
                    MessageBox.Show(delete+"");
                    if (Dom_SqlClass.Delete(delete) == true)
                    {
                        while (dataExpens.SelectedRows.Count > 0)
                        {
                            int i = dataExpens.SelectedRows[0].Index;
                            dataExpens.Rows.RemoveAt(i);
                        }
                    }
                }
                dataExpens.ClearSelection();
                Clear();
            }
            else
            {
                MessageBox.Show("Please ! Select Any Rows in List");
            }
        }
    }
}
