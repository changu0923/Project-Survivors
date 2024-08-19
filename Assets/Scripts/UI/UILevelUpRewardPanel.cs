using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelUpRewardPanel : MonoBehaviour
{
    [SerializeField] HorizontalLayoutGroup skillHolder;
    [SerializeField] GameObject rewardElementPrefab;

    private List<UIRewardSkillElement> currentElements = new List<UIRewardSkillElement>();

    private void OnEnable()
    {
        GetRandomSkill();
    }

    private void GetRandomSkill()
    {
        if ((currentElements.Count > 0)) currentElements.Clear();

        PlayerSkill skill = GameManager.Instance.Player.GetComponent<PlayerSkill>();
        Skill[] getRandomSkills = skill.GetRandomBaseSkill();

        foreach (Skill skillElement in getRandomSkills)
        {
            UIRewardSkillElement element = Instantiate(rewardElementPrefab).GetComponent<UIRewardSkillElement>();
            element.SetRewardElement(skillElement);
            element.transform.parent = skillHolder.transform;
            currentElements.Add(element);
        }
    }
}
