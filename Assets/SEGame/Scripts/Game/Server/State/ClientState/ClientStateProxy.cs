
using SkyForge.Reactive.Extention;
using SkyForge.Reactive;

namespace SEGame
{
    public class ClientStateProxy : IClientStateProxy
    {
        public ReactiveProperty<ulong> ClientId { get; private set; }
        
        public ClientState OriginState { get; private set; }

        public ClientStateProxy(ClientState originState)
        {
            if (originState is null)
            {
                originState = new ClientState();
            }

            OriginState = originState;
            
            ClientId = new ReactiveProperty<ulong>(originState.clientId);
            ClientId.Subscribe(newValue => originState.clientId = newValue);
        }
    }
}