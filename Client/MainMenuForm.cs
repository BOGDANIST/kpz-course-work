using System;
using System.Windows.Forms;
using ChatLibrary;
using Client.ChatServiceReference;

namespace Client
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
            ChatServerConnector.GetInstance().ActiveMenuForm = this;
            this.FormClosing += MainMenuForm_FormClosing;

        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (listBoxLobbies != null)
            {
                listBoxLobbies.DisplayMember = "DisplayString";
                listBoxLobbies.ValueMember = "Id";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введіть ім'я!");
                return;
            }

            ChatServerConnector.GetInstance().Connect(textBox1.Text);
            button1.Enabled = false;
            textBox1.Enabled = false;
            MessageBox.Show("Ви підключилися! Тепер оберіть або створіть лобі.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg = textBox2.Text.Trim();
            string lowerMsg = msg.ToLower();

            // Перехоплення команд через чат
            if (lowerMsg == "t" || lowerMsg == "take" || lowerMsg == "взяти" || lowerMsg == "hit")
                ChatServerConnector.GetInstance().Hit();
            else if (lowerMsg == "s" || lowerMsg == "stand" || lowerMsg == "пас" || lowerMsg == "стоп")
                ChatServerConnector.GetInstance().Stand();
            else
                ChatServerConnector.GetInstance().SendMessageToServer(msg);

            textBox2.Clear();
        }

        private void btnCreateLobby_Click(object sender, EventArgs e)
        {
            string lobbyName = txtLobbyName.Text;
            if (string.IsNullOrWhiteSpace(lobbyName)) return;

            ChatServerConnector.GetInstance().CreateLobby(lobbyName);

            // ПЕРЕХІД НА СТІЛ
            GameForm gameForm = new GameForm(this);
            this.Hide();
            gameForm.Show();
        }

        private void btnJoinLobby_Click(object sender, EventArgs e)
        {
            if (listBoxLobbies.SelectedItem is LobbyInfoWrapper selectedLobby)
            {
                ChatServerConnector.GetInstance().JoinLobby(selectedLobby.Id);

                // ПЕРЕХІД НА СТІЛ
                GameForm gameForm = new GameForm(this);
                this.Hide();
                gameForm.Show();
            }
            else MessageBox.Show("Виберіть лобі!");
        }

        // Обробка закриття вікна
        private void MainMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChatServerConnector.GetInstance().Disconnect();
        }

        public void ResetUIOnDisconnect()
        {
            button1.Enabled = true;
            textBox1.Enabled = true;
            listBoxLobbies.Items.Clear();
            richTextBox1.AppendText("--- Зв'язок розірвано ---" + Environment.NewLine);
            lblGameStatus.Text = "Від'єднано";
            lblGameStatus.ForeColor = System.Drawing.Color.Red;
        }

        // МЕТОД ДЛЯ КОЛЬОРОВИХ МАСТЕЙ У ЧАТІ
        public void AddChatMessage(string message)
        {
            int start = richTextBox1.TextLength;
            richTextBox1.AppendText(message + Environment.NewLine);
            int end = richTextBox1.TextLength;

            for (int i = start; i < end; i++)
            {
                char c = richTextBox1.Text[i];
                if (c == '♥' || c == '♦')
                {
                    richTextBox1.Select(i, 1);
                    richTextBox1.SelectionColor = System.Drawing.Color.Red;
                    richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Bold);
                }
            }

            // Повертаємо курсор в кінець і робимо колір чорним для наступних літер
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.SelectionColor = System.Drawing.Color.Black;
            richTextBox1.SelectionFont = richTextBox1.Font;
            richTextBox1.ScrollToCaret();
        }

        public void RefreshLobbyList(ChatLibrary.LobbyInfo[] lobbies)
        {
            listBoxLobbies.Items.Clear();
            foreach (var lobby in lobbies)
            {
                listBoxLobbies.Items.Add(new LobbyInfoWrapper(lobby));
            }
        }

        public void RefreshGameTable(string status, string colorName, string[] dealerCards, string[] myCards)
        {
            lblGameStatus.Text = status;
            try { lblGameStatus.ForeColor = System.Drawing.Color.FromName(colorName); }
            catch { lblGameStatus.ForeColor = System.Drawing.Color.Gold; }

            listDealerCards.Items.Clear();
            if (dealerCards != null) listDealerCards.Items.AddRange(dealerCards);

            listMyCards.Items.Clear();
            if (myCards != null) listMyCards.Items.AddRange(myCards);
        }

        // НОВІ КНОПКИ ГРИ
        private void btnHit_Click(object sender, EventArgs e)
        {
            ChatServerConnector.GetInstance().Hit();
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
            ChatServerConnector.GetInstance().Stand();
        }
    }

    //public class LobbyInfoWrapper
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }
    //    public int PlayerCount { get; set; }

    //    public string DisplayString => $"{Name} (Гравців: {PlayerCount})";

    //    public LobbyInfoWrapper(LobbyInfo info)
    //    {
    //        Id = info.Id;
    //        Name = info.Name;
    //        PlayerCount = info.PlayerCount;
    //    }
    //}
}