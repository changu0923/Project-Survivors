using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieMonster : Monster
{
    private Vector2 targetDir;
    private Coroutine AttackCoolTimeCoroutine;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        GetDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetDirection()
    {
        targetDir = (Target.transform.position - transform.position).normalized;
    }


    protected override void Move()
    {
        Rb.velocity = targetDir * MoveSpeed;
    }

    private void Attack()
    {
        if (AttackFlag == true) return;

        AttackFlag = true; 
        Target.TakeDamage(Damage);
        StartCoroutine(AttackCoolDown(0.1f));
    }

    protected override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Attack();
        }
    }

    IEnumerator AttackCoolDown(float t_wait)
    {
        yield return new WaitForSeconds(t_wait);
        Debug.Log("AttackCoolDown Called");
        AttackFlag = false;
    }
}
