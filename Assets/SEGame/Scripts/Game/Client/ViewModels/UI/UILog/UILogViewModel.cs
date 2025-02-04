using SkyForge.Reactive;
using UnityEngine;

namespace SEGame
{
    public class UILogViewModel : IUILogViewModel
    {
        public ReactiveProperty<string> MessageLog { get; private set; } = new();
        
        public void Dispose()
        {
            
        }

        public void Update(float deltaTime)
        {
            
        }

        public void PhysicsUpdate(float deltaTime)
        {
            
        }
        
        public void LogMessage(string name, string message)
        {
            MessageLog.Value = name + ":> " + message;
        }

        public void LogMessage(string message)
        {
            MessageLog.Value = message;
        }
    }
}


