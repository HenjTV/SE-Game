
using SkyForge.Reactive;
using SkyForge.Proxy;

namespace SEGame
{
    public interface ILobbyStateProxy : IProxy<LobbyState>
    {
        ReactiveProperty<ulong> LobbyId { get; }
        ReactiveProperty<IClientStateProxy> FirstClientState { get; }
        ReactiveProperty<IClientStateProxy> SecondClientState { get; }
        ReactiveProperty<bool> IsFull { get; }
    }
}