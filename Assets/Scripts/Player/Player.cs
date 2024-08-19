using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int currentHP;
    [SerializeField] int maxHP;
    private int currentEXP;
    private int maxEXP = 100;
    private int level = 1;
    private float moveSpeed = 2.0f;
    private float speedMult = 1.0f;
    private float skillCoolTimeMult;
    private bool isDead;

    public Action OnPlayerHealthChanged;
    public Action OnPlayerExpChanged;

    public float MoveSpeed { get { return moveSpeed * speedMult; } }
    public float SkillCoolTimeMult { get { return skillCoolTimeMult; } }
    public bool isAlive { get { return !isDead; } }

    public int CurrentHP { get => currentHP; }
    public int MaxHP { get => maxHP; }
    public int CurrentEXP { get => currentEXP; set => currentEXP = value; }
    public int MaxEXP { get => maxEXP; set => maxEXP = value; }
    public int Level { get => level; set => level = value; }

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

    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHP -= dmg;
        OnPlayerHealthChanged?.Invoke();

        if(currentHP <= 0)
        {
            currentHP = 0;
            isDead = true;
            Die();
        }
    }

    public void GainHP(int iValue)
    {

    }

    public void GainEXP(int iValue)
    {
        currentEXP += iValue;
        if (currentEXP >= maxEXP)
        {
            LevelUP();
        }
        OnPlayerExpChanged?.Invoke();
    }

    public void LevelUP()
    {
        int levelupCount = 0;
        while (currentEXP >= maxEXP)
        {
            currentEXP -= maxEXP;
            level++;
            levelupCount++;
        }
        GameManager.Instance.PlayerLevelUpReward(levelupCount);
    }

    private void Die()
    {

    }   
}