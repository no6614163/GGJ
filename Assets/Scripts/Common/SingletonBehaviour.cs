using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyUtils
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                        instance = obj.GetComponent<T>();
                    }
                }
                return instance;
            }
        }
        static T instance = null;

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = GetComponent<T>();
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }

}