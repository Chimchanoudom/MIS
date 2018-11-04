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
    public partial class CheckOut : Form
    {
        public CheckOut()
        {
            InitializeComponent();
        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
                   }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            DataForCheckOut forCheckOut = new DataForCheckOut(dtCheckInDetail);
            forCheckOut.ShowDialog();

            DataRow[] rows = dtCheckInDetail.Select("checked=true");

            foreach(DataRow dr in rows)
            {
                DateTime dtStart = dataCon.ConvertStringToDateTime(dr["ថ្ងៃចូលស្នាក់នៅ"]+"");
                DateTime dtEnd = dataCon.ConvertStringToDateTime(dr["ថ្ងៃចាកចេញ"] + "");
                int hour=0;
                double roomPrice,electricity,subtotal;
                roomPrice = electricity = subtotal = 0;
                bool pickAc = dr["ជម្រើស"] + "" == "ម៉ាស៊ីនត្រជាក់";

                dataCon.CalculatePrice(dtStart, dtEnd, ref hour,dr["ប្រភេទបន្ទប់"]+"",ref roomPrice,pickAc,ref electricity,ref subtotal);

                dgvCheckoutDetail.Rows.Add(dr["ថ្ងៃចូលស្នាក់នៅ"],dr["ថ្ងៃចាកចេញ"],dr["ប្រភេទម៉ោង"], dr["ប្រភេទបន្ទប់"],dr["លេខបន្ទប់"],dr["ជម្រើស"],electricity,roomPrice,subtotal);
            }
        }
        string sql;
        SqlDataReader dataReader;
        DataTable dtCheckInDetail=new DataTable();
        private void CheckOut_Load(object sender, EventArgs e)
        {
            sql = "exec NewGetAutoID 'CheckOutID','_','CheckOut';";
            dataCon.Con.Open();
            dataReader = dataCon.ExecuteQry(sql);
            dataReader.Read();
            int id = dataReader.GetInt32(0);
            txtID.Text = "Checkout_" + id.ToString("000");
            dataCon.Con.Close();

            dtCheckOut.Value = DateTime.Now;
            txtEmpID.Text = UserLoginDetail.empID;

            sql = @"select m.CheckInID as 'លេខកូដសម្គាល់',format(CheckInDate,'dd/MM/yyyy hh:mm tt') as 'ថ្ងៃចូលស្នាក់នៅ',format(CheckOutDate,'dd/MM/yyyy hh:mm tt') as 'ថ្ងៃចាកចេញ',RoomTypeDesc as 'ប្រភេទបន្ទប់',           case PickAc            when 0 then N'កង្ហា'COLLATE Latin1_General_100_CI_AI           else N'ម៉ាស៊ីនត្រជាក់'COLLATE Latin1_General_100_CI_AI           end as 'ជម្រើស',RoomID as 'លេខបន្ទប់',HourType as 'ប្រភេទម៉ោង',concat(fname, ' ', lname) as 'អតិថិជន',phone as 'លេខទូរស័ព្ទ'             from CheckInDetail d join CheckIn m on d.CheckInID = m.CheckInID join Customer c on m.CusID = c.CusID            where status = 'Success'; ";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, dataCon.Con);
            
            dataAdapter.Fill(dtCheckInDetail);


            dtCheckInDetail.Columns.Add(new DataColumn("Checked", typeof(Boolean)));

            int lastIndexInDt = dtCheckInDetail.Columns.Count - 1;

            for (int i = 0; i < lastIndexInDt; i++)
                dtCheckInDetail.Columns[i].ReadOnly = true;

            dtCheckInDetail.Columns[lastIndexInDt].SetOrdinal(0);
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
