namespace Client
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxLobbies = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtLobbyName = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnCreateLobby = new System.Windows.Forms.Button();
            this.btnJoinLobby = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelLobbies = new System.Windows.Forms.Label();
            this.labelChat = new System.Windows.Forms.Label();
            this.labelNewLobby = new System.Windows.Forms.Label();
            this.lblGameStatus = new System.Windows.Forms.Label();
            this.listDealerCards = new System.Windows.Forms.ListBox();
            this.listMyCards = new System.Windows.Forms.ListBox();
            this.btnHit = new System.Windows.Forms.Button();
            this.btnStand = new System.Windows.Forms.Button();
            this.labelDealer = new System.Windows.Forms.Label();
            this.labelMyCards = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.ForeColor = System.Drawing.Color.White;
            this.labelName.Location = new System.Drawing.Point(20, 23);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(60, 13);
            this.labelName.TabIndex = 9;
            this.labelName.Text = "Ваше ім\'я:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 20);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGray;
            this.button1.Location = new System.Drawing.Point(265, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Підключитися";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelLobbies
            // 
            this.labelLobbies.AutoSize = true;
            this.labelLobbies.ForeColor = System.Drawing.Color.White;
            this.labelLobbies.Location = new System.Drawing.Point(20, 70);
            this.labelLobbies.Name = "labelLobbies";
            this.labelLobbies.Size = new System.Drawing.Size(73, 13);
            this.labelLobbies.TabIndex = 10;
            this.labelLobbies.Text = "Активні лобі:";
            // 
            // listBoxLobbies
            // 
            this.listBoxLobbies.Location = new System.Drawing.Point(20, 95);
            this.listBoxLobbies.Name = "listBoxLobbies";
            this.listBoxLobbies.Size = new System.Drawing.Size(320, 147);
            this.listBoxLobbies.TabIndex = 2;
            // 
            // btnJoinLobby
            // 
            this.btnJoinLobby.BackColor = System.Drawing.Color.LightGray;
            this.btnJoinLobby.Location = new System.Drawing.Point(20, 255);
            this.btnJoinLobby.Name = "btnJoinLobby";
            this.btnJoinLobby.Size = new System.Drawing.Size(320, 30);
            this.btnJoinLobby.TabIndex = 11;
            this.btnJoinLobby.Text = "Підключитися до вибраного лобі";
            this.btnJoinLobby.UseVisualStyleBackColor = false;
            this.btnJoinLobby.Click += new System.EventHandler(this.btnJoinLobby_Click);
            // 
            // labelNewLobby
            // 
            this.labelNewLobby.AutoSize = true;
            this.labelNewLobby.ForeColor = System.Drawing.Color.White;
            this.labelNewLobby.Location = new System.Drawing.Point(395, 70);
            this.labelNewLobby.Name = "labelNewLobby";
            this.labelNewLobby.Size = new System.Drawing.Size(107, 13);
            this.labelNewLobby.TabIndex = 12;
            this.labelNewLobby.Text = "Створити нове лобі:";
            // 
            // txtLobbyName
            // 
            this.txtLobbyName.Location = new System.Drawing.Point(398, 95);
            this.txtLobbyName.Name = "txtLobbyName";
            this.txtLobbyName.Size = new System.Drawing.Size(160, 20);
            this.txtLobbyName.TabIndex = 8;
            // 
            // btnCreateLobby
            // 
            this.btnCreateLobby.BackColor = System.Drawing.Color.LightGray;
            this.btnCreateLobby.Location = new System.Drawing.Point(570, 93);
            this.btnCreateLobby.Name = "btnCreateLobby";
            this.btnCreateLobby.Size = new System.Drawing.Size(90, 25);
            this.btnCreateLobby.TabIndex = 13;
            this.btnCreateLobby.Text = "Створити";
            this.btnCreateLobby.UseVisualStyleBackColor = false;
            this.btnCreateLobby.Click += new System.EventHandler(this.btnCreateLobby_Click);
            // 
            // lblGameStatus
            // 
            this.lblGameStatus.AutoSize = true;
            this.lblGameStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblGameStatus.ForeColor = System.Drawing.Color.Gold;
            this.lblGameStatus.Location = new System.Drawing.Point(395, 130);
            this.lblGameStatus.Name = "lblGameStatus";
            this.lblGameStatus.Size = new System.Drawing.Size(110, 18);
            this.lblGameStatus.TabIndex = 14;
            this.lblGameStatus.Text = "Очікування...";
            // 
            // labelDealer
            // 
            this.labelDealer.AutoSize = true;
            this.labelDealer.ForeColor = System.Drawing.Color.White;
            this.labelDealer.Location = new System.Drawing.Point(395, 160);
            this.labelDealer.Name = "labelDealer";
            this.labelDealer.Size = new System.Drawing.Size(81, 13);
            this.labelDealer.TabIndex = 15;
            this.labelDealer.Text = "Карти дилера:";
            // 
            // listDealerCards
            // 
            this.listDealerCards.Location = new System.Drawing.Point(398, 180);
            this.listDealerCards.Name = "listDealerCards";
            this.listDealerCards.Size = new System.Drawing.Size(262, 30);
            this.listDealerCards.TabIndex = 16;
            // 
            // labelMyCards
            // 
            this.labelMyCards.AutoSize = true;
            this.labelMyCards.ForeColor = System.Drawing.Color.White;
            this.labelMyCards.Location = new System.Drawing.Point(395, 220);
            this.labelMyCards.Name = "labelMyCards";
            this.labelMyCards.Size = new System.Drawing.Size(61, 13);
            this.labelMyCards.TabIndex = 17;
            this.labelMyCards.Text = "Мої карти:";
            // 
            // listMyCards
            // 
            this.listMyCards.Location = new System.Drawing.Point(398, 240);
            this.listMyCards.Name = "listMyCards";
            this.listMyCards.Size = new System.Drawing.Size(262, 43);
            this.listMyCards.TabIndex = 18;
            // 
            // labelChat
            // 
            this.labelChat.AutoSize = true;
            this.labelChat.ForeColor = System.Drawing.Color.White;
            this.labelChat.Location = new System.Drawing.Point(20, 300);
            this.labelChat.Name = "labelChat";
            this.labelChat.Size = new System.Drawing.Size(50, 13);
            this.labelChat.TabIndex = 19;
            this.labelChat.Text = "Чат лобі:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(20, 320);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(640, 120);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "";
            // 
            // btnHit
            // 
            this.btnHit.BackColor = System.Drawing.Color.Goldenrod;
            this.btnHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnHit.Location = new System.Drawing.Point(20, 450);
            this.btnHit.Name = "btnHit";
            this.btnHit.Size = new System.Drawing.Size(100, 30);
            this.btnHit.TabIndex = 21;
            this.btnHit.Text = "ВЗЯТИ";
            this.btnHit.UseVisualStyleBackColor = false;
            this.btnHit.Click += new System.EventHandler(this.btnHit_Click);
            // 
            // btnStand
            // 
            this.btnStand.BackColor = System.Drawing.Color.Firebrick;
            this.btnStand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnStand.ForeColor = System.Drawing.Color.White;
            this.btnStand.Location = new System.Drawing.Point(130, 450);
            this.btnStand.Name = "btnStand";
            this.btnStand.Size = new System.Drawing.Size(100, 30);
            this.btnStand.TabIndex = 22;
            this.btnStand.Text = "ПАС";
            this.btnStand.UseVisualStyleBackColor = false;
            this.btnStand.Click += new System.EventHandler(this.btnStand_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(20, 490);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(510, 20);
            this.textBox2.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightGray;
            this.button2.Location = new System.Drawing.Point(540, 487);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 26);
            this.button2.TabIndex = 4;
            this.button2.Text = "Відправити";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size(684, 531);
            this.Controls.Add(this.labelDealer);
            this.Controls.Add(this.listDealerCards);
            this.Controls.Add(this.labelMyCards);
            this.Controls.Add(this.listMyCards);
            this.Controls.Add(this.lblGameStatus);
            this.Controls.Add(this.btnHit);
            this.Controls.Add(this.btnStand);
            this.Controls.Add(this.labelChat);
            this.Controls.Add(this.btnCreateLobby);
            this.Controls.Add(this.labelNewLobby);
            this.Controls.Add(this.btnJoinLobby);
            this.Controls.Add(this.labelLobbies);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.txtLobbyName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.listBoxLobbies);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WCF Блекджек";
            this.Load += new System.EventHandler(this.MainMenuForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ListBox listBoxLobbies;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtLobbyName;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnCreateLobby;
        private System.Windows.Forms.Button btnJoinLobby;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelLobbies;
        private System.Windows.Forms.Label labelChat;
        private System.Windows.Forms.Label labelNewLobby;
        private System.Windows.Forms.Label lblGameStatus;
        private System.Windows.Forms.ListBox listDealerCards;
        private System.Windows.Forms.ListBox listMyCards;
        private System.Windows.Forms.Button btnHit;
        private System.Windows.Forms.Button btnStand;
        private System.Windows.Forms.Label labelDealer;
        private System.Windows.Forms.Label labelMyCards;
    }
}