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
        }
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized == true)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnstay_Click(object sender, EventArgs e)
        {
            Desing.dropdown(pnStay, 48, 3);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            BunifuFlatButton[] btn = { btnstay,btnbook,btnRoom,btnCustomer,btnEmployee,btnExpenAndIncome};
            foreach(BunifuFlatButton button in btn)
            {
                
            }
        }

        private void btnbook_Click(object sender, EventArgs e)
        {
            Desing.dropdown(pnBook, 48, 3);
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            Desing.dropdown(pnRoom, 48, 4);
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Desing.dropdown(pncustomer, 48, 2);
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            Desing.dropdown(pnEmployee, 48, 2);
        }

        private void btnExpenAndIncome_Click(object sender, EventArgs e)
        {
            Desing.dropdown(pnExpent, 48, 4);
        }

        private void btnDataCustomer_Click(object sender, EventArgs e)
        {
            Customer cus = new Customer();
            cus.ShowDialog();
        }

        private void btnEmployeeData_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.ShowDialog();
        }

        private void btnNoteExpen_Click(object sender, EventArgs e)
        {
            Expense EX = new Expense();
            EX.ShowDialog();
        }

        private void btnExpensType_Click(object sender, EventArgs e)
        {
            expensType expt = new expensType();
            expt.ShowDialog();
        }

        private void btnPaymentAndIncome_Click(object sender, EventArgs e)
        {
            ExpensAndIncome eai = new ExpensAndIncome();
            eai.ShowDialog();
        }

        private void btnCheckRoom_Click(object sender, EventArgs e)
        {
            CheckRoom cr = new CheckRoom();
            cr.ShowDialog();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            RoomData Rd = new RoomData();
            Rd.ShowDialog();
        }

        private void btnRoomPrice_Click(object sender, EventArgs e)
        {
            RoomPrice RP = new RoomPrice();
            RP.ShowDialog();
        }

        private void btnbooknote_Click(object sender, EventArgs e)
        {
            Booking BK = new Booking();
            BK.ShowDialog();
        }

        private void btnbookData_Click(object sender, EventArgs e)
        {
            BookingData BKD = new BookingData();
            BKD.ShowDialog();
        }
        private void btnstay_Click_1(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnStay, 48, 3);
        }

        private void btnbook_Click_1(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnBook, 48, 3);
        }

        private void btnRoom_Click_1(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnRoom, 48, 4);
        }

        private void btnCustomer_Click_1(object sender, EventArgs e)
        {
            dom_Design.dropdown(pncustomer, 48, 2);
        }

        private void btnEmployee_Click_1(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnEmployee, 48, 2);
        }

        private void btnExpenAndIncome_Click_1(object sender, EventArgs e)
        {
            dom_Design.dropdown(pnExpent, 48, 4);
        }

        private void btnbooknote_Click_1(object sender, EventArgs e)
        {
            Booking bk = new Booking();
        }

        private void btnbookData_Click_1(object sender, EventArgs e)
        {
            BookingData BD = new BookingData();
            BD.ShowDialog();
        }

        private void btnRoomPrice_Click_1(object sender, EventArgs e)
        {
            RoomPrice RP = new RoomPrice();
            RP.ShowDialog();
        }

        private void btnAddRoom_Click_1(object sender, EventArgs e)
        {
            RoomData RD = new RoomData();
            RD.ShowDialog();
        }

        private void btnCheckRoom_Click_1(object sender, EventArgs e)
        {
            CheckRoom CR = new CheckRoom();
            CR.ShowDialog();
        }

        private void btnDataCustomer_Click_1(object sender, EventArgs e)
        {
            Customer Cus = new Customer();
            Cus.ShowDialog();
        }

        private void btnEmployeeData_Click_1(object sender, EventArgs e)
        {
            Employee Emp = new Employee();
            Emp.ShowDialog();
        }

        private void btnNoteExpen_Click_1(object sender, EventArgs e)
        {
            Expense EXP = new Expense();
            EXP.ShowDialog();
        }

        private void btnExpensType_Click_1(object sender, EventArgs e)
        {
            expensType EXT = new expensType();
            EXT.ShowDialog();
        }
        
        private void btnPaymentAndIncome_Click_1(object sender, EventArgs e)
        {
            ExpensAndIncome EAI = new ExpensAndIncome();
            EAI.ShowDialog();
        }
    }
}
