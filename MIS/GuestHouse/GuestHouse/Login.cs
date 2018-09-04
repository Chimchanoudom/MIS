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
                throw;
            }
            dataCon.Con.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
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
            }
            else
            {
                MessageBox.Show("Incorrected username or password!");             
               // this.DialogResult = DialogResult.No;

            }
        }
    }
}
