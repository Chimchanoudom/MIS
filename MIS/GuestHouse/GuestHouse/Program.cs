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
<<<<<<< HEAD
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Employee());
            //if (new Login().ShowDialog() == DialogResult.OK)
            //{
            //    Application.Run(new Form1());
=======
            //Application.SetCompatibleTextRenderingDefault(false);
            //if (new Login().ShowDialog() == DialogResult.OK)
            //{
                Application.Run(new Customer());
>>>>>>> e7039f84a5ca5982791aa33f37a11f9c31871d16
            //}
        }
    }
}
