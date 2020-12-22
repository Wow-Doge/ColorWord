using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace ColorWord
{
    public enum RewardType
    {
        Coins,
    }

    [Serializable]
    public struct Reward
    {
        public RewardType type;
        public int amount;
    }
    public class DailyReward : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI coinsText;

        [SerializeField]
        RewardsDatabase rewardsDB;

        [SerializeField]
        GameObject rewardPanel;

        [SerializeField]
        GameObject noRewardPanel;

        [SerializeField]
        TextMeshProUGUI rewardAmount;

        private int nextRewardIndex { get; set; } = 0;
        void Start()
        {
            PlayerData playerData = SaveLoadSystem.Load();
            nextRewardIndex = playerData.rewardsIndex;
            UpdateCoinsTextUI();
            CheckForReward();
        }

        public void ClaimRewardButton()
        {
            PlayerData playerData = SaveLoadSystem.Load();

            Reward reward = rewardsDB.GetReward(nextRewardIndex);
            //playerData.coins += reward.amount;
            GuessGameplay.Instance.CoinNumber += reward.amount;
            UpdateCoinsTextUI();

            //save next reward index
            nextRewardIndex++;
            if (nextRewardIndex >= rewardsDB.rewardsCount)
            {
                nextRewardIndex = 0;
            }
            playerData.rewardsIndex = nextRewardIndex;

            SaveLoadSystem.Save();
            DeactivateReward();
        }

        public void UpdateCoinsTextUI()
        {
            PlayerData playerData = SaveLoadSystem.Load();
            coinsText.text = playerData.coins.ToString();
        }

        public void OpenDailyRewardUI()
        {
            gameObject.SetActive(true);
        }

        public void CloseDailyRewardUI()
        {
            gameObject.SetActive(false);
        }

        public void ActivateReward()
        {
            rewardPanel.SetActive(true);
            noRewardPanel.SetActive(false);

            Reward reward = rewardsDB.GetReward(nextRewardIndex);
            rewardAmount.text = string.Format("+{0}", reward.amount);
        }

        public void DeactivateReward()
        {
            rewardPanel.SetActive(false);
            noRewardPanel.SetActive(true);
        }

        void CheckForReward()
        {
            ActivateReward();
        }
    }
}