using System;
using System.Windows.Forms;
using Client.ChatServiceReference;
using System.ServiceModel;

namespace Client
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class ChatServerConnector : Client.ChatServiceReference.IChatServiceCallback
    {
        private static ChatServerConnector Instance = null;
        private ChatServiceClient Client;

        public string UserName { get; private set; }
        public int UserId { get; private set; }
        public Guid CurrentLobbyId { get; private set; } = Guid.Empty;

        // ПРЯМІ ПОСИЛАННЯ НА ВІКНА (Гарантують 100% доставку)
        public GameForm ActiveGameForm { get; set; }
        public MainMenuForm ActiveMenuForm { get; set; } // ТУТ ДОДАЛИ ПОСИЛАННЯ НА МЕНЮ!

        private ChatServerConnector() { InitClient(); }
        private void InitClient() { Client = new ChatServiceClient(new System.ServiceModel.InstanceContext(this)); }
        public static ChatServerConnector GetInstance()
        {
            if (Instance == null) Instance = new ChatServerConnector();
            return Instance;
        }

        private T ExecuteSafe<T>(Func<T> action)
        {
            try { return action(); }
            catch { HandleError(); return default(T); }
        }
        private void ExecuteSafe(Action action)
        {
            try { action(); }
            catch { HandleError(); }
        }

        private void HandleError()
        {
            MessageBox.Show("Зв'язок із сервером втрачено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            UserId = 0; CurrentLobbyId = Guid.Empty; InitClient();

            // ВИКОРИСТОВУЄМО ПРЯМЕ ПОСИЛАННЯ
            if (ActiveMenuForm != null && !ActiveMenuForm.IsDisposed)
            {
                ActiveMenuForm.BeginInvoke((MethodInvoker)delegate { ActiveMenuForm.ResetUIOnDisconnect(); });
            }
        }

        public void Connect(string username) { UserName = username; UserId = ExecuteSafe(() => Client.Connect(username)); }
        public void CreateLobby(string lobbyName) { CurrentLobbyId = ExecuteSafe(() => Client.CreateLobby(lobbyName, UserId)); }
        public void JoinLobby(Guid lobbyId) { if (ExecuteSafe(() => Client.JoinLobby(lobbyId, UserId))) CurrentLobbyId = lobbyId; }
        public void SendMessageToServer(string message) { if (CurrentLobbyId != Guid.Empty) ExecuteSafe(() => Client.SendLobbyMessage(CurrentLobbyId, UserId, message)); }
        public void Hit() { ExecuteSafe(() => Client.Hit(CurrentLobbyId, UserId)); }
        public void Stand() { ExecuteSafe(() => Client.Stand(CurrentLobbyId, UserId)); }
        public void Disconnect() { if (UserId > 0) ExecuteSafe(() => Client.Disconnect(UserId)); }

        public void SendMessageToClient(string senderName, string message)
        {
            if (ActiveGameForm != null && !ActiveGameForm.IsDisposed)
                ActiveGameForm.BeginInvoke((MethodInvoker)delegate { ActiveGameForm.AddChatMessage($"{senderName}: {message}"); });
            else if (ActiveMenuForm != null && !ActiveMenuForm.IsDisposed) // ВИКОРИСТОВУЄМО ПРЯМЕ ПОСИЛАННЯ
                ActiveMenuForm.BeginInvoke((MethodInvoker)delegate { ActiveMenuForm.AddChatMessage($"{senderName}: {message}"); });
        }

        public void UpdateLobbyList(ChatLibrary.LobbyInfo[] lobbies)
        {
            // БІЛЬШЕ НІЯКИХ ПОШУКІВ ПО ІМЕНІ ВІКНА!
            if (ActiveMenuForm != null && !ActiveMenuForm.IsDisposed)
            {
                ActiveMenuForm.BeginInvoke((MethodInvoker)delegate { ActiveMenuForm.RefreshLobbyList(lobbies); });
            }
        }

        public void UpdateGameTable(string lobbyStatus, string dealerScore, string[] dealerCards, ChatLibrary.PlayerGameState[] players)
        {
            if (ActiveGameForm != null && !ActiveGameForm.IsDisposed)
            {
                ActiveGameForm.BeginInvoke((MethodInvoker)delegate {
                    ActiveGameForm.UpdateGameTable(lobbyStatus, dealerScore, dealerCards, players);
                });
            }
        }
    }
}