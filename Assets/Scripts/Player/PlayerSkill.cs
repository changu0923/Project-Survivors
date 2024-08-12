using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Player player;
    private Dictionary<Skill, string> skillDict = new Dictionary<Skill, string>();

    private void Awake()
    {
       player = GetComponent<Player>(); 
    }

    private void Start()
    {

    }

    public void AddSkill(Skill skill)
    {
        if(skillDict.ContainsValue(skill.name))
        {
            skill.SkillLevelUp();
        }
        else
        {
            skillDict.Add(skill, skill.name);
            skill.SetSkillOnwer = player;
            skill.StartSkill();
        }
    }
}
