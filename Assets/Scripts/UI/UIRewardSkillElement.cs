using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardSkillElement : MonoBehaviour
{
    private Image skillImage;
    private Text skillText;
    private Button okButton;
    private Skill skill;

    private void Awake()
    {
        skillImage = GetComponent<Image>();
        skillText = GetComponent<Text>();
        okButton = GetComponent<Button>();
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
