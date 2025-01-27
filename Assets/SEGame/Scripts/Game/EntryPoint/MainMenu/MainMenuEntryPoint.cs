
using System.Collections;
using SkyForge.Extention;
using SkyForge.Reactive;
using UnityEngine;
using SkyForge;

namespace SEGame
{
    public class MainMenuEntryPoint : MonoBehaviour, IEntryPoint
    {
        private SingleReactiveProperty<MainMenuExitParams> m_menuExitParams = new ();
        private DIContainer m_container;
        
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            var mainMenuEnterParams = sceneEnterParams as MainMenuEnterParams;
            
            m_container = parentContainer;
            
            MainMenuServiceRegistration.Register(m_container, mainMenuEnterParams);
            MainMenuViewModelRegistration.Register(m_container, mainMenuEnterParams);
            m_container.RegisterSingleton<IUIMenuViewModel>(factory => new UIMenuViewModel(LoadGamePlayParams));
            MainMenuViewRegistration.BindView(m_container);
            
            yield return null;
        }

        public IObservable<SceneExitParams> Run()
        {
            return m_menuExitParams;
        }

        private void LoadGamePlayParams(object sender)
        {
            var gameplayEnterParams = new GameplayEnterParams();
            m_menuExitParams.Value = new MainMenuExitParams(gameplayEnterParams);
        }

        private void OnDestroy()
        {
            m_container.Dispose();
        }
    }
}
