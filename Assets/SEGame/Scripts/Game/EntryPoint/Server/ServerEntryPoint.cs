
using System.Collections.Generic;
using System.Collections;
using SkyForge.Extention;
using SkyForge.Reactive;
using Unity.Netcode;
using UnityEngine;
using SkyForge;

namespace SEGame
{
    public class ServerEntryPoint : MonoBehaviour, IEntryPoint
    {
        [SerializeField] private UILogView m_logPrefab;
        [SerializeField] private Canvas m_forTestDebugLog;
        
        [SerializeField] private LobbyNetworkController m_networkLobbyControllerPrefab;
        
        private SingleReactiveProperty<ServerExitParams> m_serverExitParams = new();
        
        private NetworkManager m_networkManager;
        private DIContainer m_container;

        private List<NetworkObject> m_spawnedLobbyNetworkController;
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            var serverEnterParams = sceneEnterParams as ServerEnterParams;
            m_container = parentContainer;

            m_spawnedLobbyNetworkController = new List<NetworkObject>();
            
            ServerServiceRegistration.Register(m_container, serverEnterParams);
            RegisterViewModel(m_container);
            BindView();
            
            m_networkManager = NetworkManager.Singleton;
            yield return null;
        }
        
        public SkyForge.Reactive.IObservable<SceneExitParams> Run()
        {
            if (m_networkManager.StartServer())
            {
                m_networkManager.OnClientConnectedCallback += OnClientConnection;
                
                var uILogViewModel = m_container.Resolve<IUILogViewModel>();
                
                uILogViewModel.LogMessage("server", "Server started");
                
            }
            return m_serverExitParams;
        }
        
        private void OnClientConnection(ulong clientId)
        {
            
#if DEDICATED_SERVER
            foreach (var currentNetworkObject in m_spawnedLobbyNetworkController)
            {
                currentNetworkObject.NetworkHide(clientId);
            }

            var networkObject = Instantiate(m_networkLobbyControllerPrefab).GetComponent<NetworkObject>();
            networkObject.gameObject.name = $"LobbyNetworkController:user-{clientId}";
            
            var lobbyNetworkController = networkObject.GetComponent<LobbyNetworkController>();
            
            var lobbyService = m_container.Resolve<LobbyService>();
            var uILogViewModel = m_container.Resolve<IUILogViewModel>();
            lobbyNetworkController.Init(lobbyService, uILogViewModel, clientId);

            networkObject.SpawnAsPlayerObject(clientId);
            
            m_spawnedLobbyNetworkController.Add(networkObject);
#endif
        }
        
        //for test
        private void RegisterViewModel(DIContainer container)
        {
            container.RegisterSingleton<IUILogViewModel>(factory => new UILogViewModel());
        }

        //for test
        private void BindView()
        {
            var loadService = m_container.Resolve<LoadService>();
            
            //Init uILog
            var uILogView = Instantiate(m_logPrefab);
            uILogView.transform.SetParent(m_forTestDebugLog.transform);
            
            var uILogViewModel = m_container.Resolve<IUILogViewModel>();
            uILogView.Bind(uILogViewModel);
        }

        private void OnDestroy()
        {
            m_container.Dispose();
        }
    }
}


