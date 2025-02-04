
using SkyForge.Command;

namespace SEGame
{
    public class CmdAddPlayerToLobby : ICommand
    {
        public ulong lobbyId;
        public ulong clientId;

        public CmdAddPlayerToLobby(ulong lobbyId, ulong clientId)
        {
            this.lobbyId = lobbyId;
            this.clientId = clientId;
        }
    }
}