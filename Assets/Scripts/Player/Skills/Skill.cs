using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public abstract class Skill : MonoBehaviour 
{
    private Player skillOnwer;

    [Header("��ų �̸�")]
    [SerializeField]string skillName;
    private int skillLevel = 1;
    private Coroutine skillCoroutine;

    [Header("��ų�� �� �� ���Ǵ� Ƚ��")]
    [SerializeField]float skillRPM;
    private float skillCooltimeMult;

    public string SkillName { get => skillName; }
    public Player SetSkillOnwer { set => skillOnwer = value; }

    public virtual void Use()
    {

    }

    public void SkillLevelUp()
    {
        skillLevel++;
    }

    private float CalculateSkillRPM()
    {
        float rpm = 60f / skillRPM;
        float result = rpm * (1f - skillCooltimeMult) * (1f - skillOnwer.SkillCoolTimeMult);
        return result;
    }   

    IEnumerator RepeatSkillCoroutine()
    {
        while (true)
        {
            Use();
            yield return new WaitForSeconds(CalculateSkillRPM());
        }
    }
}
