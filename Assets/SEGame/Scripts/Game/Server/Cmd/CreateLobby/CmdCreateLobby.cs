
using SkyForge.Command;

namespace SEGame
{
    public class CmdCreateLobby : ICommand
    {
        public ulong clientId;

        public CmdCreateLobby(ulong clientId)
        {
            this.clientId = clientId;
        }
    }
}