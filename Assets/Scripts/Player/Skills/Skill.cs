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

    protected void InitSkill()
    {
        skillOnwer = GameManager.Instance.Player;
    }

    public void StartSkill()
    {
        InitSkill();

        if (skillCoroutine == null)
        {
            skillCoroutine = StartCoroutine(RepeatSkillCoroutine());
            Debug.Log($"StartCorutine : {skillName} level {skillLevel}");
        }
        Debug.Log($"StartSkill : {skillName} level {skillLevel}");
    }

    public void StopSkill()
    {
        Debug.Log($"Trying To StopSkill() {skillName}");
        gameObject.SetActive(false);
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
