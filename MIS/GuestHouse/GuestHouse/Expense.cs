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
        DataTable DT;
        private void Expense_Load(object sender, EventArgs e)
        {
            String Statement = @" SELECT Expense.ExpID,Expense.DateCreated,ExpenseType.ExpDesc,ExpenseDetail.ExpDes,ExpenseDetail.ExpDate,ExpenseDetail.Amount FROM Expense JOIN ExpenseDetail ON Expense.ExpID=ExpenseDetail.ExpID JOIN ExpenseType ON ExpenseType.ExpTypeID=ExpenseDetail.ExpTypeID;  ";        
            DT = Dom_SqlClass.retriveDataMultiTable(Statement);
            // ID = dom_Design.SetID(6, Dom_SqlClass.GetIDFromDB("ExpID", "_", "Expense"), "EXP_00");
           MessageBox.Show (Dom_SqlClass.GetIDFromDB("ExpID", "_", "Expense"));
        }
    }
}
