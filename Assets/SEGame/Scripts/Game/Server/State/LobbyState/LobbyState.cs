
using System;
using UnityEngine.Serialization;

namespace SEGame
{
    [Serializable]
    public class LobbyState
    {
        public ulong lobbyId;
        public ClientState firstClientState; 
        public ClientState secondClientState;

        public bool isFull;
    }
}