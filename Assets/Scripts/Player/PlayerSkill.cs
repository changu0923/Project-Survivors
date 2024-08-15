using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Player player;

    // 스킬 관리
    [SerializeField] List<Skill> skillList = new List<Skill>(); 
    private Dictionary<string, Skill> skillListDict = new Dictionary<string, Skill>();
    private Dictionary<string, Skill> currentSkillDict = new Dictionary<string, Skill>();   

    private void Awake()
    {
        player = GetComponent<Player>(); 
        InitSkillDict();
    }

    private void Start()
    {

    }

    private void InitSkillDict()
    {
        foreach (Skill skill in skillList)
        {
            string skillName = skill.GetType().Name;
            skillListDict.Add(skillName, skill);
        }    
    }

    public void AddSkill(Skill skill)
    {       
        string newSkillName = skill.SkillName;

        // 스킬을 이미 가지고있는지 검사
        foreach(string existSkill in currentSkillDict.Keys)
        {
            if(skill.SkillName == existSkill)
            {
                UpgradeSkill(currentSkillDict[existSkill]);
                return;
            }
        }

        // 새로운 스킬이면 skillListDict에서 가져오기
        foreach (Skill newSkill in currentSkillDict.Values)
        {            
            if(newSkill.SkillName == newSkillName && newSkill.SkillLevel == 1)
            {
                currentSkillDict.Add(newSkill.SkillName, newSkill);
            }
        }
    }

    private void UpgradeSkill(Skill targetSkill)
    {
        string targetSkillName = targetSkill.SkillName;
        int targetSkillLevel = targetSkill.SkillLevel + 1;

        foreach(Skill findSkill in skillListDict.Values)
        {
            if(findSkill.SkillName == targetSkillName && findSkill.SkillLevel == targetSkillLevel)
            {
                currentSkillDict[targetSkill.SkillName] = findSkill;
            }
        }
    }
}
