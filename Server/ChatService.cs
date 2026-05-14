using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using ChatLibrary; // Обов'язково підключаємо нашу бібліотеку!

namespace Server
{
    // --- РЕАЛІЗАЦІЯ СЕРВЕРА ---
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChatService
    {
        private List<Player> _allPlayers = new List<Player>();
        private List<Lobby> _lobbies = new List<Lobby>();
        private int _nextUserId = 1;

        public int Connect(string username)
        {
            var p = new Player { Id = _nextUserId++, Name = username, Callback = OperationContext.Current.GetCallbackChannel<IChatServiceCallback>() };
            _allPlayers.Add(p);
            Console.WriteLine($"Користувач {username} підключився");
            NotifyAllLobbyUpdate();
            return p.Id;
        }

        public Guid CreateLobby(string lobbyName, int creatorId)
        {
            var lobby = new Lobby(lobbyName);
            _lobbies.Add(lobby);
            JoinLobby(lobby.Id, creatorId);
            return lobby.Id;
        }

        public bool JoinLobby(Guid lobbyId, int playerId)
        {
            var lobby = _lobbies.FirstOrDefault(l => l.Id == lobbyId);
            var player = _allPlayers.FirstOrDefault(p => p.Id == playerId);
            if (lobby != null && player != null)
            {
                if (!lobby.Players.Contains(player))
                {
                    RemovePlayerFromAllLobbies(player);
                    player.IsStanding = lobby.IsGameInProgress;
                    player.LastResult = lobby.IsGameInProgress ? "ЧЕКАЄ..." : "";
                    lobby.Players.Add(player);
                    SendLobbyMessage(lobbyId, 0, $"Гравець {player.Name} приєднався.");
                    NotifyAllLobbyUpdate();
                    BroadcastGameState(lobby);

                    if (!lobby.IsGameInProgress && lobby.Players.Count > 0)
                    {
                        Task.Run(() => StartGameLoop(lobby));
                    }
                }
                return true;
            }
            return false;
        }

        private async void StartGameLoop(Lobby lobby)
        {
            if (lobby.IsGameInProgress) return;
            lobby.IsGameInProgress = true;

            foreach (var p in lobby.Players) { p.LastResult = ""; p.Hand.Clear(); p.IsStanding = false; }
            lobby.DealerHand.Clear();

            for (int i = 10; i > 0; i--)
            {
                if (!_lobbies.Contains(lobby) || lobby.Players.Count == 0) { lobby.IsGameInProgress = false; return; }
                lobby.GameStatus = $"Тасування... Старт через {i}с";
                BroadcastGameState(lobby);
                await Task.Delay(1000);
            }

            if (!_lobbies.Contains(lobby) || lobby.Players.Count == 0) { lobby.IsGameInProgress = false; return; }

            lobby.Deck = new Deck();
            lobby.Deck.Shuffle();
            lobby.DealerHand.Add(lobby.Deck.Draw());

            foreach (var p in lobby.Players)
            {
                p.Hand.Clear();
                p.Hand.Add(lobby.Deck.Draw());
                p.Hand.Add(lobby.Deck.Draw());
                p.LastResult = "ХІД ГРАВЦЯ";
                if (p.Score == 21) { p.IsStanding = true; p.LastResult = "БЛЕКДЖЕК!"; }
            }

            for (int i = 20; i >= 0; i--)
            {
                if (!_lobbies.Contains(lobby) || lobby.Players.Count == 0) { lobby.IsGameInProgress = false; return; }
                if (lobby.Players.All(p => p.IsStanding)) break;
                lobby.GameStatus = $"Ваш хід! Залишилось: {i}с";
                BroadcastGameState(lobby);
                await Task.Delay(1000);
            }

            if (!_lobbies.Contains(lobby) || lobby.Players.Count == 0) { lobby.IsGameInProgress = false; return; }

            foreach (var p in lobby.Players.Where(p => !p.IsStanding))
            {
                p.IsStanding = true;
                if (p.LastResult == "ХІД ГРАВЦЯ") p.LastResult = "ЧАС ВИЙШОВ";
            }

            await DealerTurn(lobby);
        }

        public void Hit(Guid lobbyId, int playerId)
        {
            var lobby = _lobbies.FirstOrDefault(l => l.Id == lobbyId);
            var player = lobby?.Players.FirstOrDefault(x => x.Id == playerId);
            if (lobby == null || player == null || !lobby.IsGameInProgress || player.IsStanding) return;

            player.Hand.Add(lobby.Deck.Draw());
            if (player.Score > 21) { player.IsStanding = true; player.LastResult = "ПЕРЕБІР!"; }
            BroadcastGameState(lobby);
        }

        public void Stand(Guid lobbyId, int playerId)
        {
            var lobby = _lobbies.FirstOrDefault(l => l.Id == lobbyId);
            var player = lobby?.Players.FirstOrDefault(x => x.Id == playerId);
            if (lobby == null || player == null || !lobby.IsGameInProgress || player.IsStanding) return;

            player.IsStanding = true;
            player.LastResult = "ПАС";
            BroadcastGameState(lobby);
        }

        private async Task DealerTurn(Lobby lobby)
        {
            lobby.GameStatus = "Хід дилера...";
            BroadcastGameState(lobby);
            await Task.Delay(1000);

            // Використовуємо правильний підрахунок для дилера
            while (GetScore(lobby.DealerHand) < 17)
            {
                lobby.DealerHand.Add(lobby.Deck.Draw());
                BroadcastGameState(lobby);
                await Task.Delay(1500);
            }

            int dealerScore = GetScore(lobby.DealerHand);
            bool dealerBust = dealerScore > 21;

            foreach (var p in lobby.Players)
            {
                int pScore = p.Score;
                if (p.Hand.Count == 0 || p.LastResult == "БЛЕКДЖЕК!" || p.LastResult == "ПЕРЕБІР!") continue;

                if (dealerBust) p.LastResult = $"ВИГРАВ! ({pScore})";
                else if (pScore > dealerScore) p.LastResult = $"ВИГРАВ! ({pScore})";
                else if (pScore < dealerScore) p.LastResult = $"Програв ({pScore})";
                else p.LastResult = $"Нічия ({pScore})";
            }

            lobby.GameStatus = "Раунд завершено!";
            lobby.IsGameInProgress = false;
            BroadcastGameState(lobby);

            await Task.Delay(6000);
            if (_lobbies.Contains(lobby) && lobby.Players.Count > 0) { Task.Run(() => StartGameLoop(lobby)); }
        }


        private int GetScore(List<Card> hand)
        {
            int score = hand.Sum(c => c.Value);
            int aces = hand.Count(c => c.Rank == "A");
            while (score > 21 && aces > 0) { score -= 10; aces--; }
            return score;
        }
        private void BroadcastGameState(Lobby lobby)
        {
            string[] dealerCards = lobby.DealerHand.Select(c => c.ToString()).ToArray();
            int dealerScore = GetScore(lobby.DealerHand); // Правильний рахунок

            // Проходимось по кожному гравцю окремо
            foreach (var targetPlayer in lobby.Players.ToList())
            {
                PlayerGameState[] playerStates = lobby.Players.Select(p => new PlayerGameState
                {
                    Name = p.Name,
                    // Якщо це карти ЦЬОГО гравця - показуємо, якщо чужі - відправляємо "??"
                    Cards = (p.Id == targetPlayer.Id)
                            ? p.Hand.Select(c => c.ToString()).ToArray()
                            : p.Hand.Select(c => "??").ToArray(),

                    // Ховаємо рахунок чужих гравців
                    Score = (p.Id == targetPlayer.Id) ? p.Score : 0,
                    IsStanding = p.IsStanding,
                    ResultMessage = p.LastResult
                }).ToArray();

                try { targetPlayer.Callback.UpdateGameTable(lobby.GameStatus, dealerScore.ToString(), dealerCards, playerStates); }
                catch { Disconnect(targetPlayer.Id); }
            }
        }

        private void RemovePlayerFromAllLobbies(Player player)
        {
            foreach (var lobby in _lobbies.ToList())
            {
                if (lobby.Players.Contains(player))
                {
                    lobby.Players.Remove(player);
                    SendLobbyMessage(lobby.Id, 0, $"Гравець {player.Name} вийшов.");
                    if (lobby.Players.Count == 0) _lobbies.Remove(lobby);
                }
            }
        }

        public List<LobbyInfo> GetActiveLobbies() => _lobbies.Select(l => new LobbyInfo { Id = l.Id, Name = l.Name, PlayerCount = l.Players.Count }).ToList();
        public void SendLobbyMessage(Guid lobbyId, int senderId, string message)
        {
            var lobby = _lobbies.FirstOrDefault(l => l.Id == lobbyId);
            if (lobby == null) return;

            // Визначаємо, хто відправив повідомлення (якщо ID 0 - це пише сам сервер)
            string senderName = senderId == 0 ? "СЕРВЕР" :
                               _allPlayers.FirstOrDefault(p => p.Id == senderId)?.Name ?? "Анонім";

            // Розсилаємо повідомлення всім гравцям у цьому лобі
            foreach (var p in lobby.Players.ToList())
            {
                try
                {
                    p.Callback.SendMessageToClient(senderName, message);
                }
                catch
                {
                    // Якщо гравець відключився з помилкою, видаляємо його
                    Disconnect(p.Id);
                }
            }
        }
        private void NotifyAllLobbyUpdate()
        {
            var info = GetActiveLobbies();
            // .ToList() робить копію списку гравців для безпечної розсилки
            foreach (var p in _allPlayers.ToList())
            {
                try { p.Callback.UpdateLobbyList(info); }
                catch { }
            }
        }
        public void Disconnect(int id)
        {
            var player = _allPlayers.FirstOrDefault(p => p.Id == id);
            if (player != null) { _allPlayers.Remove(player); RemovePlayerFromAllLobbies(player); NotifyAllLobbyUpdate(); }
        }
    }
}