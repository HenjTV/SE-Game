
using System;
using SkyForge.MVVM;

#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace SEGame
{
    public class UIMenuViewModel : IUIMenuViewModel
    {
        private Action<object> m_startGameCallBack;

        public UIMenuViewModel(Action<object> startGameCallBack)
        {
            m_startGameCallBack = startGameCallBack;
        }
        
        public void Update(float deltaTime)
        {
            
        }

        public void PhysicsUpdate(float deltaTime)
        {   
            
        }
        
        [ReactiveMethod]
        public void Connection(object sender)
        {
            m_startGameCallBack?.Invoke(sender);
        }

        [ReactiveMethod]
        public void ExitGame(object sender)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        
        public void Dispose()
        {
            
        }

    }

}

