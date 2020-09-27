using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarCount : MonoBehaviour
{
    TextMeshProUGUI countingStarText;
    public int numOfStar;
    void Start()
    {
        countingStarText = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        countingStarText.text = ": " + numOfStar;
    }
}
