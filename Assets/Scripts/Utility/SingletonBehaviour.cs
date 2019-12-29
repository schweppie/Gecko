using UnityEngine;

namespace Utility
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : Object
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<T>();
                }
                
                return instance;
            }
        }
    }
}
