using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonComponent<T> : MonoBehaviour where T : Object
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("[SingletonComponent] Returning null instance for component of type {0}.");
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        SetInstance();
    }

    public static bool Exist()
    {
        return instance != null;
    }

    public void SetInstance()
    {
        if (instance != null && instance != gameObject.GetComponent<T>())
        {
            Debug.Log("[SingletonComponent] Instance already set for type ");
            return;
        }

        instance = gameObject.GetComponent<T>();
    }
}
