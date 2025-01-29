
using SkyForge;

namespace SEGame
{
    public static class GameplayViewModelRegistration
    {
        public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
        {
            container.RegisterSingleton<IUILogViewModel>(factory => new UILogViewModel());
            container.RegisterSingleton<IUIRootGameplayViewModel>(factory => new UIRootGameplayViewModel(factory.Resolve<IUILogViewModel>()));
        }
    }
}


