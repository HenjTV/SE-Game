
using SkyForge.Reactive;
using SkyForge.MVVM;

namespace SEGame
{
    public interface IUIRootGameplayViewModel : IViewModel
    {
        ReactiveProperty<bool> IsOpenSearchMenu { get; }
        
        IUILogViewModel UILogViewModel { get; }
        void OpenPauseMenu(object sender);

        void ShowSearchScreen(object sender);
        void HideSearchScreen(object sender);
    }
}


