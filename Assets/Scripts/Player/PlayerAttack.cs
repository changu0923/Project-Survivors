using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Dictionary<Skill, string> skillDict = new Dictionary<Skill, string>();
    
    public void AddSkill(Skill skill)
    {
        if(skillDict.ContainsValue(skill.name))
        {
            skill.SkillLevelUp();
        }
        else
        {
            skillDict.Add(skill, skill.name);            
        }
    }
}
