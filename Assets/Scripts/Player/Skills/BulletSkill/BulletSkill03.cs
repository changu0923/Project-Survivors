using System.Collections;
using UnityEngine;

public class BulletSkill03 : Skill
{
    [Header("스킬 설정")]
    [SerializeField] int damage;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject bulletPrefab;

    private BulletSkillProjectile bullet;
    private Player player;
    LayerMask monsterLayer;
    private void Awake()
    {
        player = GameManager.Instance.Player;
        monsterLayer = LayerMask.GetMask("Monster");
    }

    public override void Use()
    {
        StartCoroutine(MultiShotCoroutine(3, 0.05f, FindMinDistanceMob()));
    }

    private Transform FindMinDistanceMob()
    {
        Vector2 currentPos = player.transform.position;
        Collider2D[] detectedMobs = Physics2D.OverlapCircleAll(currentPos, 10f, monsterLayer);
        Transform targetMob = null;

        float minDistance = Mathf.Infinity;

        foreach (Collider2D mob in detectedMobs)
        {
            float currentMobDistance = Vector2.Distance(currentPos, mob.transform.position);

            if (currentMobDistance < minDistance)
            {
                minDistance = currentMobDistance;
                targetMob = mob.transform;
            }
        }

        return targetMob;
    }

    private void Shoot(Transform target)
    {
        if (target == null) return;

        bullet = ObjectPoolManager.Instance.Instantiate("BulletSkillProjectile", bulletPrefab).GetComponent<BulletSkillProjectile>();
        bullet.transform.position = player.transform.position;
        bullet.Shoot(damage, bulletSpeed, target);
    }

    IEnumerator MultiShotCoroutine(int rounds, float t_delay, Transform target)
    {
        for (int i = 0; i < rounds; i++)
        {
            Shoot(target);
            yield return new WaitForSeconds(t_delay);
        }
    }
}
