using System.Collections;
using UnityEngine;

public abstract class Skill : MonoBehaviour 
{
    private Player skillOnwer;

    [Header("스킬 이름")]
    [SerializeField]string skillName;
    protected enum SKILLTYPE
    {
        ACTIVE = 0,
        PASSIVE = 1,
    }

    [Header("스킬 타입")]
    [SerializeField] SKILLTYPE skillType;

    [Header("스킬 레벨")]
    [SerializeField]int skillLevel;
    private Coroutine skillCoroutine;

    [Header("스킬 이미지")]
    [SerializeField] Sprite skillSprite;

    [Header("스킬이 분 당 사용되는 횟수")]
    [SerializeField]float skillRPM;

    private float skillCooltimeMult;

    public string SkillName { get => skillName; }
    public int SkillLevel { get => skillLevel; }
    public Player SetSkillOnwer { set => skillOnwer = value; }
    public Sprite SkillSprite { get => skillSprite; }

    public string SkillType { get { return skillType.ToString(); } }


    public virtual void Use()
    {

    }

    public void StartSkill()
    {
        if (skillCoroutine == null)
        {
            skillCoroutine = StartCoroutine(RepeatSkillCoroutine());
        }
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
