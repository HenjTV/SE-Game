
using SkyForge.Reactive.Extention;
using SkyForge.Reactive;
       
namespace SEGame
{
    public class ServerStateProvider : IServerStateProvider
    {
        public ServerStateProxy ProxyState { get; private set; }

        public IObservable<bool> SaveState()
        {
            return Observable.Return(true);
        }

        public IObservable<bool> ResetState()
        {
            ProxyState = new ServerStateProxy(null);
            
            return Observable.Return(true);
        }

        public IObservable<ServerStateProxy> LoadState()
        {
            ProxyState = new ServerStateProxy(null);
            
            return Observable.Return(ProxyState);
        }
        
        
        public void Dispose()
        {
            
        }
    }
}