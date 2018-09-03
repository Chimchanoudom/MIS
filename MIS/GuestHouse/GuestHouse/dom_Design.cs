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
            if (!((num >= 65 && num <= 90) || (num >= 97 && num <= 122) || (num == 8 || num == 32)))
            {
                e.KeyChar='\0';
            }
        }
    }
}