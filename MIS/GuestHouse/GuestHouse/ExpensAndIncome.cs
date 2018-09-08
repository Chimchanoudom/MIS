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

        private void btngenerate_Click(object sender, EventArgs e)
        {
            dataExpense.Rows.Clear();
            dataProfit.Rows.Clear();
            double totalExpense = 0;
            double totalIncome = 0;
            string dateStart = DTdateStart.Value.Year + "/" + DTdateStart.Value.Month + "/" + DTdateStart.Value.Day ;
            string dateEnd = DTdateEnd.Value.Year + "/" + DTdateEnd.Value.Month + "/" + DTdateEnd.Value.Day;
            try
            {
                dataCon.Con.Open();
                string sqlCmd = @"SELECT format(ExpenseDetail.ExpDate,'yyyy/MM/dd') AS ExpDate,ExpenseType.ExpDesc,ExpenseDetail.Amount FROM ExpenseDetail join ExpenseType on ExpenseDetail.ExpTypeID=ExpenseType.ExpTypeID" + " WHERE ExpenseDetail.ExpDate>='"+dateStart+ "' AND ExpenseDetail.ExpDate<='"+dateEnd+"';";
                SqlDataReader dr = dataCon.ExecuteQry(sqlCmd);
                while (dr.Read())
                {
                    dataExpense.Rows.Add(dr["ExpDate"], dr["ExpDesc"], dr["Amount"]);
                    totalExpense += Convert.ToDouble(dr["Amount"]);
                }
                dr.Close();

                string sqlCmd1 = "SELECT CheckOutDate,Total FROM CheckOut" + " WHERE CheckOutDate >= '" + dateStart+ "' AND CheckOutDate <= '" + dateEnd+"'; ";
                dr = dataCon.ExecuteQry(sqlCmd1);
                while (dr.Read())
                {
                    dataExpense.Rows.Add(dr["CheckOutDate"], dr["Total"]);
                    totalIncome += Convert.ToDouble(dr["Total"]);
                }
                dr.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                dataCon.Con.Close();
            }

            txtTotalPayment.Text = totalExpense.ToString();
            txtTotalInCome.Text = totalIncome.ToString();
            double totalProfit = totalIncome - totalExpense;
            lblTotalProfit.Text = (totalProfit < 0) ? "សរុបប្រាក់ខាត" : "សរុបប្រាក់ចំណេញ";
            txttotalProfit.Text = (totalProfit < 0) ? totalProfit.ToString().Substring(1) : totalProfit.ToString();
        }
    }
}
