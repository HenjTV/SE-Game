
using System.Collections.Generic;
using System;

namespace SEGame
{
    [Serializable]
    public class ServerState
    {
        public ulong globalEntityId;
        public List<LobbyState> lobbies;
    }
}