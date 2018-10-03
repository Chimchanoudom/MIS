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
    public partial class CheckoutData : Form
    {
        public CheckoutData()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string sql;
        SqlDataAdapter dataAdapter;
        DataTable dtCheckOut = new DataTable();
        DataTable dtCheckOutDetail = new DataTable();
        private void CheckoutData_Load(object sender, EventArgs e)
        {
            sql = "select CheckInDate as 'ថ្ងៃចូលស្នាក់នៅ',AppointedCheckOut,HourType as 'ប្រភេទម៉ោង',RoomID as 'លេខបន្ទប់',case pickac​​ ​​when 1 then 'AC' else 'Fan' end as 'ជម្រើស',Electricity as 'ថ្លៃភ្លើង',RoomPrice as 'តម្លៃបន្ទប់',SubTotal as 'តម្លៃសរុប',checkoutID from CheckOutDetail;";

            dataCon.Con.Open();
            dgvCheckOutDetail.Columns.Clear();
            dataAdapter = new SqlDataAdapter(sql, dataCon.Con);
            dataAdapter.Fill(dtCheckOutDetail);
            dgvCheckOutDetail.DataSource = dtCheckOutDetail;
            dataCon.Con.Close();

            dgvCheckOutDetail.Columns[8].Visible = false;




            sql = "select CheckOutID as 'លេខកូដសម្គាល់',FORMAT(CheckOUtDate,'dd/MM/yyyy hh:mm') as 'ថ្ងៃចាកចេញ',FORMAT(total,'c') as 'សរុប',CusID as 'លេខកូដសម្គាល់អតិថិជន',CONCAT(fname,' ',LName) as 'បុគ្គលិក' from checkout c join Employee e on c.EmpID= e.EmpID;";
            dataCon.Con.Open();
            dataAdapter = new SqlDataAdapter(sql, dataCon.Con);
            dataAdapter.Fill(dtCheckOut);
            dataCon.Con.Close();
            
            dgvCheckOut.Columns.Clear();
            dgvCheckOut.DataSource = dtCheckOut;

            dgvCheckOut.ClearSelection();
        }

        private void dgvCheckOut_SelectionChanged(object sender, EventArgs e)
        {
            string id, filter;
            if (dgvCheckOut.SelectedRows.Count > 0)
            {
                id = dgvCheckOut.SelectedRows[0].Cells[0].Value + "";
                filter = "checkoutID='" + id + "'";
            }
            else
            {
                filter = "តម្លៃសរុប=-1";
               
            }
            dtCheckOutDetail.DefaultView.RowFilter = filter;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
