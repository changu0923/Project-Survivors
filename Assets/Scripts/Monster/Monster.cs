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

        currentHP = maxHP;
        isDead = false;        
    }

    protected virtual void Move()
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
        ObjectPoolManager.Instance.Destory(monsterName, this.gameObject);
    }

    #region Animation
    private void Flip()
    {
        // if condition
        spriteRenderer.flipX = isFacingLeft;
    }
    #endregion
}