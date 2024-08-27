using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShovelSkill03 : Skill
{
    [Header("스킬 설정")]
    [SerializeField] int weaponDamage;
    [SerializeField] int weaponRPM;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] float scaleX;
    [SerializeField] float scaleY;

    private Player player;
    private Rigidbody2D rb;

    private Coroutine skillCoroutine;
    private bool isActive;

    private void Awake()
    {
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
        player = GameManager.Instance.Player;
        rb = GetComponent<Rigidbody2D>();    
        transform.SetParent(player.transform);
        transform.localPosition = Vector3.zero; 

    }

    public override void Use()
    {
        if (!isActive)
        {
            isActive = true;
            skillCoroutine = StartCoroutine(ShovelSpinCoroutine());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.TakeDamage(weaponDamage);
        }
    }

    IEnumerator ShovelSpinCoroutine()
    {
        float rotateSpeed = weaponRPM / 60f;
        float rotateDegree = rotateSpeed * 360f;

        while (isActive)
        {
            float rotateValue = rotateDegree * Time.deltaTime;
            rb.MoveRotation(rb.rotation * rotateValue);
            yield return null;
        }
    }
}
