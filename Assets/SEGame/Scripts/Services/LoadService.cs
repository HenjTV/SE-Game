
using UnityEngine;

namespace SEGame
{
    public class LoadService : System.IDisposable
    {
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


