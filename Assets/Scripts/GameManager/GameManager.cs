using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private CSVManager csvManager;

    public Player Player { get => player; }
    public int RoundTime { get => roundTime; }
    public int KillCount { get => killCount; }
    public UIManager UiManager { get => uiManager; }
    public int RoundEndTime { get => roundEndTime; }
    public bool IsWin { get => isWin;}
    public CSVManager CsvManager { get => csvManager; }

    public Action OnRoundTimeChanged;
    public Action OnKillCountChanged;
    public Action OnGameOverCalled;

    private void Start()
    {
        if (csvManager == null) 
        {
           csvManager = FindObjectOfType<CSVManager>();
        }
        InitGame();
    }

    private void InitGame()
    {
        // 게임 초기화 
        roundTime = 0;
        killCount = 0;
        OnRoundTimeChanged += CheckWinTimer;
        StartCoroutine(RoundTimerCoroutine());
        AudioManager.Instance.PlayBGM();
    }

    private void CheckWinTimer()
    {
        if (roundTime == roundEndTime)
        {
            GameOver(true);
        }
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

    public void GameOver(bool resultWin)
    {
        AudioManager.Instance.PlayBGM();
        if (resultWin == false)
        {
            AudioManager.Instance.Play(AudioManager.Instance.Lose);
            isWin = false;
        }
        else
        {
            AudioManager.Instance.Play(AudioManager.Instance.Win);
            isWin = true;
        }        
        OnGameOverCalled?.Invoke();
        uiManager.ShowGameOverPanel();
    }

    public void PlayerLevelUpReward(int levelUpAmount)
    {
        uiManager.OnLevelUpRewardPopUP(levelUpAmount);
    }

    public void AddKillCount()
    {
        killCount++;
        OnKillCountChanged?.Invoke();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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