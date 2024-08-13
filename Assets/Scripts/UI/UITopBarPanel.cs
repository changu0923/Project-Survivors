using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITopBarPanel : MonoBehaviour
{
    [SerializeField] Slider expSlider;
    [SerializeField] Text roundTimeText;

    private void Awake()
    {
        GameManager.Instance.OnRoundTimeChanged += UpdateRoundTime;
    }

    private void UpdateRoundTime()
    {
        int min = GameManager.Instance.RoundTime / 60;
        int sec = GameManager.Instance.RoundTime % 60;
        string currentTime = string.Format("{0:00}:{1:00}", min, sec);
        roundTimeText.text = currentTime;
    }

}
