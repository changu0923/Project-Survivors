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

    [SerializeField] Player player;

    public Player Player { get => player; }
    public int RoundTime { get => roundTime; }

    public Action OnRoundTimeChanged;

    private void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        // 게임 초기화 
        roundTime = 0;
        StartCoroutine(RoundTimerCoroutine());
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