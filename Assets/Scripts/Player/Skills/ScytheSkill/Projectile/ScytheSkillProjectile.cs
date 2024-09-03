using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSkillProjectile : MonoBehaviour
{
    private Transform targetTransform;
    [SerializeField] Transform rendererTransform;
    private Vector2 moveDir;
    private int damage;
    private float moveSpeed;

    private void OnEnable()
    {
        targetTransform = null;
        moveDir = Vector3.zero;
    }

    private void Update()
    {
        Fly();
    }

    public void Shoot(int damage, float moveSpeed, Transform targetTransform)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.targetTransform = targetTransform;
        moveDir = (targetTransform.position - transform.position).normalized;
        StartCoroutine(DestoryCoroutine());
    }

    private void Fly()
    {
        rendererTransform.Rotate(-Vector3.forward * 360 * Time.deltaTime);
        if (targetTransform == null)
            return;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            Monster currentMob = collision.transform.GetComponent<Monster>();
            currentMob.TakeDamage(damage);
        }
    }

    IEnumerator DestoryCoroutine()
    {
        yield return new WaitForSeconds(10f);
        ObjectPoolManager.Instance.Destory("ScytheSkillProjectile", gameObject);
    }
}
