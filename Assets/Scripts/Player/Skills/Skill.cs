using System.Collections;
using UnityEngine;

public abstract class Skill : MonoBehaviour 
{
    private Player skillOnwer;

    [Header("��ų �̸�")]
    [SerializeField]string skillName;
    protected enum SKILLTYPE
    {
        ACTIVE = 0,
        PASSIVE = 1,
    }

    [Header("��ų Ÿ��")]
    [SerializeField] SKILLTYPE skillType;

    [Header("��ų ����")]
    [SerializeField]int skillLevel;
    private Coroutine skillCoroutine;

    [Header("��ų �̹���")]
    [SerializeField] Sprite skillSprite;

    [Header("��ų�� �� �� ���Ǵ� Ƚ��")]
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
