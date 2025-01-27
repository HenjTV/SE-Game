
using System.Collections;
using SkyForge.Extention;
using SkyForge.Reactive;
using UnityEngine;
using SkyForge;

namespace SEGame
{
    public class GameplayEntryPoint : MonoBehaviour, IEntryPoint
    {
        private SingleReactiveProperty<GameplayExitParams> m_gameplayExitParams = new ();
        private DIContainer m_container;
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            var gameplayEnterParams = sceneEnterParams as GameplayEnterParams;
            m_container = parentContainer;
            
            GameplayServiceRegistration.Register(m_container, gameplayEnterParams);
            GameplayViewModelRegistration.Register(m_container, gameplayEnterParams);
            GameplayViewRegistration.BindView(m_container);
            
            yield return null;
        }

        public IObservable<SceneExitParams> Run()
        {
             
            return m_gameplayExitParams;
        }
    }
}


