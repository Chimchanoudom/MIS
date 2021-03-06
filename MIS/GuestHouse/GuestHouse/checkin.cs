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
    public partial class checkin : Form
    {
        public checkin()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void FillItem()
        {
            Dom_SqlClass.FillItemToCombobox(@"SELECT CusID,CONCAT(Customer.Fname,' ',Customer.Lname) AS 'fullName' FROM Customer;", "CusID", "fullName", cmCustomer);

        }
        private void checkin_Load(object sender, EventArgs e)
        {
            //FillItem();
           txtEmpID.Text= UserLoginDetail.empID;
        }

        private void rnd3_CheckedChanged(object sender, EventArgs e)
        {
            double time = 0;
            datecheckout.Enabled = true;

            if (rnd3.Checked)
                time = 3;
            else if (rnd6.Checked)
                time = 6;
            else if (rnd12.Checked)
                time = 12;
            else if (rnd24.Checked)
                time = 24;

            datecheckout.Value = datecheckin.Value.AddHours(time);
            datecheckout.Enabled = false;
             
          //  MessageBox.Show(datecheckout.Value.ToString("dd/MM/yyyy")+" " +datecheckout.Value.ToShortTimeString());
        }

        private void datecheckin_ValueChanged(object sender, EventArgs e)
        {
            if (rnd3.Checked || rnd6.Checked || rnd12.Checked)
            {
                rnd3_CheckedChanged(null, null);
            }
        }

        private void rndover24_CheckedChanged(object sender, EventArgs e)
        {
            datecheckout.Enabled = true;
        }

        private void cmCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            //int r = e.KeyChar;
            //MessageBox.Show(r.ToString());
            if (e.KeyChar == 13)
            {
                cmCustomer_MouseLeave(null, null);
            }
            
        }

        private void cmCustomer_MouseLeave(object sender, EventArgs e)
        {
            
        }
        String IDCus="",IDcheckIN="";

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btngetPrice_Click(object sender, EventArgs e)
        {
            dataCon.GetPrice();
            int Over24 = 0;
            int TimeGreater = 0;
            int TimeLess = 0;

            String[] Time = new string[2];
            String  Type = "";
            String Room = "";
            double Roomprice = 0;
            double electricity = 0;
            double subtotal = 0;
            foreach (RadioButton rnd in pnhr.Controls)
            {
                if (rnd.Checked && rnd.Name != rndover24.Name)
                { Time = rnd.Text.Split(' ');
                }
                else
                {
                    TimeSpan Over = (datecheckout.Value - datecheckin.Value);
                    int over = (int)Over.TotalHours;
                    if (Over.TotalHours > 24)
                    {
                        while (over > 24)
                        {
                            TimeGreater += 1;
                            over -= 24;
                            TimeLess = over;
                            
                        }
                    }
                    break;
                }
            }
            foreach (RadioButton rnd in pnRoom.Controls)
            {
                if (rnd.Checked)
                    Type = RndSingle.Name.Substring(3);
            }
            Room = CmRoomNum.SelectedItem.ToString();
            //  MessageBox.Show(Time[0] + " " + Type+" "+Room+" "+Over24);
            if (TimeGreater == 0)
            {
                int time = int.Parse(Time[0]);
                dataCon.CalculatePrice(datecheckin.Value, datecheckout.Value, ref time, "Single", ref Roomprice, RndFan.Checked = true ? true : false, ref electricity, ref subtotal);
                //dataCon.CalculatePrice(datecheckin.Value, datecheckout.Value, Type,ref Roomprice,true,ref electricity,ref subtotal);
                //MessageBox.Show("RoomPrice="+Roomprice+" \n Time="+time+"\n Elictricity Price= "+electricity+" \nSubtotal="+subtotal);
            }
            else
            {
                int less = 0;
                int Oneday = 24;
                double totalRoomprice =0, totalElictricity = 0,TotalSub=0;
                if (TimeLess > 12)
                    less = 24;
                else if (TimeLess > 6)
                    less = 12;
                else if (TimeLess > 3)
                    less = 6;
                else
                    less = 3;
                dataCon.CalculatePrice(datecheckin.Value, datecheckout.Value, ref Oneday , Type, ref Roomprice, RndFan.Checked = true ? true : false, ref electricity, ref subtotal);
               //totalRoomprice= Roomprice *(double)TimeGreater;
               // totalElictricity=electricity *(double) TimeGreater;
               // TotalSub = subtotal * (double)TimeGreater;
               // Roomprice = 0;
               // electricity = 0;
               // subtotal = 0;
               // dataCon.CalculatePrice(datecheckin.Value, datecheckout.Value, ref less, Type, ref Roomprice, RndFan.Checked = true ? true : false, ref electricity, ref subtotal);
               // totalElictricity += electricity;
               // totalRoomprice += Roomprice;
               // TotalSub += subtotal;

                MessageBox.Show("RoomPrice="+Roomprice+" \n Time="+((TimeGreater*24)+TimeLess)+"\n Elictricity Price= "+electricity+" \nSubtotal="+subtotal);
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void cmCustomer_TextChanged(object sender, EventArgs e)
        {
            if (cmCustomer.SelectedIndex == -1)
            {
                foreach (String S in cmCustomer.Items)
                {
                    if (cmCustomer.Text.ToLower() != S.ToLower())
                    {
                        if (IDCus == "")
                        {
                            IDCus = dom_Design.GenerateID(Dom_SqlClass.GetIDFromDB("cusID", "_", "customer"), "Cus_00");
                        }
                        else
                        {
                            IDCus = dom_Design.GenerateID(IDCus, "Cus_00");
                        }
                        txtIDCus.Text = IDCus;
                    }
                }
            }
            else
            {
                
            }
        }
    }
}
