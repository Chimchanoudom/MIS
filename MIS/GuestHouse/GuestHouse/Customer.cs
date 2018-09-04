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

            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            try
            {
                dataCon.Con.Open();
                MessageBox.Show("Connected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
                dataCon.Con.Close();
            }


            
        }
    }
}
