using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuestHouse.ss
{
    class RestrictionClass
    {
        public static class restrictFromKeyboard
        {
            public static void DisablerestrictAnyKeyFromKeyboard(TextBox txt)
            {
                txt.ContextMenuStrip = new TextBox().ContextMenuStrip;
                txt.ShortcutsEnabled = true;
                txt.KeyPress += Txt_KeyPress1;
            }

            private static void Txt_KeyPress1(object sender, KeyPressEventArgs e)
            {
                e.Handled = false;
            }

            public static void restrictAnyKeyFromKeyboard(TextBox txt)
            {
                ContextMenuStrip cms = new ContextMenuStrip();
                txt.ContextMenuStrip = cms;
                txt.ShortcutsEnabled = false;
                txt.KeyPress += Txt_KeyPress;
            }

            private static void Txt_KeyPress(object sender, KeyPressEventArgs e)
            {
                e.Handled = true;
            }

            public static void restrictAlphabet(KeyPressEventArgs e,string[] exception)
            {
                string exce = "";
                for (int i = 0; i < exception.Length; i++)
                    exce += exception[i];
                if (("1234569870០១២៣៤៥៦៧៨៩\b"+exce).IndexOf(e.KeyChar) < 0)
                    e.KeyChar = '\0';
            }

            public static void restrictNumberAndSigns(KeyPressEventArgs e)
            {
                if("1234569870'!@#$%^&*()_+][{}:;\"<,>.?/\\~`|=-.".IndexOf(e.KeyChar) >= 0)
                    e.KeyChar = '\0';
                
            }

            public static void restrictUnicodeAlphabets(KeyPressEventArgs e)
            {
                bool bAlpha=false;
                bool bNumeric = false;
                char st = e.KeyChar;
                int c = (int)st;
                if (!((c >= 65 && c < 91) || (c >= 97 && c < 123)))
                    bAlpha = true;//not English alphabet
                if ((("1234569870០១២៣៤៥៦៧៨៩\b").IndexOf(e.KeyChar) < 0))
                    bNumeric = true;//not number
                if(bAlpha&&bNumeric)
                    e.KeyChar = '\0';
            }
        }

        public static string GetIntFromKhNumber(string text)
        {
            string temp = "";
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '១':
                        temp += "1";
                        break;
                    case '២':
                        temp += "2";
                        break;
                    case '៣':
                        temp += "3";
                        break;
                    case '៤':
                        temp += "4";
                        break;
                    case '៥':
                        temp += "5";
                        break;
                    case '៦':
                        temp += "6";
                        break;
                    case '៧':
                        temp += "7";
                        break;
                    case '៨':
                        temp += "8";
                        break;
                    case '៩':
                        temp += "9";
                        break;
                    case '0':
                        temp += "0";
                        break;
                    default:
                        temp += text[i];
                        break;
                }
            }
            return temp;
        }

        public static string GetIntNumber(string text)
        {
            string temp = "";
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '1':
                        temp += "១";
                        break;
                    case '2':
                        temp += "២";
                        break;
                    case '3':
                        temp += "៣";
                        break;
                    case '4':
                        temp += "៤";
                        break;
                    case '5':
                        temp += "៥";
                        break;
                    case '6':
                        temp += "៦";
                        break;
                    case '7':
                        temp += "៧";
                        break;
                    case '8':
                        temp += "៨";
                        break;
                    case '9':
                        temp += "៩";
                        break;
                    case '0':
                        temp += "0";
                        break;
                    default:
                        temp += text[i];
                        break;
                }
            }

            return temp;
        }
    }
}
