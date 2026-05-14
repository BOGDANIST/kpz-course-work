namespace Client
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnHit = new System.Windows.Forms.Button();
            this.btnStand = new System.Windows.Forms.Button();
            this.btnLeave = new System.Windows.Forms.Button();
            this.lblGameStatus = new System.Windows.Forms.Label();
            this.panelTable = new System.Windows.Forms.Panel();
            this.lblRoundResult = new System.Windows.Forms.Label();
            this.panelChat = new System.Windows.Forms.Panel();
            this.lblChatHeader = new System.Windows.Forms.Label();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.panelTable.SuspendLayout();
            this.panelChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTable (ЦЕНТРАЛЬНИЙ СТІЛ)
            // 
            this.panelTable.BackColor = System.Drawing.Color.Transparent;
            this.panelTable.Controls.Add(this.panelChat); // Додаємо чат на стіл
            this.panelTable.Controls.Add(this.lblRoundResult);
            this.panelTable.Controls.Add(this.lblGameStatus);
            this.panelTable.Location = new System.Drawing.Point(0, 0);
            this.panelTable.Name = "panelTable";
            this.panelTable.Size = new System.Drawing.Size(1100, 650);
            this.panelTable.TabIndex = 0;
            this.panelTable.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTable_Paint);
            // 
            // lblGameStatus 
            // 
            this.lblGameStatus.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblGameStatus.ForeColor = System.Drawing.Color.Gold;
            this.lblGameStatus.Location = new System.Drawing.Point(400, 280);
            this.lblGameStatus.Name = "lblGameStatus";
            this.lblGameStatus.Size = new System.Drawing.Size(300, 30);
            this.lblGameStatus.TabIndex = 0;
            this.lblGameStatus.Text = "Очікування...";
            this.lblGameStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRoundResult
            // 
            this.lblRoundResult.BackColor = System.Drawing.Color.Transparent;
            this.lblRoundResult.Font = new System.Drawing.Font("Arial", 48F, System.Drawing.FontStyle.Bold);
            this.lblRoundResult.ForeColor = System.Drawing.Color.Yellow;
            this.lblRoundResult.Location = new System.Drawing.Point(200, 200);
            this.lblRoundResult.Name = "lblRoundResult";
            this.lblRoundResult.Size = new System.Drawing.Size(700, 100);
            this.lblRoundResult.TabIndex = 1;
            this.lblRoundResult.Text = "ВИГРАВ!";
            this.lblRoundResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRoundResult.Visible = false;
            // 
            // panelChat (ВІДЖЕТ ЧАТУ)
            // 
            this.panelChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panelChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChat.Controls.Add(this.lblChatHeader);
            this.panelChat.Controls.Add(this.richTextBoxChat);
            this.panelChat.Controls.Add(this.txtMessage);
            this.panelChat.Controls.Add(this.btnSend);
            this.panelChat.Location = new System.Drawing.Point(750, 250); // Початкова позиція
            this.panelChat.Name = "panelChat";
            this.panelChat.Size = new System.Drawing.Size(320, 270);
            this.panelChat.TabIndex = 2;
            // 
            // lblChatHeader (ЗАГОЛОВОК ДЛЯ ПЕРЕТЯГУВАННЯ)
            // 
            this.lblChatHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblChatHeader.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.lblChatHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChatHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblChatHeader.ForeColor = System.Drawing.Color.White;
            this.lblChatHeader.Location = new System.Drawing.Point(0, 0);
            this.lblChatHeader.Name = "lblChatHeader";
            this.lblChatHeader.Size = new System.Drawing.Size(318, 25);
            this.lblChatHeader.TabIndex = 0;
            this.lblChatHeader.Text = "≡ Чат (перетягніть) ≡";
            this.lblChatHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.richTextBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxChat.ForeColor = System.Drawing.Color.White;
            this.richTextBoxChat.Location = new System.Drawing.Point(10, 35);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.Size = new System.Drawing.Size(300, 190);
            this.richTextBoxChat.TabIndex = 1;
            this.richTextBoxChat.Text = "";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(10, 235);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(210, 20);
            this.txtMessage.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.LightGray;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Location = new System.Drawing.Point(230, 233);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(80, 24);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "OK";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnHit
            // 
            this.btnHit.BackColor = System.Drawing.Color.Goldenrod;
            this.btnHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnHit.Location = new System.Drawing.Point(20, 560);
            this.btnHit.Name = "btnHit";
            this.btnHit.Size = new System.Drawing.Size(150, 60);
            this.btnHit.TabIndex = 7;
            this.btnHit.Text = "ВЗЯТИ (HIT)";
            this.btnHit.UseVisualStyleBackColor = false;
            this.btnHit.Click += new System.EventHandler(this.btnHit_Click);
            // 
            // btnStand
            // 
            this.btnStand.BackColor = System.Drawing.Color.Firebrick;
            this.btnStand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnStand.ForeColor = System.Drawing.Color.White;
            this.btnStand.Location = new System.Drawing.Point(180, 560);
            this.btnStand.Name = "btnStand";
            this.btnStand.Size = new System.Drawing.Size(120, 60);
            this.btnStand.TabIndex = 8;
            this.btnStand.Text = "ПАС";
            this.btnStand.UseVisualStyleBackColor = false;
            this.btnStand.Click += new System.EventHandler(this.btnStand_Click);
            // 
            // btnLeave
            // 
            this.btnLeave.BackColor = System.Drawing.Color.DimGray;
            this.btnLeave.ForeColor = System.Drawing.Color.White;
            this.btnLeave.Location = new System.Drawing.Point(980, 10);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(100, 30);
            this.btnLeave.TabIndex = 12;
            this.btnLeave.Text = "Вийти з лобі";
            this.btnLeave.UseVisualStyleBackColor = false;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.btnHit);
            this.Controls.Add(this.btnStand);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.panelTable);
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WCF Blackjack - Poker Table";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.panelTable.ResumeLayout(false);
            this.panelChat.ResumeLayout(false);
            this.panelChat.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnHit;
        private System.Windows.Forms.Button btnStand;
        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblGameStatus;
        private System.Windows.Forms.Button btnLeave;
        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.Label lblRoundResult;

        // НОВІ ЕЛЕМЕНТИ ВІДЖЕТУ ЧАТУ
        private System.Windows.Forms.Panel panelChat;
        private System.Windows.Forms.Label lblChatHeader;
    }
}