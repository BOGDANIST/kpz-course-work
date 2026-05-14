using System;
using Client.ChatServiceReference;

namespace Client
{
    public class LobbyInfoWrapper
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PlayerCount { get; set; }
        public string DisplayString => $"{Name} (Гравців: {PlayerCount})";

        public LobbyInfoWrapper(ChatLibrary.LobbyInfo info)
        {
            Id = info.Id;
            Name = info.Name;
            PlayerCount = info.PlayerCount;
        }
    }
}