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
    public partial class ExpensAndIncome : Form
    {
        public ExpensAndIncome()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExpensAndIncome_Load(object sender, EventArgs e)
        {
            Desing.resizewidth(pnList.Width, new Control[] { GroupExpens, GroupIncome }, 2);
        }
    }
}
