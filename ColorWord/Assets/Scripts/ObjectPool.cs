using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject objectPrefab = null;
    private List<GameObject> instantiatedObjects = new List<GameObject>();
    private Transform objectPrefabParent = null;

    public ObjectPool(GameObject objectPrefab, int size, Transform parent = null)
    {
        this.objectPrefab = objectPrefab;
        this.objectPrefabParent = parent;

        for (int i = 0; i < size; i++)
        {
            GameObject obj = CreateObject();
            obj.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < instantiatedObjects.Count; i++)
        {
            if (!instantiatedObjects[i].activeSelf)
            {
                return instantiatedObjects[i];
            }
        }
        return CreateObject();
    }

    public void ReturnAllObjectsToPool()
    {
        for (int i = 0; i < instantiatedObjects.Count; i++)
        {
            instantiatedObjects[i].SetActive(false);
        }
    }

    private GameObject CreateObject()
    {
        GameObject obj = GameObject.Instantiate(objectPrefab);
        obj.transform.SetParent(objectPrefabParent, false);
        return obj;
    }
}
