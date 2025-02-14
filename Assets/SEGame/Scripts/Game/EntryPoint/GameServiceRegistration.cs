
using SkyForge;

namespace SEGame
{
    public static class GameServiceRegistration
    {
        public static void RegisterService(DIContainer container)
        {
            container.RegisterSingleton(factory => new LoadService());
            container.RegisterSingleton(factory => new SceneService());
        }
    }
}


