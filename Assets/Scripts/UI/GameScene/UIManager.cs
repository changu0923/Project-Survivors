using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UILevelUpRewardPanel uILevelUpRewardPanel;

    public UILevelUpRewardPanel UILevelUpRewardPanel { get => uILevelUpRewardPanel; }

    public void OnLevelUpRewardPopUP(int levelAmount)
    {
        StartCoroutine(LevelUPRewardCoroutine(levelAmount));
    }

    IEnumerator LevelUPRewardCoroutine(int levelAmount)
    {
        PlayerSkill playerSkill = GameManager.Instance.Player.GetComponent<PlayerSkill>();
        int count = 0;
        while (count < levelAmount)
        {
            GameManager.Instance.PauseGame();
            uILevelUpRewardPanel.gameObject.SetActive(true);
            yield return new WaitUntil(() => !uILevelUpRewardPanel.gameObject.activeSelf);
            GameManager.Instance.PauseGame();
            count++;
        }
    }
}
