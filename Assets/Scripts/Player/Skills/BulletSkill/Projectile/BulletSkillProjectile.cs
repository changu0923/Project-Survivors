using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class BulletSkillProjectile : MonoBehaviour
{
    private int damage;
    private float moveSpeed;
    private Vector2 moveDir;

    private Transform targetTransform;
    private Rigidbody2D rb;
    private Player player;


    Coroutine bulletDestoryCoroutine;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.velocity = Vector2.zero;
        damage = 0;
        moveSpeed = 0f;
        targetTransform = null;
        bulletDestoryCoroutine = null;
        player = GameManager.Instance.Player;
    }

    private void FixedUpdate()
    {
        if (targetTransform == null) return;

        Fly();
    }

    public void Shoot(int damage, float moveSpeed, Transform target)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        targetTransform = target;

        //방향을 계산하기
        moveDir = (Vector2)(targetTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        if (bulletDestoryCoroutine == null) { bulletDestoryCoroutine = StartCoroutine(BulletDestoryCoroutine()); }             
    }

    private void Fly()
    {        
        rb.velocity = moveDir * moveSpeed;
    }

    IEnumerator BulletDestoryCoroutine()
    { 
        yield return new WaitForSeconds(10f);
        ObjectPoolManager.Instance.Destroy("BulletSkillProjectile", gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Monster")
        {
            Monster currentMob = collision.transform.GetComponent<Monster>();
            currentMob.TakeDamage(this.damage);
            ObjectPoolManager.Instance.Destroy("BulletSkillProjectile", gameObject);
        }
    }
}
