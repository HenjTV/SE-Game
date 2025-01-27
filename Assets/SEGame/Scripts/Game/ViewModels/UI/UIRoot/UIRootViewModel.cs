
using SkyForge.MVVM;
using SkyForge.Reactive;

namespace SEGame
{
    public class UIRootViewModel : IUIRootViewModel
    {
        public ReactiveProperty<bool> IsOpenLoadingScreen { get; private set; } = new ();
        public ReactiveCollection<View> UIScreenViews { get; private set; } = new ();
        

        public void Update(float deltaTime)
        {
            
        }

        public void PhysicsUpdate(float deltaTime)
        {
            
        }
        
        public void ShowLoadingScreen()
        {
            IsOpenLoadingScreen.Value = true;
        }

        public void HideLoadingScreen()
        {
            IsOpenLoadingScreen.Value = false;
        }

        public void AttachScreenUI(UIScreenView screenView)
        {
            UIScreenViews.Clear();
            UIScreenViews.Add(screenView);
        }
        
        public void Dispose()
        {
            
        }
    }
}


