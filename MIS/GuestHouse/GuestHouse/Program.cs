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
            //  Application.Run(new Customer());
            //}
            //Application.Run(new RoomData());
            //Application.Run(new RoomPrice());
            //Application.Run(new CheckRoom());
            //Application.Run(new expensType());
            Application.Run(new Expens());
            //Application.Run(new Form1());
            //Application.Run(new Customer());
            //Application.Run(new ExpensAndIncome());

        }
    }
}
