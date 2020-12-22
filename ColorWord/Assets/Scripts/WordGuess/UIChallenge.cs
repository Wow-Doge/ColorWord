using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChallenge : MonoBehaviour
{
    public Transform challengeListContainer;
    public GameObject challengeListPrefab;

    private ObjectPool challengeItemObjectPool;

    public List<ListOfChallenge> ListOfCurrentGoals { get; set; }
    public void Awake()
    {
        challengeItemObjectPool = new ObjectPool(challengeListPrefab, 3, challengeListContainer);
    }

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        SetupChallenge();
    }

    public void SetupChallenge()
    {
        List<ChallengeInfo> challengeInfo = GuessManager.Instance.ChallengeInfos;
        challengeItemObjectPool.ReturnAllObjectsToPool();
        PlayerData playerData = SaveLoadSystem.Load();

        if (playerData != null)
        {
            for (int i = 0; i < challengeListContainer.childCount; i++)
            {
                GameObject challengeGO = challengeListContainer.GetChild(i).gameObject;
                challengeGO.SetActive(true);
                ChallengeListItem challenge = challengeGO.transform.GetComponent<ChallengeListItem>();
                challenge.Setup(challengeInfo[i].title, playerData.challengeList[i].curGoal, challengeInfo[i].goal, challengeInfo[i].challengeDescription);
            }
        }
        else
        {
            for (int i = 0; i < challengeListContainer.childCount; i++)
            {
                GameObject challengeGO = challengeListContainer.GetChild(i).gameObject;
                challengeGO.SetActive(true);
                ChallengeListItem challenge = challengeGO.transform.GetComponent<ChallengeListItem>();
                challenge.Setup(challengeInfo[i].title, 0, challengeInfo[i].goal, challengeInfo[i].challengeDescription);
            }
        }
    }

    public void ShowChallenge()
    {
        UIManager.Instance.SlideDown(gameObject);
    }

    public void HideChallenge()
    {
        UIManager.Instance.SlideUp(gameObject);
    }
}
