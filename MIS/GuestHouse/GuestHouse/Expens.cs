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
        DataTable DT2 = new DataTable();
        DataTable DTAll = new DataTable();
        String ID;
        private void Expens_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ghDataSet.ExpenseType' table. You can move, or remove it, as needed.
            this.expenseTypeTableAdapter.Fill(this.ghDataSet.ExpenseType);
            String Date = DateTime.Now.ToString("yyyy-MM-dd");
            ID = dom_Design.GenerateID(Dom_SqlClass.GetIDFromDB("ExpID", "_", "Expense"), "EXP_00");
            txtID.Text = ID;

        }
    }
}
