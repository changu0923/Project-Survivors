using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UITopBarPanel : MonoBehaviour
{
    private Player player;
    [SerializeField] Slider expSlider;
    [SerializeField] Text levelText;
    [SerializeField] Text roundTimeText;
    [SerializeField] Text killCountText;

    private StringBuilder sb = new StringBuilder();
    private int currentKillCount = 0;

    private void Awake()
    {
        player = GameManager.Instance.Player;
        GameManager.Instance.OnRoundTimeChanged += UpdateRoundTime;
        GameManager.Instance.OnKillCountChanged += UpdateKillCount;
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

    private void UpdateKillCount()
    {
        currentKillCount = GameManager.Instance.KillCount;
        sb.Clear();
        sb.Append(currentKillCount.ToString());
        killCountText.text = sb.ToString();
    }

}
