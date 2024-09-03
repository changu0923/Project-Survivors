using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSkill03 : Skill
{
    [Header("스킬 설정")]
    [SerializeField] int damage;
    [SerializeField] int targetCount;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject scythePrefab;

    private ScytheSkillProjectile bullet;
    private Player player;
    LayerMask monsterLayer;
    private void Awake()
    {
        player = GameManager.Instance.Player;
        monsterLayer = LayerMask.GetMask("Monster");
    }

    public override void Use()
    {
        StartCoroutine(ShootCoroutine(targetCount));
    }

    private Transform FindRandomMob()
    {
        Vector2 currentPos = player.transform.position;
        Collider2D[] detectedMobs = Physics2D.OverlapCircleAll(currentPos, 10f, monsterLayer);
        Transform targetMob = null;

        if (detectedMobs.Length > 0)
        {
            int index = Random.Range(0, detectedMobs.Length);
            targetMob = detectedMobs[index].transform; 
        }

        return targetMob;
    }

    private void Shoot(Transform target)
    {
        if (target == null) return;

        bullet = ObjectPoolManager.Instance.Instantiate("ScytheSkillProjectile", scythePrefab).GetComponent<ScytheSkillProjectile>();
        bullet.transform.position = player.transform.position;
        bullet.Shoot(damage, bulletSpeed, target);
    }

    IEnumerator ShootCoroutine(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Shoot(FindRandomMob());
        }
        yield return null;
    }
}
