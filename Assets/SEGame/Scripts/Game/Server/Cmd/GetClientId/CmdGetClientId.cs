using SkyForge.Command;

namespace SEGame
{
    public class CmdGetClientId : ICommand
    {
        public ulong lobbyId;
        public ulong firstClientIdResult;
        public ulong secondClientIdResult;

        public CmdGetClientId(ulong lobbyId)
        {
            this.lobbyId = lobbyId;
            //TODO: make tag fo clientIdResult
        }
    }
}