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

            //Application.Run(new Employee());
<<<<<<< HEAD
            //if (new Login().ShowDialog() == DialogResult.OK)
            //{
            //    Application.Run(new Form1());
            //}


            //Application.Run(new Employee());
=======
>>>>>>> f880db08ebbf43b1aa144caee7c80ed60f90d538

            // Application.Run(new Employee());

            if (new Login().ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Form1());
            }

            //Application.SetCompatibleTextRenderingDefault(false);
            //if (new Login().ShowDialog() == DialogResult.OK)
            //{

            //  Application.Run(new RoomData());

            //Application.Run(new Customer());
            //Application.Run(new RoomData());
            //Application.Run(new RoomPrice());
            //Application.Run(new CheckRoom());
            //Application.Run(new expensType());

            //Application.Run(new RoomData());




            //Application.Run(new Expense());

            //Application.Run(new Form1());
            //Application.Run(new Customer());
            //Application.Run(new ExpensAndIncome());
            //Application.Run(new Booking());
            //Application.Run(new BookingData());
            //Application.Run(new ExpensAndIncome());
            //Application.Run(new ExpensAndIncome());
            //Application.Run(new ExpensAndIncome());
        }


    }
    }

