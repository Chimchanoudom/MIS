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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            loadUserAccounts();
        }


        public Dictionary<string, string> userAcc = new Dictionary<string, string>();
        public void loadUserAccounts()
        {
            try
            {
                dataCon.Con.Open();
                string sql = "select * from UserAcc;";
                SqlDataReader dr=dataCon.ExecuteQry(sql);
                while (dr.Read())
                {
                    try
                    {
                        userAcc.Add(dr.GetString(1), dr.GetString(2));
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Connect to Database!");
            }
            dataCon.Con.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            txtPassword_MouseLeave(txtPassword,null);
            txtPassword_MouseLeave(txtUsername, null);
            string inputPassword = txtPassword.Text;
            string inputUsername = txtUsername.Text;
            bool isCorrected = false;
            foreach(String userName in userAcc.Keys)
            {
                if (inputUsername.ToLower() == userName.ToLower() && inputPassword.ToLower() == userAcc[userName].ToLower())
                {
                    isCorrected = true;
                    break;
                }
            }
            if (isCorrected)
            {
                if (!UserLoginDetail.isActive((inputUsername).ToLower())){
                    MessageBox.Show("This account has been deactivated!");
                    return;
                }
                MessageBox.Show("Welcome "+UserLoginDetail.fName+ " "+UserLoginDetail.lName+"");
                this.DialogResult = DialogResult.OK;
                dataCon.GetPrice();
            }
            else
            {
                MessageBox.Show("Incorrected username or password!");             
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                btnLogin_Click(null, null);
            GuestHouse.ss.RestrictionClass.restrictFromKeyboard.restrictUnicodeAlphabets(e);
        }

        private void txtPassword_MouseLeave(object sender, EventArgs e)
        {
            TextBox txb = (TextBox)sender;
            txb.Text = GuestHouse.ss.RestrictionClass.GetIntFromKhNumber(txb.Text);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
    }
}
