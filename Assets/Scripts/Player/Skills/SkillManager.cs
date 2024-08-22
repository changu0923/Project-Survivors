using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] List<Skill> skillList = new List<Skill>();
    private List<Skill> baseSkillList = new List<Skill>();  
    private Dictionary<string, Skill> skillDictionary =new Dictionary<string, Skill>();

    public Dictionary<string, Skill> SkillDictionary { get => skillDictionary; }

    private void Awake()
    {
        InitSkillDict();
    }

    private void InitSkillDict()
    {
        foreach (Skill skill in skillList)
        {
            string skillName = skill.GetType().Name;
            skillDictionary.Add(skillName, skill);
            if(skill.SkillLevel == 0)
            {
                baseSkillList.Add(skill);
            }
        }
    }
    public Skill[] GetRandomBaseSkill()
    {
        // TODO : 스킬 추가후 개선하기
        int maxRange = baseSkillList.Count;
        Skill[] randomSkillArray = new Skill[3];

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, maxRange);
            randomSkillArray[i] = baseSkillList[randomIndex];
        }

        return randomSkillArray;
    }

    public bool AddNewSkill(string skillName, out Skill returnSkill)
    {
        string newSkillName = skillName;

        foreach (Skill newSkill in skillDictionary.Values)
        {
            if (newSkill.SkillName == newSkillName && newSkill.SkillLevel == 1)
            {
                GameObject newSkillObj = Instantiate(newSkill.gameObject, transform.position, Quaternion.identity);
                newSkillObj.transform.SetParent(transform);
                newSkillObj.transform.localPosition = transform.localPosition;
                returnSkill = newSkillObj.GetComponent<Skill>();
                return true;
            }
        }

        returnSkill = null;
#if UNITY_EDITOR
        Debug.LogError("스킬 추가 실패. 스킬을 찾을 수 없습니다.");
#endif
        return false;
    }

    public bool UpgradeSkill(Skill targetSkill, out Skill upgradeSkill)
    {
        string targetSkillName = targetSkill.SkillName;
        int targetSkillLevel = targetSkill.SkillLevel + 1;

        foreach (Skill findSkill in skillDictionary.Values)
        {
            if (findSkill.SkillName == targetSkillName && findSkill.SkillLevel == targetSkillLevel)
            {
                GameObject newSkillObj = Instantiate(findSkill.gameObject, transform.position, Quaternion.identity);
                newSkillObj.transform.SetParent(transform);
                newSkillObj.transform.localPosition = transform.localPosition;
                upgradeSkill = newSkillObj.GetComponent<Skill>();
                return true;
            }
        }
        upgradeSkill = null;
#if UNITY_EDITOR
        Debug.LogError("스킬 업그레이드 실패. 스킬을 찾을 수 없거나, 스킬이 이미 만렙입니다.");
#endif
        return false;
    }
}
