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
    public partial class BookingData : Form
    {
        public BookingData()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BookingData_Resize(object sender, EventArgs e)
        {
            dom_Design.resizewidth(this.Width, new Control[] { groupBox1, groupBox2 }, 2);
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            BookingData BD = new BookingData();
            BD.Close();
            checkin CI = new checkin();
            CI.Show();


        }
    }
}
