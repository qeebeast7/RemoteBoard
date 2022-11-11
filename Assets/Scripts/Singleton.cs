using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    private static bool isDestroy=false;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (isDestroy)
            {
                return default(T);
            }

            lock (_lock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    //DontDestroyOnLoad(instance.gameObject);
                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        return instance;
                    }
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).ToString();
                        instance = obj.AddComponent<T>();
                        //DontDestroyOnLoad(obj);
                    }
                }
                return instance;
            }
        }
    }

    private void OnDestroy()
    {
        isDestroy = true;
    }
}
