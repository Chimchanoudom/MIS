using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GuestHouse
{
   class Desing
    {
        public static void dropdown(Panel pn,int Hieght,int numberbutton)
        {
            if (pn.Size.Height == 48)
                pn.Size = new Size(pn.Size.Width, numberbutton * Hieght);
            else
                pn.Size = new Size(pn.Size.Width, 48);

        }
        public static void resizewidth(int widthParent,Control[]control,int numcontrol)
        {
            for (int i = 0; i < control.Length; i++)
            {
                control[i].Width = widthParent / numcontrol;
            }
        }
    }
}
