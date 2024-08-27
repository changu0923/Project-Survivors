using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]List<Monster> spawnList = new List<Monster>();

    [Header("스폰 큐 목록")]
    [SerializeField] private List<SpawnQueue> spawnQueueList = new List<SpawnQueue>();

    Player player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    private void Start()
    {
        //this is testcode
        StartCoroutine(MobSpawner(3f));
    }

    public void SpawnMonster(Monster monster)
    {
        GameObject spawnedMob;
        string spawnTarget = monster.MonsterName;
        spawnedMob = ObjectPoolManager.Instance.Instantiate(spawnTarget, monster.gameObject);
        spawnedMob.transform.parent = ObjectPoolManager.Instance.transform;

        Vector2 getRandomPos = Random.insideUnitCircle.normalized * 10f;
        Vector2 spawnPos = new Vector2(player.transform.position.x + getRandomPos.x, player.transform.position.y + getRandomPos.y);
        spawnedMob.transform.position = spawnPos;
    }

    IEnumerator MobSpawner(float t_spawn)
    {
        while (true)
        {
            yield return new WaitForSeconds(t_spawn);
            SpawnMonster(spawnList[0]);
        }
    }
}
