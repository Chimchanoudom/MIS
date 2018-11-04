using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuestHouse
{
    public partial class DataForCheckOut : Form
    {
        public DataForCheckOut()
        {
            InitializeComponent();
        }

        public DataForCheckOut(DataTable dtCheckInDetail)
        {
            this.dtCheckInDetail = dtCheckInDetail;
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
            string filter = "";
            if (rndID.Checked) filter = "លេខកូដសម្គាល់=";
            else if (rndName.Checked) filter = "អតិថិជន=";
            else if (rndTell.Checked) filter = "លេខទូរស័ព្ទ=";
            else if (rndRoomID.Checked) filter = "លេខបន្ទប់=";

            filter += "'" + txtSearch.Text + "'";

            dtCheckInDetail.DefaultView.RowFilter = filter;


        }
        DataTable dtCheckInDetail;
        

        private void DataForCheckOut_Load(object sender, EventArgs e)
        {
            
            dgvCheckInDetail.Columns.Clear();
           
            dgvCheckInDetail.DataSource = dtCheckInDetail;


            ckBox =new CheckBox();
            Rectangle rect =

               this.dgvCheckInDetail.GetCellDisplayRectangle(0, -1, true);
            rect.X = rect.Location.X + (rect.Width / 2);
            rect.Y= rect.Location.Y + (rect.Height /3);
            ckBox.Size = new Size(18, 18);
            ckBox.Location = rect.Location;
            ckBox.CheckedChanged += new EventHandler(ckBox_CheckedChanged);
            this.dgvCheckInDetail.Controls.Add(ckBox);

            dgvCheckInDetail.Columns[0].HeaderText = "";


            dgvCheckInDetail.Columns[1].DefaultCellStyle.Format = "yyyy/MM/dd hh:mm tt";
            dgvCheckInDetail.Columns[2].DefaultCellStyle.Format = "yyyy/MM/dd hh:mm tt";
        }

       

        private void ckBox_CheckedChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < dgvCheckInDetail.Rows.Count; i++)
            {
                dgvCheckInDetail.Rows[i].Cells[0].Value = ckBox.Checked;
            }
            dgvCheckInDetail.EndEdit();


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtCheckInDetail.DefaultView.RowFilter= string.Empty;
        }

        CheckBox ckBox;

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            dgvCheckInDetail.EndEdit();
            this.Close();
        }
    }
}
