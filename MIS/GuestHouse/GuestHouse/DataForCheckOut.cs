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
        DataTable dtCheckInDetail = new DataTable();
        string sql;

        private void DataForCheckOut_Load(object sender, EventArgs e)
        {
            sql = @"select m.CheckInID as 'លេខកូដសម្គាល់',CheckInDate as 'ថ្ងៃចូលស្នាក់នៅ',CheckOutDate as 'ថ្ងៃចាកចេញ',RoomTypeDesc as 'ប្រភេទបន្ទប់',
            case PickAc
            when 0 then N'កង្ហា'COLLATE Latin1_General_100_CI_AI
            else N'ម៉ាស៊ីនត្រជាក់'COLLATE Latin1_General_100_CI_AI
            end as 'ជម្រើស',RoomID as 'លេខបន្ទប់',HourType as 'ប្រភេទម៉ោង',concat(fname, ' ', lname) as 'អតិថិជន',phone as 'លេខទូរស័ព្ទ'
             from CheckInDetail d join CheckIn m on d.CheckInID = m.CheckInID join Customer c on m.CusID = c.CusID
             where status = 'Success'; ";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql,dataCon.Con);
            dgvCheckInDetail.Columns.Clear();
            dataAdapter.Fill(dtCheckInDetail);
            

            dtCheckInDetail.Columns.Add(new DataColumn(" ", typeof(Boolean)));
            int lastIndexInDt = dtCheckInDetail.Columns.Count - 1;

            for (int i = 0; i < lastIndexInDt; i++)
                dtCheckInDetail.Columns[i].ReadOnly = true;
       
            dtCheckInDetail.Columns[lastIndexInDt].SetOrdinal(0);
            dgvCheckInDetail.DataSource = dtCheckInDetail;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtCheckInDetail.DefaultView.RowFilter= string.Empty;
        }
    }
}
