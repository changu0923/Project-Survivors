using Unity.VisualScripting;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] string monsterName;
    private int currentHP;
    [SerializeField] int maxHP;
    [SerializeField] int damage;

    private bool isDead;
    private bool isFacingLeft;

    private Player player;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    protected virtual void Init()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();   
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        currentHP = maxHP;
        isDead = false;
        
    }

    protected virtual void Attack()
    {
        // 플레이어를 향해 이동
    }

    protected virtual void TakeDamage(int damage)
    {
        if(isDead == true) return;

        currentHP -= damage;
        if(currentHP <= 0)
        {
            isDead = true;
            Die();
        }
    }

    protected virtual void Die()
    {
        // 1. 죽는 애니메이션 재생하며, 리지드바디를 없애 몸 관통 가능하게 함
        // 2. 애니메이션이 끝난 후 안보이게 하고, 오브젝트 풀에 집어넣음 
    }

    #region Animation
    private void Flip()
    {
        // if condition
        spriteRenderer.flipX = isFacingLeft;
    }
    #endregion
}