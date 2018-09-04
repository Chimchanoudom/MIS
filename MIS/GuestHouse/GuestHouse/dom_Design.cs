using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuestHouse
{
   static class dom_Design
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
    }
}