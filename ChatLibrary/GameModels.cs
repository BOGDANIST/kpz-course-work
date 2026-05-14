using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatLibrary
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IChatServiceCallback Callback { get; set; }
        public List<Card> Hand { get; set; } = new List<Card>();
        public bool IsStanding { get; set; } = false;
        public string LastResult { get; set; } = "";

        public int Score
        {
            get
            {
                int score = Hand.Sum(c => c.Value);
                int aces = Hand.Count(c => c.Rank == "A");
                while (score > 21 && aces > 0) { score -= 10; aces--; }
                return score;
            }
        }
    }

    public class Lobby
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Card> DealerHand { get; set; } = new List<Card>();
        public Deck Deck { get; set; }
        public bool IsGameInProgress { get; set; } = false;
        public string GameStatus { get; set; } = "Очікування...";

        public Lobby(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }

    public class Deck
    {
        private List<Card> cards = new List<Card>();
        private Random rnd = new Random();
        public Deck()
        {
            string[] suits = { "♠", "♥", "♦", "♣" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            int[] values = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
            for (int i = 0; i < ranks.Length; i++)
                foreach (var suit in suits) cards.Add(new Card { Suit = suit, Rank = ranks[i], Value = values[i] });
        }
        public void Shuffle() => cards = cards.OrderBy(x => rnd.Next()).ToList();
        public Card Draw() { var c = cards[0]; cards.RemoveAt(0); return c; }
    }
}