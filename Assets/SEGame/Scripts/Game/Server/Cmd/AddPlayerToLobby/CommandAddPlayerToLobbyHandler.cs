
using SkyForge.Command;
using System.Linq;

namespace SEGame
{
    public class CommandAddPlayerToLobbyHandler : ICommandHandler<CmdAddPlayerToLobby>
    {
        private IServerStateProxy m_serverStateProxy;

        public CommandAddPlayerToLobbyHandler(IServerStateProxy serverStateProxy)
        {
            m_serverStateProxy = serverStateProxy;
        }
        
        
        public bool Handle(CmdAddPlayerToLobby command)
        {
            var lobby = m_serverStateProxy.Lobbies?.Where(lobby => lobby.LobbyId.Value.Equals(command.lobbyId)).FirstOrDefault();

            if (lobby is null)
                return false;

            var newClientState = new ClientState()
            {
                clientId = command.clientId
            };
            
            var clientStateProxy = new ClientStateProxy(newClientState);
            lobby.SecondClientState.Value = clientStateProxy;
            lobby.IsFull.Value = true;
            
            return true;
        }
    }
}