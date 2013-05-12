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
        private Player player=new Player("tester");
        private String userName = "";
        private Game game = null;
        private bool needToUpdate = false;
        public Form1()
        {
           
            InitializeComponent();

            new InputDialog("Username", this).Show();
            WindowState = FormWindowState.Minimized;
            playerCards.Controls.Add(new Card("test",false));

        }

        public void setUserName(String s)
        {
            userName = s;
            label1.Text = userName;
            WindowState = FormWindowState.Normal;
            player = new Player(userName);
            player.cardChange += player_cardChange;
            VisualUpdater.Start();
        }

        void player_cardChange(object sender, EventArgs e)
        {
            needToUpdate = true;
        }

        private void doCards()
        {
            int i = playerCards.Controls.Count;
            foreach(Card card in player.getCards()){
                card.setPostion(i * Card.WIDTH, 0);
                playerCards.Controls.Add(card);
                i++;
            }
            Debug.WriteLine(i);
            Update();
            Refresh();
            needToUpdate = false;
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

        private void VisualUpdater_Tick(object sender, EventArgs e)
        {
            if(needToUpdate)
            doCards();
        }

        
    }
}
