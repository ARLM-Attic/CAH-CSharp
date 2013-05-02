using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CAH
{
    public partial class Card : UserControl
    {
        private Boolean isBlack,isUp;
        public readonly static int WIDTH=100, HIGHT=100;
        public event EventHandler onSelected;

        public Card( String s,bool black)
        {
            InitializeComponent();
            label1.MaximumSize = new Size(WIDTH-5, 0);
            isBlack = black;
            BackColor = Color.White;
           label1.Text = s;
           this.Width = WIDTH;
           this.Height = HIGHT;
        }

        public Card(String s, int requredInpuits)
        {
            InitializeComponent(); 
            isBlack = true;
            BackColor = Color.Black;
            label1.Text = s;
            label1.ForeColor = Color.White;
            label1.MaximumSize = new Size(WIDTH - 5, 0);
            this.Width = WIDTH;
            this.Height = HIGHT;
        }

        public void setPostion(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        public bool isABlackCard()
        {
            return isBlack;
        }

        public void flip()
        {
            isUp = !isUp;
            if (!isUp)
            {
                label1.Hide();
            }
            else
                label1.Show();
        }
        public string getText()
        {
            return label1.Text;
        }
       

        private void Card_Click(object sender, EventArgs e)
        {
            onSelected.Invoke(this, new EventArgs());
        }


       

        


    }
}
