using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CAH;


namespace CAH
{
    public partial class ipInput : Form
    {
        Client owner=null;
        public ipInput(Client c)
        {
            InitializeComponent();
            owner = c;
            Show();
            
        }

        

        private void ipInput_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s="";
            s += textBox1.Text + ".";
            s += textBox2.Text + ".";
            s += textBox4.Text + ".";
            s += textBox3.Text;
            owner.connect(s);

            Dispose();
        }
    }
}
