using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelProjectile : MonoBehaviour
{
    [SerializeField] Skill shovelSkill;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.transform.GetComponent<Monster>();
            monster.TakeDamage(shovelSkill.SkillDamage);
            Debug.Log($"[{monster.name}] TakeDamage({shovelSkill.SkillDamage})");
        }
    }
}
