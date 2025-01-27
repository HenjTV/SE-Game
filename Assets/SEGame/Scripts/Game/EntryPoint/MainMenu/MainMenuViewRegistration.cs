
using SkyForge;
using UnityEngine;

namespace SEGame
{
    public static class MainMenuViewRegistration
    {
        public static void BindView(DIContainer container)
        {
            var loadService = container.Resolve<LoadService>();
            
            //Init UIMenu
            var prefab = loadService.LoadPrefab<UIMenuView>(LoadService.PREFAB_UI_MENU);
            var uIMenuView = Object.Instantiate(prefab);
            var uIRootViewModel = container.Resolve<IUIRootViewModel>();
            uIRootViewModel.AttachScreenUI(uIMenuView);
            
            var uIMenuViewModel = container.Resolve<IUIMenuViewModel>();
            uIMenuView.Bind(uIMenuViewModel);
        }
    }
}


