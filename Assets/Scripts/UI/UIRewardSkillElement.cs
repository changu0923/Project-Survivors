using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardSkillElement : MonoBehaviour
{
    [SerializeField] Image skillImage;
    [SerializeField] Text skillText;
    [SerializeField] Text skillLevelText;
    [SerializeField] Button okButton;
    private Skill skill;

    private void Awake()
    {
        okButton.onClick.AddListener(OnOkButtonClicked);
    }

    public void SetRewardElement(Skill p_skill)
    {
        skill = p_skill;
        skillImage.sprite = skill.SkillSprite;
        skillText.text = skill.SkillName;
    }

    private void OnOkButtonClicked()
    {
        Player player = GameManager.Instance.Player;
        PlayerSkill targetPlayer = player.GetComponent<PlayerSkill>();  
        targetPlayer.AddSkill(skill);
    }

}
