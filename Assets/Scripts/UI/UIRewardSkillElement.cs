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
    private UILevelUpRewardPanel levelUpRewardPanel;

    private void OnEnable()
    {
        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(OnOkButtonClicked);
        levelUpRewardPanel = GetComponentInParent<UILevelUpRewardPanel>();
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
        levelUpRewardPanel.gameObject.SetActive(false);
    }
}
