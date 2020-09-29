using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    public GameObject wordPrefab;
    public Transform wordCanvas;
    public WordDisplay SpawnWord()
    {
        Vector3 randomPos = new Vector3(Random.Range(-3f, 3f), 8f, 0f);

        GameObject wordObj = Instantiate(wordPrefab, randomPos, Quaternion.identity, wordCanvas);
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();

        return wordDisplay;
    }
}
