using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int currentHP;
    private int maxHP;
    private int currentEXP;
    private int maxEXP;
    private int level = 1;
    private float moveSpeed = 2.0f;
    private float speedMult = 1.0f;
    private float skillCoolTimeMult;
    private bool isDead;

    public float MoveSpeed { get { return moveSpeed * speedMult; } }
    public float SkillCoolTimeMult { get { return skillCoolTimeMult; } }
    public bool isAlive { get { return !isDead; } }


    private void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        isDead = false;
        currentHP = maxHP;
        currentEXP = 0;
        speedMult = 1.0f;
    }
}