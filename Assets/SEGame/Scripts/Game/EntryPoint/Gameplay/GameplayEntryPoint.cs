
using System.Collections;
using SkyForge.Extention;
using SkyForge.Reactive;
using Unity.Netcode;
using UnityEngine;
using SkyForge;
using UnityEngine.UI;

namespace SEGame
{
    public class GameplayEntryPoint : MonoBehaviour, IEntryPoint
    {
        private SingleReactiveProperty<GameplayExitParams> m_gameplayExitParams = new ();

        [SerializeField] private Button m_testButton;
        
        private NetworkManager m_networkManager;
        private DIContainer m_container;
        
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            var gameplayEnterParams = sceneEnterParams as GameplayEnterParams;
            m_container = parentContainer;
            
            GameplayServiceRegistration.Register(m_container, gameplayEnterParams);
            GameplayViewModelRegistration.Register(m_container, gameplayEnterParams);
            GameplayViewRegistration.BindView(m_container);
            
            m_networkManager = NetworkManager.Singleton;
            
            m_testButton.onClick.AddListener(OnSearch);
            
            var uILogViewModel = m_container.Resolve<IUILogViewModel>();
            
            uILogViewModel.LogMessage($"user {m_networkManager.LocalClientId}", "Init gameplay");
            yield return null;
        }
        
        public IObservable<SceneExitParams> Run()
        {
            if (m_networkManager.StartClient())
            {
                m_networkManager.OnClientConnectedCallback += OnClientConnection;
                m_networkManager.OnClientDisconnectCallback += OnClientDisconnection;
            }
            
            return m_gameplayExitParams;
        }
        
        private void OnClientConnection(ulong clientId)
        {
            if (m_networkManager.LocalClientId.Equals(clientId))
            {
                var uILogViewModel = m_container.Resolve<IUILogViewModel>();
                
                uILogViewModel.LogMessage($"user {m_networkManager.LocalClientId}", "Client connected to server");
            }
        }

        private void OnClientDisconnection(ulong clientId)
        {
            if (m_networkManager.LocalClientId.Equals(clientId))
            {
                var uILogViewModel = m_container.Resolve<IUILogViewModel>();
                uILogViewModel.LogMessage($"user {m_networkManager.LocalClientId}", "Client disconnected from server");
            }
        }

        private void OnSearch()
        {
            var lobbyNetworkController = m_container.Resolve<LobbyNetworkController>();
            lobbyNetworkController.SearchLobbyClient();
        }
        
#if !DEDICATED_SERVER

        private void OnDestroy()
        {
            m_container.Dispose();
        }
        
        public DIContainer GetContainer()
        {
            return m_container;
        }
        
#endif
    }
}


