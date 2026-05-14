using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ChatLibrary
{
    [DataContract]
    public class Card
    {
        [DataMember] public string Suit { get; set; }
        [DataMember] public string Rank { get; set; }
        [DataMember] public int Value { get; set; }
        public override string ToString() => $"{Rank}{Suit}";
    }

    [DataContract]
    public class LobbyInfo
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public int PlayerCount { get; set; }
    }

    [DataContract]
    public class PlayerGameState
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string[] Cards { get; set; }
        [DataMember] public int Score { get; set; }
        [DataMember] public bool IsStanding { get; set; }
        [DataMember] public string ResultMessage { get; set; }
    }

    [ServiceContract(CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract] int Connect(string username);
        [OperationContract(IsOneWay = true)] void Disconnect(int id);
        [OperationContract] List<LobbyInfo> GetActiveLobbies();
        [OperationContract] Guid CreateLobby(string lobbyName, int creatorId);
        [OperationContract] bool JoinLobby(Guid lobbyId, int playerId);
        [OperationContract(IsOneWay = true)] void SendLobbyMessage(Guid lobbyId, int senderId, string message);
        [OperationContract(IsOneWay = true)] void Hit(Guid lobbyId, int playerId);
        [OperationContract(IsOneWay = true)] void Stand(Guid lobbyId, int playerId);
    }

    [ServiceContract]
    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)] void SendMessageToClient(string senderName, string message);
        [OperationContract(IsOneWay = true)] void UpdateLobbyList(List<LobbyInfo> lobbies);
        [OperationContract(IsOneWay = true)] void UpdateGameTable(string lobbyStatus, string dealerScore, string[] dealerCards, PlayerGameState[] players);
    }
}