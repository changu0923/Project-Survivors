using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillPanel : MonoBehaviour
{
    [SerializeField] HorizontalLayoutGroup activeSkillPanel;
    [SerializeField] HorizontalLayoutGroup passiveSkillPanel;
    [SerializeField] GameObject skillElement;

    private List<UISkillElements> activeSkillList = new List<UISkillElements>();
    private List<UISkillElements> passiveSkillList = new List<UISkillElements>();

    public void AddSkillIcon(Skill skill)
    {
        UISkillElements element = Instantiate(skillElement).GetComponent<UISkillElements>();
        string currentSkillType = skill.SkillType;

        if (string.Compare(currentSkillType, "ACTIVE") == 0)
        {
            if (activeSkillList.Count < 6)
            {
                element.SetImage(skill.SkillSprite);
                element.transform.parent = activeSkillPanel.transform;
                activeSkillList.Add(element);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Active skill list is Full.");
#endif
                Destroy(element);
            }
        }
        else if (string.Compare(currentSkillType, "PASSIVE") == 0)
        {
            if (passiveSkillList.Count < 6)
            {
                element.SetImage(skill.SkillSprite);
                element.transform.parent = passiveSkillPanel.transform;
                passiveSkillList.Add(element);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Passive skill list is Full");
#endif
                Destroy(element);
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("SkillType Error");
#endif
            Destroy(element);
        }
    }
}