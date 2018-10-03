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
    public partial class CheckOut : Form
    {
        public CheckOut()
        {
            InitializeComponent();
        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
                   }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            CheckInData CI = new CheckInData();
            CI.Show();
            
        }
        string sql;
        SqlDataReader dataReader;
        private void CheckOut_Load(object sender, EventArgs e)
        {
            sql = "exec NewGetAutoID 'CheckOutID','_','CheckOut';";
            dataCon.Con.Open();
            dataReader = dataCon.ExecuteQry(sql);
            dataReader.Read();
            int id = dataReader.GetInt32(0);
            txtID.Text = "Checkout_" + id.ToString("000");
            dataCon.Con.Close();

            dtCheckOut.Value = DateTime.Now;
            txtEmpID.Text = UserLoginDetail.empID;
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
