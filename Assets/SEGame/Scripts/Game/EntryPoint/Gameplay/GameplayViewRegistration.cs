
using UnityEngine;
using SkyForge;

namespace SEGame
{
    public static class GameplayViewRegistration 
    {
        public static void BindView(DIContainer container)
        {
            var loadService = container.Resolve<LoadService>();
            
            //Init UIRootGameplay
            var prefabUIRootGameplay = loadService.LoadPrefab<UIRootGameplayView>(LoadService.PREFAB_UI_ROOT_GAMEPLAY);
            var uIRootGameplayView = Object.Instantiate(prefabUIRootGameplay);
            
            var uIRootGameplayViewModel = container.Resolve<IUIRootGameplayViewModel>();
            uIRootGameplayView.Bind(uIRootGameplayViewModel);
            
            var uIRootViewModel = container.Resolve<IUIRootViewModel>();
            uIRootViewModel.AttachScreenUI(uIRootGameplayView);
        }
    }
}


