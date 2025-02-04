using SkyForge.MVVM;
using SkyForge.Reactive;
using UnityEngine;

namespace SEGame
{
    public interface IUILogViewModel : IViewModel
    {
        ReactiveProperty<string> MessageLog { get; }
        void LogMessage(string name, string message);

        void LogMessage(string message);
    } 
}


