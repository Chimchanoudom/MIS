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
    public partial class CheckIn : Form
    {
        public CheckIn()
        {
            InitializeComponent();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            BookingData BD = new BookingData();
            BD.ShowDialog();
            this.Hide();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}
