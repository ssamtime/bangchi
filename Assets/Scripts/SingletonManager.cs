using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object _Lock = new object();
    private static T instance;

    public static T Instance
    {
        get
        {
            lock(_Lock)
            {
                if(instance == null)
                {
                    GameObject obj;
                    obj = GameObject.Find(typeof(T).Name);

                    if(obj == null)
                    {
                        obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                    }
                    else
                    {
                        instance = obj.GetComponent<T>();
                    }
                }
                return instance;
            }
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
