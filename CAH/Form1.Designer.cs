namespace CAH
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.GameCards = new System.Windows.Forms.FlowLayoutPanel();
            this.playerCards = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.conncetionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchForIpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enterIpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beTheServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(513, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // GameCards
            // 
            this.GameCards.Location = new System.Drawing.Point(26, 22);
            this.GameCards.Name = "GameCards";
            this.GameCards.Size = new System.Drawing.Size(332, 100);
            this.GameCards.TabIndex = 1;
            // 
            // playerCards
            // 
            this.playerCards.Location = new System.Drawing.Point(26, 150);
            this.playerCards.Name = "playerCards";
            this.playerCards.Size = new System.Drawing.Size(332, 100);
            this.playerCards.TabIndex = 2;
            this.playerCards.Paint += new System.Windows.Forms.PaintEventHandler(this.playerCards_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conncetionToolStripMenuItem,
            this.gameToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(651, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // conncetionToolStripMenuItem
            // 
            this.conncetionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchForIpsToolStripMenuItem,
            this.enterIpToolStripMenuItem});
            this.conncetionToolStripMenuItem.Name = "conncetionToolStripMenuItem";
            this.conncetionToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.conncetionToolStripMenuItem.Text = "conncetion";
            // 
            // searchForIpsToolStripMenuItem
            // 
            this.searchForIpsToolStripMenuItem.Enabled = false;
            this.searchForIpsToolStripMenuItem.Name = "searchForIpsToolStripMenuItem";
            this.searchForIpsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.searchForIpsToolStripMenuItem.Text = "search for ips";
            // 
            // enterIpToolStripMenuItem
            // 
            this.enterIpToolStripMenuItem.Name = "enterIpToolStripMenuItem";
            this.enterIpToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.enterIpToolStripMenuItem.Text = "enter ip";
            this.enterIpToolStripMenuItem.Click += new System.EventHandler(this.enterIpToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beTheServerToolStripMenuItem,
            this.startGameToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.gameToolStripMenuItem.Text = "game";
            // 
            // beTheServerToolStripMenuItem
            // 
            this.beTheServerToolStripMenuItem.Name = "beTheServerToolStripMenuItem";
            this.beTheServerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.beTheServerToolStripMenuItem.Text = "be the server";
            this.beTheServerToolStripMenuItem.Click += new System.EventHandler(this.beTheServerToolStripMenuItem_Click);
            // 
            // startGameToolStripMenuItem
            // 
            this.startGameToolStripMenuItem.Enabled = false;
            this.startGameToolStripMenuItem.Name = "startGameToolStripMenuItem";
            this.startGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startGameToolStripMenuItem.Text = "start game";
            this.startGameToolStripMenuItem.Click += new System.EventHandler(this.startGameToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 262);
            this.Controls.Add(this.playerCards);
            this.Controls.Add(this.GameCards);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel GameCards;
        private System.Windows.Forms.FlowLayoutPanel playerCards;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem conncetionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchForIpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enterIpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beTheServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startGameToolStripMenuItem;
    }
}

