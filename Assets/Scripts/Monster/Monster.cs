using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] string monsterName;
    private int currentHP;
    [SerializeField] int maxHP;
    [SerializeField] int damage;
    [SerializeField] float moveSpeed;

    private bool isDead;
    private bool isFacingLeft;
    private bool attackFlag;

    private Player target;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    #region Properties
    public string MonsterName { get => monsterName; set => monsterName = value; }
    public int CurrentHP { get => currentHP; set => currentHP = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int Damage { get => damage; set => damage = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool IsFacingLeft { get => isFacingLeft; set => isFacingLeft = value; }
    public Player Target { get => target; set => target = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public bool AttackFlag { get => attackFlag; set => attackFlag = value; }
    #endregion

    protected virtual void Init()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();   
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        target = GameManager.Instance.Player;

        int monsterLayer = LayerMask.NameToLayer("Monster");
        gameObject.layer = monsterLayer;
        Color color = new Color(1, 1, 1, 1);
        spriteRenderer.color = color;
        currentHP = maxHP;
        isDead = false;
        AnimationInit();
    }

    protected virtual void Move()
    {
        // 플레이어를 향해 이동
    }

    public virtual void TakeDamage(int damage)
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
        rb.velocity = Vector2.zero;
        // 1. 죽는 애니메이션 재생하며, 리지드바디를 없애 몸 관통 가능하게 함
        int deadLayer = LayerMask.NameToLayer("Dead");
        gameObject.layer = deadLayer;
        AnimationDie();
        // 2. 애니메이션이 끝난 후 안보이게 하고, 오브젝트 풀에 집어넣음 
        StartCoroutine(RemoveBody());
    }

    #region Animation
    protected void Flip()
    {
        spriteRenderer.flipX = isFacingLeft;
    }

    protected void AnimationHit()
    {
        animator.SetTrigger("Hit");
    }

    protected void AnimationDie()
    {
        animator.SetBool("isDead", true);
        animator.SetTrigger("Die");
    }

    protected void AnimationInit()
    {
        animator.SetBool("isDead", false); 
        animator.SetTrigger("Init");
    }
    
    protected void AnimationReset()
    {
        animator.ResetTrigger("Hit");
        animator.ResetTrigger("Die");
        animator.ResetTrigger("Init");
    }

    #endregion

    IEnumerator RemoveBody()
    {
        yield return new WaitForSeconds(1.5f);
        AnimationReset();
        ObjectPoolManager.Instance.Destory(monsterName, gameObject);
    }
}