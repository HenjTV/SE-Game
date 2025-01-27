
using System.Collections;
using SkyForge.Extention;
using SkyForge.Reactive;
using UnityEngine;
using SkyForge;

namespace SEGame
{
    public class MainMenuEntryPoint : MonoBehaviour, IEntryPoint
    {
        private SingleReactiveProperty<MainMenuExitParams> m_menuExitParams;
        
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            Debug.Log("Init MainMenu");
            yield return null;
        }

        public IObservable<SceneExitParams> Run()
        {
            return m_menuExitParams;
        }
    }
}
