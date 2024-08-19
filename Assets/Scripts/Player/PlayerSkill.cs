using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Player player;

    // ��ų ����
    [SerializeField] List<Skill> skillList = new List<Skill>();
    [SerializeField] List<Skill> baseSkillList = new List<Skill>();
    [SerializeField] Transform skillStorage;
    private Dictionary<string, Skill> skillListDict = new Dictionary<string, Skill>();
    private Dictionary<string, Skill> currentSkillDict = new Dictionary<string, Skill>();

    [SerializeField] Skill testSkill;

    private void Awake()
    {
        player = GetComponent<Player>(); 
        InitSkillDict();
    }

    private void Start()
    {
        AddSkill(testSkill);
    }

    private void InitSkillDict()
    {
        foreach (Skill skill in skillList)
        {
            string skillName = skill.GetType().Name;
            skillListDict.Add(skillName, skill);
        }    
    }

    public Skill[] GetRandomBaseSkill()
    {
        // TODO : ��ų �߰��� �����ϱ�
        int maxRange = baseSkillList.Count;
        Skill[] randomSkillArray = new Skill[3];

        for (int i = 0; i<3; i++)
        {
            int randomIndex = Random.Range(0, maxRange);
            randomSkillArray[i] = baseSkillList[randomIndex];
        }

        return randomSkillArray;
    }

    public void AddSkill(Skill skill)
    {       
        string newSkillName = skill.SkillName;

        // ��ų�� �̹� �������ִ��� �˻�
        foreach(string existSkill in currentSkillDict.Keys)
        {
            if(skill.SkillName == existSkill)
            {
                UpgradeSkill(currentSkillDict[existSkill]);
                return;
            }
        }

        // ���ο� ��ų�̸� skillListDict���� ��������
        foreach (Skill newSkill in skillListDict.Values)
        {            
            if(newSkill.SkillName == newSkillName && newSkill.SkillLevel == 1)
            {
                GameObject newSkillObj = Instantiate(newSkill.gameObject, transform.position, Quaternion.identity);
                newSkillObj.transform.parent = skillStorage.transform;
                newSkillObj.transform.localPosition = skillStorage.transform.localPosition;

                Skill newSkillObjInstance = newSkillObj.GetComponent<Skill>();

                currentSkillDict.Add(newSkillObjInstance.SkillName, newSkillObjInstance);
                newSkillObjInstance.StartSkill();
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
