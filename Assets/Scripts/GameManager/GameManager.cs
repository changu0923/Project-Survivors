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
    

    private void InitGame()
    {
        // 게임 초기화 
        roundTime = 0;        
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
            // 여기서 라운드 시간 계속 증가
            yield return null;
        }
    }
}