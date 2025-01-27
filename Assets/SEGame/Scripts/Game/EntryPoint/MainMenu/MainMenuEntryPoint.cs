
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
            
            yield return new WaitForSeconds(2f);
        }

        public IObservable<SceneExitParams> Run()
        {
            return m_menuExitParams;
        }
    }
}
