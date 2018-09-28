using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuestHouse
{
  class dom_Design:Desing
    {
        public static void NumberOnly(KeyPressEventArgs e)
        {
            int num = e.KeyChar;
            if (!(num>=48&&num<=57))
            {
                e.KeyChar='\0';
            }
        }
        public static void CharaterOnly(KeyPressEventArgs e)
        {
            int ch = e.KeyChar;
            
            if (!((ch >= 65 && ch <= 90) || (ch >= 97 && ch <= 122) || (ch == 8 || ch == 32)))
            {
                e.KeyChar=('\0');
            }
        }
        public static void GenerateColumHeader(String[] ColumnHeader, int numberColum, DataGridView Data)
        {
            for (int i = 0; i < numberColum; i++)
            {
                Data.Columns[i].HeaderText = ColumnHeader[i];
            }
        }

        public static String GenerateID(string ID,String Suffix) 
        {
            int num = 0;
            if (ID == "")
                num = 1;
            else
            {
                try
                {
                    num = int.Parse(ID) + 1;
                }
                catch
                {
                    num = int.Parse(ID.Substring(5));
                }
            }
            if (Suffix != null)
            {
                ID = Suffix + (num);
            }
            return ID;
        }

        public static void ColumnName(DataGridView data,int column,String[]columnName)
        {
            
            for (int i = 0; i < column; i++)
            {
                data.Columns[i].Name = columnName[i];
            }
        }
        public static String SetID(int indexsubstring,String ID,String suffix)
        {
            ID = dom_Design.GenerateID(ID.Substring(indexsubstring), suffix);
            return ID;
        }
    }
}