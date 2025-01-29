
using SkyForge.Reactive;
using SkyForge.MVVM;

namespace SEGame
{
    public interface IUIRootViewModel : IViewModel
    {
        ReactiveProperty<bool> IsOpenLoadingScreen { get; }
        ReactiveCollection<View> UIScreenViews { get; }
        
        void ShowLoadingScreen();
        void HideLoadingScreen();
        void AttachScreenUI(UIScreenView screenView);
    }
}


