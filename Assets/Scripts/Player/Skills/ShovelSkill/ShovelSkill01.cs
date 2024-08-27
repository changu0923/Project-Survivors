using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShovelSkill01 : Skill
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
    private bool isActive = false;

    private void Awake()
    {
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
        player = GameManager.Instance.Player;
        rb = GetComponent<Rigidbody2D>();    
    }

    public override void Use()
    {
        if (!isActive)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.zero;
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

        while (isActive == true)
        {
            float rotateValue = rotateDegree * Time.deltaTime;
            transform.Rotate(0, 0, -rotateValue);
            yield return null;
        }
    }
}
