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
    private int maxEXP;
    private int level = 1;
    private float moveSpeed = 2.0f;
    private float speedMult = 1.0f;
    private float skillCoolTimeMult;
    private bool isDead;

    public Action OnPlayerHealthChanged;

    public float MoveSpeed { get { return moveSpeed * speedMult; } }
    public float SkillCoolTimeMult { get { return skillCoolTimeMult; } }
    public bool isAlive { get { return !isDead; } }

    public int CurrentHP { get => currentHP; }
    public int MaxHP { get => maxHP; }

    private void Start()
    {
        InitGame();
        StartCoroutine(TestCoroutine());
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

    private void Die()
    {

    }

    IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(3);
        TakeDamage(10);
        yield return new WaitForSeconds(3);
        TakeDamage(20);
    }
}