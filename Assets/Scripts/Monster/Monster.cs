using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [Header("몬스터 정보")]
    [SerializeField] string monsterName;
    private int currentHP;
    [SerializeField] int maxHP;
    [SerializeField] int damage;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject damageFont;

    [Header("드랍 아이템 정보")]
    [SerializeField] List<GameObject> itemList = new List<GameObject>(); 

    private bool isDead;
    private bool isFacingLeft;
    private bool attackFlag;
    private bool isGameOver;

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
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    #endregion

    protected virtual void Init()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();   
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        target = GameManager.Instance.Player;
        GameManager.Instance.OnGameOverCalled += GameOverEventCalled;

        int monsterLayer = LayerMask.NameToLayer("Monster");
        gameObject.layer = monsterLayer;
        currentHP = maxHP;
        isDead = false;
        isGameOver = !target.isAlive;
        AnimationInit();
    } 

    protected virtual void Move()
    {

    }

    public virtual void TakeDamage(int damage)
    {
        if(isDead == true) return;

        currentHP -= damage;
        ShowDamageFont(damage);

        if(currentHP <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private void GameOverEventCalled()
    {
        isGameOver = true;
    }

    protected virtual void Die()     
    {
        rb.velocity = Vector2.zero;
        DropItem();
        GameManager.Instance.AddKillCount();
        // 1. 죽는 애니메이션 재생하며, 리지드바디를 없애 몸 관통 가능하게 함
        int deadLayer = LayerMask.NameToLayer("Dead");
        gameObject.layer = deadLayer;
        AnimationDie();
        // 2. 애니메이션이 끝난 후 안보이게 하고, 오브젝트 풀에 집어넣음 
        StartCoroutine(RemoveBody());
    }

    protected virtual void DropItem()
    {
        foreach(var item in itemList)
        {
            Item currentItem = item.GetComponent<Item>();
            float currentChance = currentItem.ItemDropChance;

            float magicNumber = Random.Range(0f, 1f);
            if(magicNumber <= currentChance)
            {
                GameObject obj = ObjectPoolManager.Instance.Instantiate(currentItem.ItemName, currentItem.gameObject);
                obj.transform.position = transform.position;
                obj.transform.parent = ObjectPoolManager.Instance.transform;
            }
        }        
    }

    protected void ShowDamageFont(int p_damage)
    {
        GameObject obj = ObjectPoolManager.Instance.Instantiate("DamageFont", damageFont);
        obj.GetComponent<UIDamageFont>().SetDamageFont(p_damage, transform);
        obj.transform.SetParent(ObjectPoolManager.Instance.transform);
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