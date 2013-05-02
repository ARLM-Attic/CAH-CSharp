using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;
using System.Threading;
using System.Diagnostics;

namespace CAH
{
    public partial class Form1 : Form
    {
        ////////Fields\\\\\\\\
        private Player player;
        private String userName = "";
        private Game game = null;
        public Form1()
        {
           
            InitializeComponent();

            new InputDialog("Username", this).Show();
            WindowState = FormWindowState.Minimized;
            

        }

        public void setUserName(String s)
        {
            userName = s;
            label1.Text = userName;
            WindowState = FormWindowState.Normal;
            player = new Player(userName);
            player.cardChange += player_cardChange;
        }

        void player_cardChange(object sender, EventArgs e)
        {
            doCards();
        }

        private void doCards()
        {
            int i = 0;
            foreach(Card card in player.getCards()){
                card.setPostion(i * Card.WIDTH, 0);
                playerCards.Controls.Add(card);
                i++;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();

        }

        private void playerCards_Paint(object sender, PaintEventArgs e)
        {

        }

        private void enterIpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ipInput(player.getClient());
            startGameToolStripMenuItem.Enabled = true;
        }

        private void beTheServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startGameToolStripMenuItem.Enabled = true;
            game = new Game();
        }

        private void startGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.startGame();
        }

        
    }
}
