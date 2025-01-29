
using System.Collections;
using SkyForge.Extention;
using SkyForge.Reactive;
using Unity.Netcode;
using UnityEngine;
using SkyForge;

namespace SEGame
{
    public class GameplayEntryPoint : MonoBehaviour, IEntryPoint
    {
        private SingleReactiveProperty<GameplayExitParams> m_gameplayExitParams = new ();
        
        private NetworkManager m_networkManager;
        private DIContainer m_container;
        
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            var gameplayEnterParams = sceneEnterParams as GameplayEnterParams;
            m_container = parentContainer;
            
            GameplayServiceRegistration.Register(m_container, gameplayEnterParams);
            GameplayViewModelRegistration.Register(m_container, gameplayEnterParams);
            GameplayViewRegistration.BindView(m_container);
            
           // m_networkManager = NetworkManager.Singleton;
            
            var uILogViewModel = m_container.Resolve<IUILogViewModel>();
            
            uILogViewModel.LogMessage("Init gameplay");
            yield return null;
        }
        
        public IObservable<SceneExitParams> Run()
        {
            /*if (m_networkManager.StartClient())
            {
                OnConnectedClient();
            }*/
            var uILogViewModel = m_container.Resolve<IUILogViewModel>();
            uILogViewModel.LogMessage("Run gameplay");
            return m_gameplayExitParams;
        }
        
        /*private void OnConnectedClient()
        {
            var uIRootGameplayViewModel = m_container.Resolve<IUIRootGameplayViewModel>();
            uIRootGameplayViewModel.ShowSearchScreen(null);
            
            
        }*/

        private void OnDestroy()
        {
            m_container.Dispose();
        }
        
    }
}


