
using SkyForge.Command;

namespace SEGame
{
    public class CmdSearchLobby : ICommand
    {
        public ulong clientId;
        
        public ulong lobbyIdResult;
        
        public CmdSearchLobby(ulong clientId)
        {
            this.clientId = clientId;
        }
    }
}