
using SkyForge.Reactive;
using SkyForge.MVVM;

namespace SEGame
{
    public class UIRootGameplayViewModel : IUIRootGameplayViewModel
    {
        public ReactiveProperty<bool> IsOpenSearchMenu { get; private set; } = new();
        
        [SubViewModel(typeof(UILogViewModel))]
        public IUILogViewModel UILogViewModel { get; private set; }

        public UIRootGameplayViewModel(IUILogViewModel uiLogViewModel)
        {
            UILogViewModel = uiLogViewModel;
        }

        public void Update(float deltaTime)
        {
            
        }

        public void PhysicsUpdate(float deltaTime)
        {
            
        }
        
        [ReactiveMethod]
        public void OpenPauseMenu(object sender)
        {
            
        }
        public void ShowSearchScreen(object sender)
        {
            IsOpenSearchMenu.Value = true;
        }
        
        public void HideSearchScreen(object sender)
        {
            IsOpenSearchMenu.Value = false;
        }
        
        
        public void Dispose()
        {
            
        }
    }
}


