using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject products;

    private ObjectPool tileItemObjectPool;
    public void EnableUIShop()
    {
        gameObject.SetActive(true);
    }

    public void DisableUIShop()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        tileItemObjectPool = new ObjectPool(tilePrefab, 9, products.transform);
    }
    private void Start()
    {
        for (int i = 0; i < products.transform.childCount; i++)
        {
            GameObject tile = products.transform.GetChild(i).gameObject;
            tile.SetActive(true);
        }
    }
}
