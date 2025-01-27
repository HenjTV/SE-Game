
using UnityEngine;

namespace SEGame
{
    public class LoadService : System.IDisposable
    {
        public const string PREFAB_UI_ROOT = "Prefabs/UI/UIRoot";
        public const string PREFAB_UI_MENU = "Prefabs/UI/UIMenu";
        public void Dispose()
        {
            
        }

        public T LoadPrefab<T>(string pathPrefab) where T : Object
        {
            var prefab = Resources.Load<T>(pathPrefab);
            if (prefab is null)
            {
                Debug.LogError($"Failed to load prefab: {pathPrefab}");
                throw new System.MethodAccessException("Failed to load prefab");
            }
            
            return prefab;
        }
    }
}


