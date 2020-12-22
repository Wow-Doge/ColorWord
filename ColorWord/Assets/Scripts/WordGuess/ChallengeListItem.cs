using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChallengeListItem : MonoBehaviour
{
    public string challengeName;
    public int curGoal { get; set; } = 0;
    public int maxGoal;
    public string description;

    public TextMeshProUGUI challengeNameText;
    public TextMeshProUGUI fillText;
    public Image fill;

    public void Setup(string challengeName, int currentGoal, int maxGoal, string description)
    {
        this.challengeName = challengeName;
        this.curGoal = currentGoal;
        this.maxGoal = maxGoal;
        this.description = description;

        challengeNameText.text = description.ToString();
        ChallengeUIUpdate();
    }

    public void ChallengeUIUpdate()
    {
        fillText.text = curGoal.ToString() + "/" + maxGoal.ToString();
        float curGoalFloat = curGoal;
        float maxGoalFloat = maxGoal;
        float result = curGoalFloat / maxGoalFloat;
        fill.fillAmount = result;
    }
}
