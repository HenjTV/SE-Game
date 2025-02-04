
using SkyForge.Reactive.Extention;
using SkyForge.Reactive;

namespace SEGame
{
    public class LobbyStateProxy : ILobbyStateProxy
    {
        public ReactiveProperty<IClientStateProxy> FirstClientState { get; private set; }
        public ReactiveProperty<IClientStateProxy> SecondClientState { get; private set; }
        
        public ReactiveProperty<ulong> LobbyId { get; private set; }
        
        public ReactiveProperty<bool> IsFull { get; private set; }
        public LobbyState OriginState { get; private set; }

        public LobbyStateProxy(LobbyState originState)
        {
            if (originState is null)
            {
                originState = new LobbyState();
            }
            OriginState = originState;
            
            FirstClientState = new ReactiveProperty<IClientStateProxy>(new ClientStateProxy(OriginState.firstClientState));
            FirstClientState.Subscribe(newPlayerState => OriginState.firstClientState = newPlayerState.OriginState);
            
            SecondClientState = new ReactiveProperty<IClientStateProxy>(new ClientStateProxy(OriginState.secondClientState));
            SecondClientState.Subscribe(newPlayerState => OriginState.secondClientState = newPlayerState.OriginState);
            
            LobbyId = new ReactiveProperty<ulong>(originState.lobbyId);
            LobbyId.Subscribe(newLobbyId => OriginState.lobbyId = newLobbyId);
            
            IsFull = new ReactiveProperty<bool>(OriginState.isFull);
            IsFull.Subscribe(newIsFull => OriginState.isFull = newIsFull);
        }
    }
}