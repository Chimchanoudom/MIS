﻿using System;
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
    public partial class CheckInData : Form
    {
        public CheckInData()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            new CheckOut().Show();
            btnback_Click(null,null);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
