using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Player player;

    // 스킬 관리
    [SerializeField] SkillManager skillManager;
    private Dictionary<string, Skill> playerSkillDict = new Dictionary<string, Skill>();

    [SerializeField] Skill testSkill;

    public SkillManager SkillManager { get => skillManager; }

    private void Awake()
    {
        player = GetComponent<Player>(); 
    }

    private void Start()
    {
        AddSkill(testSkill);
    }   

    public void AddSkill(Skill skill)
    {
        Skill newSkill = null;
        string newSkillName = skill.SkillName;
        bool result;

        foreach(string existSkill in playerSkillDict.Keys)
        {
            if(existSkill == newSkillName)
            {
                result = skillManager.UpgradeSkill(playerSkillDict[existSkill], out newSkill);
                if (result)
                {
                    Skill prevSkill = playerSkillDict[existSkill];
                    playerSkillDict[existSkill] = newSkill;
                    playerSkillDict[existSkill].StartSkill();
                    prevSkill.StopSkill();
                    return;
                }
            }
        }    

        result = skillManager.AddNewSkill(newSkillName, out newSkill);
        if (result)
        {
            playerSkillDict.Add(newSkillName, newSkill);
            playerSkillDict[newSkillName].StartSkill();
        }
    }

    public int GetSkillLevel(Skill p_Skill)
    {
        int currentSkillLevel = -1;
        foreach(var skill in playerSkillDict)
        {
            if(skill.Value.SkillName == p_Skill.SkillName)
            {
                currentSkillLevel = skill.Value.SkillLevel;
            }
        }
        return currentSkillLevel;
    }
    
    public List<string> GetMaxLevelSkillFilter()
    {
        List<string> resultList = new List<string>();
        foreach(var skill in playerSkillDict)
        {
            if(skill.Value.SkillLevel == 5)
            {
                resultList.Add(skill.Value.SkillName);
            }
        }

        return resultList;
    }
}
