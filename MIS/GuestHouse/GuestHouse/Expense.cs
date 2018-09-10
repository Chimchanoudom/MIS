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
    public partial class Expense : Form
    {
        public Expense()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        String ID;
        DataTable DT=new DataTable();
        String Statement1="", Statement2="";
        int n = 0;
        void Clear()
        {
            DataExpense.ClearSelection();
            DateNote.Value = DateTime.Now;
            cmType.SelectedIndex = -1;
            txtName.Text = "";
            datePay.Value = DateTime.Now;
            txtAmount.Text = "";
            txtID.Text = ID;
        }
        void GeneratData()
        {
            DataExpense.Rows.Clear();

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DataExpense.Rows.Add(DT.Rows[i]["ExpID"], DT.Rows[i]["DateCreated"], DT.Rows[i]["ExpDesc"], DT.Rows[i]["ExpDes"], DT.Rows[i]["ExpDate"], DT.Rows[i]["Amount"]);
                }
            
            foreach(DataGridViewColumn column in DataExpense.Columns)
            {
                DataExpense.Columns["column1"].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            DataExpense.Columns[1].DefaultCellStyle.Format = "ddd-dd-MMMM-yyyy";
            DataExpense.Columns[4].DefaultCellStyle.Format = "ddd-dd-MMMM-yyyy";
        }
        private void Expense_Load(object sender, EventArgs e)
        {
            ID = dom_Design.GenerateID(Dom_SqlClass.GetIDFromDB("ExpID", "_", "Expense"), "EXP_00");
            txtID.Text = ID;
            Dom_SqlClass.FillItemToCombobox("SELECT ExpTypeID,ExpDesc FROM ExpenseType", "ExpTypeID", "ExpDesc", cmType);
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (DataExpense.SelectedRows.Count <1)
            {
                if (cmType.SelectedIndex !=-1 && txtName.Text != "" && txtAmount.Text != "")
                {
                    String EID = txtID.Text;
                    DateTime DN = DateNote.Value;
                    String Type = cmType.SelectedItem.ToString();
                    String Name = txtName.Text;
                    DateTime DP = datePay.Value;
                    float Amount =float.Parse( txtAmount.Text);
                    Statement1 = @"INSERT INTO Expense Values( '"+EID+"',"+"'"+DN.ToShortDateString()+"');";
                    Statement2 = @"INSERT INTO ExpenseDetail Values('" + EID + "', " + "'" + cmType.ValueMember + "', "+"'"+Name+"',"+"'"+Amount+"',"+"'"+DP.ToShortDateString()+"');";
                   // MessageBox.Show(Statement1 + "\n" + Statement2);
                   if( Dom_SqlClass.SQLMultiTable(new string[] { Statement1, Statement2 }) == true)
                    {
                        DataExpense.Rows.Add(new object[] { EID,DN,Type,Name,DP,Amount});
                        n++;
                    }
                    DataExpense.ClearSelection();
                    ID = dom_Design.SetID(6, ID, "EXP_00");
                    Clear();
                }
                else
                {
                    MessageBox.Show("Please Input Importan Information!");
                }
            }
            else
            {
                DialogResult dialog = MessageBox.Show("You are selecting one or more rows!\nDo you want to clear selection?", "Warning", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    DataExpense.ClearSelection();

                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (DataExpense.SelectedRows.Count == 1)
            {
                Statement1 = @"UPDATE Expense Set ExpID='" + txtID.Text + "', DateCreated='" + DateNote.Value.ToShortDateString() + "' WHERE ExpID='"+txtID.Text+"';" ;
                Statement2 = @"UPDATE ExpenseDetail Set ExpID='" + txtID.Text + "', ExpTypeID='"+cmType.ValueMember+"', ExpDes='"+txtName.Text+"', ExpDate='"+datePay.Value.ToShortDateString()+"', Amount="+txtAmount.Text+ " WHERE ExpID='" + txtID.Text + "';";
               // MessageBox.Show(Statement1 + "\n" + Statement2);
                if(Dom_SqlClass.SQLMultiTable(new string[] { Statement1, Statement2 }) == true)
                {
                    int i = DataExpense.SelectedRows[0].Index;
                    DataExpense.Rows[i].Cells[0].Value=txtID.Text;
                    DataExpense.Rows[i].Cells[1].Value=DateNote.Value;
                   DataExpense.Rows[i].Cells[2].Value=cmType.SelectedItem.ToString();
                    DataExpense.Rows[i].Cells[3].Value=txtName.Text;
                    DataExpense.Rows[i].Cells[4].Value=datePay.Value;
                    DataExpense.Rows[i].Cells[5].Value=txtAmount.Text;
                }
            }
            else
            {
                MessageBox.Show("Please Select any Rows in List");
            }
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult Dir = MessageBox.Show("តើអ្នកចង់លុបទិន្នន័យនេះឫទេ?", "ទិន្នន័យនឹងត្រូវបានលុប", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Dir == DialogResult.Yes)
            {
                Statement1 = @"DELETE FROM Expense WHERE ExpID in(";
                for (int i = 0; i < DataExpense.SelectedRows.Count; i++)
                {
                    Statement1 += "'" + DataExpense.SelectedRows[i].Cells[0].Value.ToString()+"',";
                }
                Statement1 = Statement1.TrimEnd(',') + ");";
                MessageBox.Show(Statement1);
                if (Dom_SqlClass.SQLMultiTable(new string[] { Statement1})==true)
                {
                    while (DataExpense.SelectedRows.Count > 0)
                    {
                        int i = DataExpense.SelectedRows[0].Index;
                        DataExpense.Rows.RemoveAt(i);
                        if (n != 0)
                        {
                            n--;
                        }
                    }
                }
            }
            //ID = dom_Design.GenerateID(ID.Substring(6), "Cus_00");
            //txtID.Text = ID;
            Clear();
        }

        private void rndSearchID_CheckedChanged(object sender, EventArgs e)
        {
            if (rndSearcAll.Checked)
            {
                txtSearch.Visible = false;
                DateSearch.Visible = false;
            }
            else if(rndSearchNote.Checked||rndSearchPay.Checked)
            {
                txtSearch.Visible = false;
                DateSearch.Visible = true;
            }
            else
            {
                txtSearch.Visible = true;
                DateSearch.Visible = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (rndSearchID.Checked)
            {
                DT.Rows.Clear();
                String ST= @"SELECT Expense.ExpID,Expense.DateCreated,ExpenseType.ExpDesc,ExpenseDetail.ExpDes,ExpenseDetail.ExpDate,ExpenseDetail.Amount FROM Expense JOIN ExpenseDetail ON ExpenseDetail.ExpID=Expense.ExpID JOIN ExpenseType ON ExpenseDetail.ExpTypeID=ExpenseType.ExpTypeID WHERE LOWER(Expense.ExpID)='"+txtSearch.Text+"';";
                DT = Dom_SqlClass.retriveDataMultiTable(ST);
                GeneratData();
                DataExpense.ClearSelection();

            }
            else if (rndSearcAll.Checked)
            {
                DT.Rows.Clear();
                String Statement = @" SELECT Expense.ExpID,Expense.DateCreated,ExpenseType.ExpDesc,ExpenseDetail.ExpDes,ExpenseDetail.ExpDate,ExpenseDetail.Amount FROM Expense JOIN ExpenseDetail ON Expense.ExpID=ExpenseDetail.ExpID JOIN ExpenseType ON ExpenseType.ExpTypeID=ExpenseDetail.ExpTypeID; ";
                DT = Dom_SqlClass.retriveDataMultiTable(Statement);
                GeneratData();
                DataExpense.ClearSelection();
            }
            else 
            {
                DT.Rows.Clear();
                String ST = @"SELECT Expense.ExpID,Expense.DateCreated,ExpenseType.ExpDesc,ExpenseDetail.ExpDes,ExpenseDetail.ExpDate,ExpenseDetail.Amount FROM Expense JOIN ExpenseDetail ON ExpenseDetail.ExpID=Expense.ExpID JOIN ExpenseType ON ExpenseDetail.ExpTypeID=ExpenseType.ExpTypeID WHERE Expense.DateCreated='"+DateSearch.Value.ToShortDateString()+"'or ExpenseDetail.ExpDate='"+ DateSearch.Value.ToShortDateString()+"';";
                DT = Dom_SqlClass.retriveDataMultiTable(ST);
                GeneratData();
                DataExpense.ClearSelection();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataExpense.Rows.RemoveAt(0);
            }
            rndSearchID.Checked=true;
            txtSearch.Text = "";
        }

        private void DataExpense_SelectionChanged(object sender, EventArgs e)
        {
            if (DataExpense.SelectedRows.Count >= 1)
            {
                int i = DataExpense.SelectedRows[0].Index;
                txtID.Text = DataExpense.Rows[i].Cells[0].Value.ToString();
                DateNote.Value = DateTime.Parse(DataExpense.Rows[i].Cells[1].Value.ToString());
                cmType.SelectedItem = DataExpense.Rows[i].Cells[2].Value.ToString();
                txtName.Text = DataExpense.Rows[i].Cells[3].Value.ToString();
                DateNote.Value = DateTime.Parse(DataExpense.Rows[i].Cells[4].Value.ToString());
                txtAmount.Text = DataExpense.Rows[i].Cells[5].Value.ToString();
            }
            else
            {
                Clear();
            }
        }
    }
}
