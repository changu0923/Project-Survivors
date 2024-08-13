using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITopBarPanel : MonoBehaviour
{
    private Player player;
    [SerializeField] Slider expSlider;
    [SerializeField] Text levelText;
    [SerializeField] Text roundTimeText;

    private void Awake()
    {
        player = GameManager.Instance.Player;
        GameManager.Instance.OnRoundTimeChanged += UpdateRoundTime;
        player.OnPlayerExpChanged += UpdateEXPSlider;
    }

    private void UpdateRoundTime()
    {
        int min = GameManager.Instance.RoundTime / 60;
        int sec = GameManager.Instance.RoundTime % 60;
        string currentTime = string.Format("{0:00}:{1:00}", min, sec);
        roundTimeText.text = currentTime;
    }

    private void UpdateEXPSlider()
    {
        expSlider.value = ((float)player.CurrentEXP / player.MaxEXP);
        string levelString = "LV " + player.Level.ToString();
        levelText.text = levelString;
    }

}
