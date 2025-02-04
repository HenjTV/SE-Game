
using System.Linq;
using SkyForge.Command;

namespace SEGame
{
    public class CommandCreateLobbyHandler : ICommandHandler<CmdCreateLobby>
    {

        private IServerStateProxy m_serverStateProxy;

        public CommandCreateLobbyHandler(IServerStateProxy serverStateProxy)
        {
            m_serverStateProxy = serverStateProxy;
        }
        
        public bool Handle(CmdCreateLobby command)
        {
            
            if (m_serverStateProxy.Lobbies.Any(lobby => lobby.FirstClientState.Value.ClientId.Value.Equals(command.clientId) ||
                                                         lobby.SecondClientState.Value.ClientId.Value.Equals(command.clientId)))
            {
                return false;
            }
            
            var newClientState = new ClientState()
            {
                clientId = command.clientId
            };
            
            var newLobbyState = new LobbyState()
            {
                lobbyId = m_serverStateProxy.GetEntityId(),
                firstClientState = newClientState
            };
            
            var lobbyStateProxy = new LobbyStateProxy(newLobbyState);
            
            m_serverStateProxy.Lobbies.Add(lobbyStateProxy);
            
            return true;
        }
    }
}