using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuestHouse
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Employee());
            //if (new Login().ShowDialog() == DialogResult.OK)
            //{
            //    Application.Run(new Form1());
            //}
            //Application.SetCompatibleTextRenderingDefault(false);
            //if (new Login().ShowDialog() == DialogResult.OK)
            //{

            //Application.Run(new RoomData());

            //  Application.Run(new Customer());

            //}
            //Application.Run(new RoomData());
            //Application.Run(new RoomPrice());
            //Application.Run(new CheckRoom());
            //Application.Run(new expensType());
<<<<<<< HEAD
            Application.Run(new RoomData());
=======
            //Application.Run(new Expense());
>>>>>>> ea42a259d254475c0a0a5b78133c83ff5b11156a
            //Application.Run(new Form1());
            //Application.Run(new Customer());
            //Application.Run(new ExpensAndIncome());
            //Application.Run(new Booking());
            Application.Run(new BookingData());
            //Application.Run(new ExpensAndIncome());
            //Application.Run(new ExpensAndIncome());
            //Application.Run(new ExpensAndIncome());

        }
    }
}
