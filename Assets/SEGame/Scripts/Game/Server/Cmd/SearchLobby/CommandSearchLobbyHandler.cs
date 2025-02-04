using System.Linq;
using SkyForge.Command;

namespace SEGame
{
    public class CommandSearchLobbyHandler : ICommandHandler<CmdSearchLobby>
    {

        private IServerStateProxy m_serverStateProxy;

        public CommandSearchLobbyHandler(IServerStateProxy serverStateProxy)
        {
            m_serverStateProxy = serverStateProxy;
        }
        
        public bool Handle(CmdSearchLobby command)
        {
            var lobbyIsNotFull = m_serverStateProxy.Lobbies?.Where(lobby => !lobby.IsFull.Value && 
                                                                                 !lobby.FirstClientState.Value.ClientId.Value.Equals(command.clientId) &&
                                                                                 !lobby.SecondClientState.Value.ClientId.Value.Equals(command.clientId))
                                                                         .FirstOrDefault();
            
            if (lobbyIsNotFull is not null)
            {
                command.lobbyIdResult = lobbyIsNotFull.LobbyId.Value;
                return true;
            }
            
            return false;
        }
    }
}