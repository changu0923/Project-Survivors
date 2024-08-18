using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSkillProjectile : MonoBehaviour
{
    private int damage;
    private float moveSpeed;

    public Transform targetTransform;
    private Rigidbody2D rb;

    Coroutine bulletDestoryCoroutine;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.velocity = Vector2.zero;
        damage = 0;
        moveSpeed = 0f;
        targetTransform = null;
        bulletDestoryCoroutine = null;
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
        if (bulletDestoryCoroutine == null) { bulletDestoryCoroutine = StartCoroutine(BulletDestoryCoroutine()); }             
    }

    private void Fly()
    {
        Vector2 dir = ((Vector2)targetTransform.position - rb.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        rb.velocity = dir * moveSpeed;
    }

    IEnumerator BulletDestoryCoroutine()
    { 
        yield return new WaitForSeconds(10f);
        ObjectPoolManager.Instance.Destory("BulletSkillProjectile", gameObject);
    }
}
