using ChatLibrary;
using Client.ChatServiceReference;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{
    public partial class GameForm : Form
    {
        private MainMenuForm _mainMenu;

        // Змінні для перетягування чату
        private bool isDraggingChat = false;
        private Point chatDragStartPoint;

        public GameForm(MainMenuForm mainMenu)
        {
            InitializeComponent();
            _mainMenu = mainMenu;
            ChatServerConnector.GetInstance().ActiveGameForm = this;
            richTextBoxChat.Clear();

            // Підключаємо події миші для перетягування чату
            lblChatHeader.MouseDown += LblChatHeader_MouseDown;
            lblChatHeader.MouseMove += LblChatHeader_MouseMove;
            lblChatHeader.MouseUp += LblChatHeader_MouseUp;
        }

        // --- ЛОГІКА ПЕРЕТЯГУВАННЯ ЧАТУ ---
        private void LblChatHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDraggingChat = true;
                chatDragStartPoint = e.Location;
                panelChat.BringToFront(); // Піднімаємо чат поверх усього
            }
        }

        private void LblChatHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingChat)
            {
                // Рухаємо панель відносно її минулої позиції
                panelChat.Left += e.X - chatDragStartPoint.X;
                panelChat.Top += e.Y - chatDragStartPoint.Y;
            }
        }

        private void LblChatHeader_MouseUp(object sender, MouseEventArgs e)
        {
            isDraggingChat = false;
        }

        // --- МАЛЮЄМО СТІЛ ПРОГРАМНО ---
        private void panelTable_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush tableBrush = new SolidBrush(Color.FromArgb(20, 100, 40));
            Pen borderPen = new Pen(Color.FromArgb(60, 30, 10), 20);
            Rectangle tableRect = new Rectangle(50, 30, panelTable.Width - 100, panelTable.Height - 60);
            g.FillEllipse(tableBrush, tableRect);
            g.DrawEllipse(borderPen, tableRect);
        }

        private Panel CreateVisualCard(string cardString)
        {
            if (string.IsNullOrEmpty(cardString)) return new Panel();

            Panel cardPanel = new Panel { Size = new Size(45, 65), BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(2) };

            if (cardString == "??")
            {
                cardPanel.BackColor = Color.MidnightBlue;
                Label lblBack = new Label { Text = "🂠", ForeColor = Color.LightSkyBlue, Font = new Font("Segoe UI Emoji", 24), AutoSize = false, Size = new Size(45, 65), Location = new Point(0, 0), TextAlign = ContentAlignment.MiddleCenter };
                cardPanel.Controls.Add(lblBack);
                return cardPanel;
            }

            cardPanel.BackColor = Color.White;
            string suit = cardString.Substring(cardString.Length - 1);
            string rank = cardString.Substring(0, cardString.Length - 1);
            bool isRed = (suit == "♥" || suit == "♦");
            Color cardColor = isRed ? Color.Red : Color.Black;

            Label lblTop = new Label { Text = rank + suit, ForeColor = cardColor, Font = new Font("Arial", 8, FontStyle.Bold), Location = new Point(1, 1), AutoSize = true };
            Label lblCenter = new Label { Text = suit, ForeColor = cardColor, Font = new Font("Arial", 20, FontStyle.Regular), AutoSize = false, Size = new Size(45, 30), Location = new Point(0, 25), TextAlign = ContentAlignment.MiddleCenter };

            cardPanel.Controls.Add(lblTop);
            cardPanel.Controls.Add(lblCenter);
            return cardPanel;
        }

        private Panel CreatePlayerAvatar(PlayerGameState pInfo)
        {
            Panel playerPanel = new Panel { Size = new Size(160, 120), BackColor = Color.Transparent };

            Label lblIcon = new Label { Text = "👤", Font = new Font("Segoe UI Emoji", 26F), ForeColor = Color.White, Location = new Point(55, 0), AutoSize = true };

            string scoreDisplay = pInfo.Score > 0 ? $" ({pInfo.Score})" : "";
            Label lblName = new Label { Text = $"{pInfo.Name}{scoreDisplay}", Font = new Font("Arial", 9F, FontStyle.Bold), ForeColor = Color.White, Location = new Point(0, 45), Size = new Size(160, 15), TextAlign = ContentAlignment.MiddleCenter };

            FlowLayoutPanel flowCards = new FlowLayoutPanel { Size = new Size(160, 70), Location = new Point(0, 60), BackColor = Color.Transparent, WrapContents = false, FlowDirection = FlowDirection.LeftToRight };
            if (pInfo.Cards != null) foreach (var card in pInfo.Cards) flowCards.Controls.Add(CreateVisualCard(card));

            string safeResultMsg = pInfo.ResultMessage ?? "";
            Label lblResult = new Label { Text = safeResultMsg, Font = new Font("Arial", 10F, FontStyle.Bold), Location = new Point(0, 105), Size = new Size(160, 15), TextAlign = ContentAlignment.MiddleCenter };
            lblResult.ForeColor = safeResultMsg.Contains("ВИГРАВ") ? Color.Yellow : (safeResultMsg == "ПЕРЕБІР!" ? Color.Red : Color.LightGray);

            playerPanel.Controls.Add(lblIcon); playerPanel.Controls.Add(lblName); playerPanel.Controls.Add(flowCards); playerPanel.Controls.Add(lblResult);
            return playerPanel;
        }

        private Panel CreateDealerAvatar(string score, string[] cards)
        {
            Panel dealerPanel = new Panel { Size = new Size(250, 120), BackColor = Color.Transparent };
            Label lblIcon = new Label { Text = "🤵 ДИЛЕР", Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold), ForeColor = Color.White, Location = new Point(70, 0), AutoSize = true };
            Label lblScore = new Label { Text = $"Рахунок: {score}", Font = new Font("Arial", 10F, FontStyle.Bold), ForeColor = Color.White, Location = new Point(0, 25), Size = new Size(250, 20), TextAlign = ContentAlignment.MiddleCenter };

            FlowLayoutPanel flowCards = new FlowLayoutPanel { Size = new Size(250, 70), Location = new Point(0, 45), BackColor = Color.Transparent, WrapContents = false, FlowDirection = FlowDirection.LeftToRight };
            if (cards != null) foreach (var card in cards) flowCards.Controls.Add(CreateVisualCard(card));

            dealerPanel.Controls.Add(lblIcon); dealerPanel.Controls.Add(lblScore); dealerPanel.Controls.Add(flowCards);
            return dealerPanel;
        }

        // ОНОВЛЕНЕ ОЧИЩЕННЯ СТОЛУ (Щоб чат не зникав)
        public void UpdateGameTable(string status, string dealerScore, string[] dealerCards, PlayerGameState[] players)
        {
            string safeStatus = status ?? "Очікування...";
            lblGameStatus.Text = safeStatus;
            lblGameStatus.ForeColor = safeStatus.Contains("Залишилось") ? Color.Green : Color.Gold;

            // Видаляємо ТІЛЬКИ старі аватари (не чіпаємо чат, статус і результат)
            for (int i = panelTable.Controls.Count - 1; i >= 0; i--)
            {
                Control c = panelTable.Controls[i];
                if (c != lblRoundResult && c != lblGameStatus && c != panelChat)
                {
                    panelTable.Controls.Remove(c);
                    c.Dispose(); // Звільняємо пам'ять
                }
            }

            Panel dealerAvatar = CreateDealerAvatar(dealerScore, dealerCards);
            dealerAvatar.Location = new Point(panelTable.Width / 2 - 125, 40);
            panelTable.Controls.Add(dealerAvatar);

            if (players != null && players.Length > 0)
            {
                int totalPlayers = players.Length;
                int centerX = panelTable.Width / 2;
                int centerY = panelTable.Height / 2 + 20;
                int rx = (panelTable.Width - 300) / 2;
                int ry = (panelTable.Height - 200) / 2;

                string myName = ChatServerConnector.GetInstance().UserName ?? "";
                lblRoundResult.Visible = false;

                for (int i = 0; i < totalPlayers; i++)
                {
                    var pInfo = players[i];
                    Panel pAvatar = CreatePlayerAvatar(pInfo);

                    double angleDeg = 270;
                    if (totalPlayers > 1) angleDeg = 180 + (180.0 / (totalPlayers - 1)) * i;

                    double angleRad = Math.PI * angleDeg / 180.0;
                    int x = centerX + (int)(rx * Math.Cos(angleRad)) - (pAvatar.Width / 2);
                    int y = centerY + (int)(ry * Math.Sin(angleRad)) - (pAvatar.Height / 2);

                    pAvatar.Location = new Point(x, y);
                    panelTable.Controls.Add(pAvatar);

                    if (pInfo.Name == myName && safeStatus == "Раунд завершено!")
                    {
                        string safeResultMsg = pInfo.ResultMessage ?? "";
                        if (safeResultMsg.Contains("ВИГРАВ")) { lblRoundResult.Text = "ВИГРАВ!"; lblRoundResult.ForeColor = Color.Yellow; lblRoundResult.Visible = true; }
                        else if (safeResultMsg.Contains("Програв")) { lblRoundResult.Text = "ПРОГРАВ."; lblRoundResult.ForeColor = Color.Red; lblRoundResult.Visible = true; }
                    }
                }
            }

            // Завжди тримаємо чат поверх карт
            panelChat.BringToFront();
        }

        public void AddChatMessage(string message)
        {
            int start = richTextBoxChat.TextLength;
            richTextBoxChat.AppendText(message + Environment.NewLine);
            int end = richTextBoxChat.TextLength;

            for (int i = start; i < end; i++)
            {
                char c = richTextBoxChat.Text[i];
                if (c == '♥' || c == '♦')
                {
                    richTextBoxChat.Select(i, 1);
                    richTextBoxChat.SelectionColor = Color.Red;
                    richTextBoxChat.SelectionFont = new Font(richTextBoxChat.Font, FontStyle.Bold);
                }
            }
            richTextBoxChat.SelectionStart = richTextBoxChat.Text.Length;
            richTextBoxChat.SelectionColor = Color.White;
            richTextBoxChat.ScrollToCaret();
        }

        private void btnHit_Click(object sender, EventArgs e) => ChatServerConnector.GetInstance().Hit();
        private void btnStand_Click(object sender, EventArgs e) => ChatServerConnector.GetInstance().Stand();

        private void btnSend_Click(object sender, EventArgs e)
        {
            string msg = txtMessage.Text.Trim();
            if (msg.ToLower() == "t" || msg.ToLower() == "hit") ChatServerConnector.GetInstance().Hit();
            else if (msg.ToLower() == "s" || msg.ToLower() == "пас") ChatServerConnector.GetInstance().Stand();
            else ChatServerConnector.GetInstance().SendMessageToServer(msg);
            txtMessage.Clear();
        }

        private void btnLeave_Click(object sender, EventArgs e) { this.Close(); }
        private void GameForm_FormClosing(object sender, FormClosingEventArgs e) { ChatServerConnector.GetInstance().ActiveGameForm = null; _mainMenu.Show(); }
    }
}