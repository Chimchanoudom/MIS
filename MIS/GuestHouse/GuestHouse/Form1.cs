using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.Lib;
using Bunifu.Framework.UI;

namespace GuestHouse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Tslide.Start();
            Tdate.Start();
            lblName.Text = UserLoginDetail.fName +" "+ UserLoginDetail.lName;
            label2.Dock = DockStyle.Left;
            lblposition2.Text = UserLoginDetail.position;
            lblposition2.Dock = DockStyle.Left;
            lblPosition.Visible = false;
        }
       
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnstay_Click_1(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnStay, 48, 3);
            //btnstay.Normalcolor = Color.Green;
            //ClickChang(btnstay.Name);
        }

        private void btnbook_Click_1(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnBook, 48, 3);
            //btnbook.Normalcolor = Color.Green;
            //ClickChang(btnbook.Name);
        }

    
        private void btnbooknote_Click_1(object sender, EventArgs e)
        {
            Booking bk = new Booking();
            bk.ShowDialog();
        }

        private void btnbookData_Click_1(object sender, EventArgs e)
        {
            BookingData BD = new BookingData();
            BD.ShowDialog();
        }


        private void btncheckin_Click(object sender, EventArgs e)
        {
            checkin CI = new checkin();
            CI.ShowDialog();
        }

        private void btncheckINData_Click(object sender, EventArgs e)
        {
            CheckInData CID = new CheckInData();
            CID.ShowDialog();
        }

        private void btncheckoutNote_Click(object sender, EventArgs e)
        {
            dom_Design.dropdown(pncheckout, 48, 3);
            //btncheckoutNote.Normalcolor = Color.Green;
            //ClickChang(btncheckoutNote.Name);
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            CheckOut CO = new CheckOut();
            CO.ShowDialog();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            CheckoutData COD = new CheckoutData();
            COD.ShowDialog();
        }

        private void btnRoom_Click_2(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnRoom, 48, 4);
            //btnRoom.Normalcolor = Color.Green;
            //ClickChang(btnRoom.Name);
        }

        private void btnCheckRoom_Click_2(object sender, EventArgs e)
        {
            CheckRoom CR = new CheckRoom();
            CR.ShowDialog();
        }

        private void btnAddRoom_Click_2(object sender, EventArgs e)
        {
            RoomData RD = new RoomData();
            RD.ShowDialog();
        }

        private void btnRoomPrice_Click_2(object sender, EventArgs e)
        {
            RoomPrice RP = new RoomPrice();
            RP.ShowDialog();
        }

        private void btnCustomer_Click_2(object sender, EventArgs e)
        {
            dom_Design.dropdown(pncustomer,48,2);
            //btnCustomer.Normalcolor = Color.Green;
            //ClickChang(btnCustomer.Name);
        }

        private void btnDataCustomer_Click_2(object sender, EventArgs e)
        {
            Customer CUS = new Customer();
            CUS.ShowDialog();
        }

        private void btnEmployee_Click_2(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnEmployee, 48, 2);
            //btnEmployee.Normalcolor = Color.Green;
            //ClickChang(btnEmployee.Name);
        }

        private void btnEmployeeData_Click_2(object sender, EventArgs e)
        {
            Employee EMP = new Employee();
            EMP.ShowDialog();
        }

        private void btnExpenAndIncome_Click_2(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnExpent,48,4);
            //btnExpenAndIncome.Normalcolor= Color.Green;
            //ClickChang(btnExpenAndIncome.Name);
        }

        private void btnNoteExpen_Click_2(object sender, EventArgs e)
        {
            Expense EXP = new Expense();
            EXP.ShowDialog();
        }

        private void btnExpensType_Click_2(object sender, EventArgs e)
        {
            expensType EXT = new expensType();
            EXT.ShowDialog();
        }

        private void btnPaymentAndIncome_Click_2(object sender, EventArgs e)
        {
            ExpensAndIncome EAI = new ExpensAndIncome();
            EAI.ShowDialog();
        }

        private void Tslide_Tick(object sender, EventArgs e)
        {
            if (ph11.Visible == true)
            {
                Transition1.ShowSync(ph11);
                ph11.Visible = false;
                ph31.Visible = true;
               
            }
            else if(ph31.Visible==true)
            {
                Transition3.ShowSync(ph31);
                
                ph11.Visible = false;
                ph21.Visible = true;
                ph31.Visible = false;
            }
            else
            {
                Transition2.ShowSync(ph21);
                ph11.Visible = true;
                ph21.Visible = false;
                ph31.Visible = false;
            }

        }

        private void btnAllReport_Click(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnReport,48,3);
        }

        private void btnPlayAndStop_Click(object sender, EventArgs e)
        {
            if (btnPlayAndStop.Text == "បញ្ឈប់ចលនា")
            {
                Tslide.Stop();
                btnPlayAndStop.Text = "ដំណើរកាចលនា";
            }
            else
            {
                Tslide.Start();
                btnPlayAndStop.Text = "បញ្ឈប់ចលនា";
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnSetting,48,3);
        }

        private void Tdate_Tick(object sender, EventArgs e)
        {
            LBLTime.Text = DateTime.Now.ToLongDateString()+"\n"+DateTime.Now.ToShortTimeString();
        }
    }
}
