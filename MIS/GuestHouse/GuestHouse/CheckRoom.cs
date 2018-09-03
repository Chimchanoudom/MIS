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
    public partial class CheckRoom : Form
    {
        public CheckRoom()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckRoom_Load(object sender, EventArgs e)
        {
            Desing.resizewidth(pnList.Width, new Control[] {GroupFree,Groupbusy }, 2);
        }
    }
}
