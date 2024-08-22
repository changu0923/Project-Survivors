using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Player player;

    // ��ų ����
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
}
