
using SkyForge.Command;
using System;

namespace SEGame
{
    public class LobbyService : IDisposable
    {
        private ICommandProcessor m_serverCommandProcessor;

        public LobbyService(ICommandProcessor commandProcessor)
        {
            m_serverCommandProcessor = commandProcessor;
        }

        public bool SearchLobby(ulong clientId, out ulong lobbyId)
        {
            var searchCommand = new CmdSearchLobby(clientId);
            var result = m_serverCommandProcessor.Process(searchCommand);
            
            if (result)
                lobbyId = searchCommand.lobbyIdResult;
            else
                lobbyId = 0;
            
            return result;
        }

        public bool CreateLobby(ulong clientId)
        {
            var createLobbyCommand = new CmdCreateLobby(clientId);
            return m_serverCommandProcessor.Process(createLobbyCommand);
        }

        public bool AddPlayerToLobby(ulong lobbyId, ulong clientId)
        {
            var addPlayerCommand = new CmdAddPlayerToLobby(lobbyId, clientId);
            return m_serverCommandProcessor.Process(addPlayerCommand);
        }

        public bool GetFirstClientId(ulong lobbyId, out ulong clientId)
        {
            var getClientIdCommand = new CmdGetClientId(lobbyId);
            if (m_serverCommandProcessor.Process(getClientIdCommand))
            {
                clientId = getClientIdCommand.firstClientIdResult;
                return true;
            }
            
            clientId = 0;
            return false;
        }

        public bool GetSecondClientId(ulong lobbyId, out ulong clientId)
        {
            var getClientIdCommand = new CmdGetClientId(lobbyId);
            if (m_serverCommandProcessor.Process(getClientIdCommand))
            {
                clientId = getClientIdCommand.secondClientIdResult;
                return true;
            }
            
            clientId = 0;
            return false;
        }
        
        public void Dispose()
        {
            
        }
    }
}