
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
        private NetworkManager m_networkManager;
        
        private SingleReactiveProperty<ServerExitParams> m_serverExitParams = new();
        private DIContainer m_container;
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            var serverEnterParams = sceneEnterParams as ServerEnterParams;
            m_container = parentContainer;
            
            m_networkManager = NetworkManager.Singleton;
            yield return null;
        }
        
        public IObservable<SceneExitParams> Run()
        {
            Debug.Log("Run Server");
            if(m_networkManager.StartServer())
                Debug.Log("Server started");
            
            return m_serverExitParams;
        }
    }
}


