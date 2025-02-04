
using SkyForge.Reactive;
using SkyForge.Proxy;

namespace SEGame
{
    public interface IServerStateProxy : IProxy<ServerState>
    {
        ReactiveCollection<ILobbyStateProxy> Lobbies { get; }

        ulong GetEntityId();
    }
}