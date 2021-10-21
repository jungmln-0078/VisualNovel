using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon<T> where T : SingleTon<T>, new()
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
}

public class MonoSingleTon<T>: MonoBehaviour where T : MonoBehaviour, new()
{
    private static T _instance;
    private static bool _isDestroyed = false;
    private static readonly object _lock = new object();
    public static T Instance
    {
        get
        {
            if (_isDestroyed)
            {
                Debug.Log($"Destroyed {typeof(T)}");
                return null;
            }
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance == null)
                    {
                        var obj = new GameObject();
                        _instance = obj.AddComponent<T>();
                        obj.name = typeof(T).ToString();
                        DontDestroyOnLoad(obj);
                    }
                }
                return _instance;
            }
        }
    }
    void OnDestroy()
    {
        _isDestroyed = true;
    }
}   
