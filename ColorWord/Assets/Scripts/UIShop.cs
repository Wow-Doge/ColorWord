using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShop : MonoBehaviour
{
    [System.Serializable]
    class TileShop
    {
        public Sprite sprite;
        public int price;
        public bool isPurchased = false;
    }
    [System.Serializable]
    class BackgroundShop
    {
        public Sprite sprite;
        public int price;
        public bool isPurchased = false;
    }

    [SerializeField]
    List<TileShop> TilesList;

    [SerializeField]
    List<BackgroundShop> BackgroundsList;

    public GameObject tilePrefab;
    public GameObject tilesList;
    public GameObject backgroundPrefab;
    public GameObject backgroundsList;

    private ObjectPool tileItemObjectPool;
    private ObjectPool backgroundsItemObjectPool;

    Button button;

    private void Awake()
    {
        tileItemObjectPool = new ObjectPool(tilePrefab, 7, tilesList.transform);
        backgroundsItemObjectPool = new ObjectPool(backgroundPrefab, 6, backgroundsList.transform);
    }
    private void Start()
    {
        for (int i = 0; i < tilesList.transform.childCount; i++)
        {
            GameObject tile = tilesList.transform.GetChild(i).gameObject;
            tile.transform.GetChild(0).GetComponent<Image>().sprite = TilesList[i].sprite;
            tile.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = TilesList[i].price.ToString();
            button = tile.transform.GetChild(2).GetComponent<Button>();
            button.interactable = !TilesList[i].isPurchased;
            button.AddEventListener(i, OnTilesListPress);
            tile.SetActive(true);
        }

        for (int j = 0; j < backgroundsList.transform.childCount; j++)
        {
            GameObject background = backgroundsList.transform.GetChild(j).gameObject;
            background.transform.GetChild(0).GetComponent<Image>().sprite = BackgroundsList[j].sprite;
            background.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = BackgroundsList[j].price.ToString();
            button = background.transform.GetChild(2).GetComponent<Button>();
            button.interactable = !BackgroundsList[j].isPurchased;
            button.AddEventListener(j, OnBackgroundsListPress);
            background.SetActive(true);
        }
    }

    void OnTilesListPress(int itemIndex)
    {
        Debug.Log(itemIndex);
        TilesList[itemIndex].isPurchased = true;
        tilesList.transform.GetChild(itemIndex).GetChild(2).GetComponent<Button>().interactable = false;
        tilesList.transform.GetChild(itemIndex).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "BUY";
    }

    void OnBackgroundsListPress(int itemIndex)
    {
        Debug.Log(itemIndex);
        BackgroundsList[itemIndex].isPurchased = true;
        backgroundsList.transform.GetChild(itemIndex).GetChild(2).GetComponent<Button>().interactable = false;
        backgroundsList.transform.GetChild(itemIndex).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "BUY";
    }

    public void EnableUIShop()
    {
        gameObject.SetActive(true);
    }

    public void DisableUIShop()
    {
        gameObject.SetActive(false);
    }

    public void ShowTilesList()
    {
        tilesList.SetActive(true);
    }
    public void HideTilesList()
    {
        tilesList.SetActive(false);
    }
    public void ShowBackgroundsList()
    {
        backgroundsList.SetActive(true);
    }
    public void HideBackgroundsList()
    {
        backgroundsList.SetActive(false);
    }
}
