
using SkyForge.Reactive;
using SkyForge.Proxy;

namespace SEGame
{
    public interface IClientStateProxy : IProxy<ClientState>
    {
        ReactiveProperty<ulong> ClientId { get; }
    }
}