using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CAH
{
    class CardEncoder
    {

        public String encodeCard(Card c){
            String s = "";
            if (c.isABlackCard())
                s += "Black";
            else
                s += "White";
            s += ","+c.getText();
            return s;
        }


        public Card decodeCard(String s)
        {
            Card c = null;
           
            String[] strings = s.Split(',');
            try
            {
                c = new Card(strings[1].TrimEnd('\0'), strings[0].Equals("Black"));
            }
            catch (Exception ex)
            {
               Debug.WriteLine( ex.StackTrace);
               return new Card("this was a fail",false);
            }
            return c;
        }
    }
}
