
using SkyForge.Reactive.Extention;
using System.Collections.Generic;
using SkyForge.Reactive;

namespace SEGame
{
    public class ServerStateProxy : IServerStateProxy
    {
        public ReactiveCollection<ILobbyStateProxy> Lobbies { get; private set; }
        
        public ServerState OriginState { get; private set; }

        public ServerStateProxy(ServerState originState)
        {
            if (originState is null)
            {
                originState = new ServerState();
                originState.lobbies = new List<LobbyState>();
            }
            
            OriginState = originState;
            
            Lobbies = new ReactiveCollection<ILobbyStateProxy>();
            OriginState.lobbies.ForEach(originLobby => Lobbies.Add(new LobbyStateProxy(originLobby)));

            Lobbies.SubscribeAdded(newLobby =>
            {
                OriginState.lobbies.Add(newLobby.OriginState);
            });

            Lobbies.SubscribeRemoved(removedLobby =>
            {
                OriginState.lobbies.Remove(removedLobby.OriginState);
            });
        }

        public ulong GetEntityId()
        {
            return OriginState.globalEntityId++;
        }
    }
}