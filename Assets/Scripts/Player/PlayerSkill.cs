using Mono.Cecil;
using System;
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

    public Action<Skill> OnSkillAddFirstTime;


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
            OnSkillAddFirstTime?.Invoke(playerSkillDict[newSkillName]);
        }
    }

    public int GetSkillLevel(Skill p_Skill)
    {
        int currentSkillLevel = 0;
        if (playerSkillDict.TryGetValue(p_Skill.SkillName, out Skill targetSkill)) 
        {
            currentSkillLevel = targetSkill.SkillLevel;            
        }

        return currentSkillLevel + 1;
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
