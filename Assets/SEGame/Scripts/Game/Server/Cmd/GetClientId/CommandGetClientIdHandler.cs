
using SkyForge.Command;
using System.Linq;

namespace SEGame
{
    public class CommandGetClientIdHandler : ICommandHandler<CmdGetClientId>
    {

        private IServerStateProxy m_serverStateProxy;
        
        public CommandGetClientIdHandler(IServerStateProxy serverStateProxy)
        {
            m_serverStateProxy = serverStateProxy;
        }
        
        public bool Handle(CmdGetClientId command)
        {
            var lobby = m_serverStateProxy.Lobbies?.Where(lobby => lobby.LobbyId.Value.Equals(command.lobbyId))
                                                  .FirstOrDefault();
            if (lobby is null)
                return false;
            
            command.firstClientIdResult = lobby.FirstClientState.Value.ClientId.Value;
            command.secondClientIdResult = lobby.SecondClientState.Value.ClientId.Value;
            return true;
        }
    }
}