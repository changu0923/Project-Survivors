using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieMonster : Monster
{
    private Vector2 targetDir;
    private Coroutine AttackCoolTimeCoroutine;

    private void OnEnable()
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
        if (targetDir.x < 0)
        {
            IsFacingLeft = true;
        }
        else if (targetDir.x > 0)
        {
            IsFacingLeft = false;
        }
        Flip();
    }


    protected override void Move()
    {
        if (IsDead == true) return;

        Rb.velocity = targetDir * MoveSpeed;
    }

    private void Attack()
    {
        if (AttackFlag == true) return;

        AttackFlag = true; 
        Target.TakeDamage(Damage);
        StartCoroutine(AttackCoolDown(0.1f));
    }

    public override void TakeDamage(int damage)
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
        AttackFlag = false;
    }
}
