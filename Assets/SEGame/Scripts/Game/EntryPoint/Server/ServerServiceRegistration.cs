using SkyForge;
using SkyForge.Command;

namespace SEGame
{
    public static class ServerServiceRegistration
    {
        public static void Register(DIContainer container, ServerEnterParams serverEnterParams)
        {
            RegisterCommands(container);
            
            container.RegisterSingleton(factory => new LobbyService(factory.Resolve<ICommandProcessor>()));
        }
        
        private static void RegisterCommands(DIContainer container)
        {
            var serverCommandProcessor = new CommandProcessor();

            var serverStateProxy = container.Resolve<IServerStateProvider>().ProxyState;
            
            serverCommandProcessor.RegisterCommandHandler(new CommandCreateLobbyHandler(serverStateProxy));
            serverCommandProcessor.RegisterCommandHandler(new CommandSearchLobbyHandler(serverStateProxy));
            serverCommandProcessor.RegisterCommandHandler(new CommandAddPlayerToLobbyHandler(serverStateProxy));
            serverCommandProcessor.RegisterCommandHandler(new CommandGetClientIdHandler(serverStateProxy));
            
            container.RegisterInstance<ICommandProcessor>(serverCommandProcessor);
        }
    }
}