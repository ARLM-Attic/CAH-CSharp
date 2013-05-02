using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAH
{
    public partial class InputDialog : Form
    {
        Form owner;
        public InputDialog(String use,Form f)
        {
            InitializeComponent();
            label1.Text = use;
            owner = f;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (owner is CAH.Form1)
            {
                CAH.Form1 temp = (CAH.Form1)owner;
                temp.setUserName(textBox1.Text);
                Dispose();
            }
                
        }
    }
}
