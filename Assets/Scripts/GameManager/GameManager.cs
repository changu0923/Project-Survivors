using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }   
    #endregion

    private bool isWin;
    private int roundTime;
    [SerializeField] int roundEndTime;
    private int killCount;
    private bool isPaused;

    [SerializeField] Player player;
    [SerializeField] UIManager uiManager;

    public Player Player { get => player; }
    public int RoundTime { get => roundTime; }
    public int KillCount { get => killCount; }
    public UIManager UiManager { get => uiManager; }
    public int RoundEndTime { get => roundEndTime; }

    public Action OnRoundTimeChanged;
    public Action OnKillCountChanged;

    private void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        // 게임 초기화 
        roundTime = 0;
        killCount = 0;
        StartCoroutine(RoundTimerCoroutine());
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused == true)
        {
            Time.timeScale = 0f;
        }
        else if (isWin == false)
        {
            Time.timeScale = 1f;
        }
    }

    public void PlayerLevelUpReward(int levelUpAmount)
    {
        uiManager.OnLevelUpRewardPopUP(levelUpAmount);
    }

    private void GameOver()
    {
        if(isWin == true)
        {
            // DO Win Action.
        }
        else
        {
            // Do Lose Action.
        }
    }

    public void AddKillCount()
    {
        killCount++;
        OnKillCountChanged?.Invoke();
    }

    IEnumerator RoundTimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            roundTime++;
            OnRoundTimeChanged?.Invoke();
        }
    }
}