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
    public partial class checkin : Form
    {
        public checkin()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkin_Load(object sender, EventArgs e)
        {
            Dom_SqlClass.FillItemToCombobox(@"SELECT CusID,CONCAT(Customer.Fname,' ',Customer.Lname) AS 'fullName' FROM Customer;", "CusID", "fullName", cmCustomer);


        }

        private void rnd3_CheckedChanged(object sender, EventArgs e)
        {
            double time = 0;
            datecheckout.Enabled = true;

            if (rnd3.Checked)
                time = 3;
            else if (rnd6.Checked)
                time = 6;
            else if (rnd12.Checked)
                time = 12;
            else if (rnd24.Checked)
                time = 24;

            datecheckout.Value = datecheckin.Value.AddHours(time);
            datecheckout.Enabled = false;
          //  MessageBox.Show(datecheckout.Value.ToString("dd/MM/yyyy")+" " +datecheckout.Value.ToShortTimeString());
        }

        private void datecheckin_ValueChanged(object sender, EventArgs e)
        {
            if (rnd3.Checked || rnd6.Checked || rnd12.Checked)
            {
                rnd3_CheckedChanged(null, null);
            }
        }

        private void rndover24_CheckedChanged(object sender, EventArgs e)
        {
            datecheckout.Enabled = true;
        }

        private void cmCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
  //foreach(String s in cmCustomer.Items)
  //          {
  //              if (cmCustomer.Text != s)
  //              {
  //                  txtIDCus.Text = "123";
  //              }
  //          }
        }

        private void cmCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            //int r = e.KeyChar;
            //MessageBox.Show(r.ToString());
            if (e.KeyChar == 13)
            {
                cmCustomer_MouseLeave(null, null);
            }
            
        }

        private void cmCustomer_MouseLeave(object sender, EventArgs e)
        {
            if (cmCustomer.SelectedIndex == -1)
            {
                txtIDCus.Text = "123";
            }
        }
    }
}
