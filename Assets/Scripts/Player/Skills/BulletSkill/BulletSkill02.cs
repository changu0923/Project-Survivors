using System.Collections;
using UnityEngine;

public class BulletSkill02 : Skill
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
        monsterLayer = LayerMask.NameToLayer("Monster");
    }

    public override void Use()
    {
        MultiShotCoroutine(2, 0.05f, FindMinDistanceMob());
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
                targetMob = mob.transform;
            }
        }

        return targetMob;
    }
    
    private void Shoot(Transform target)
    {
        bullet = ObjectPoolManager.Instance.Instantiate("BulletSkillProjectile", bulletPrefab).GetComponent<BulletSkillProjectile>();
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
