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
        // �÷��̾ ���� �̵�
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
        // 1. �״� �ִϸ��̼� ����ϸ�, ������ٵ� ���� �� ���� �����ϰ� ��
        // 2. �ִϸ��̼��� ���� �� �Ⱥ��̰� �ϰ�, ������Ʈ Ǯ�� ������� 
    }

    #region Animation
    private void Flip()
    {
        // if condition
        spriteRenderer.flipX = isFacingLeft;
    }
    #endregion
}