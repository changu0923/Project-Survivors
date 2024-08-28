using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[Serializable]
public class SpawnQueue
{
    [Header("스폰할 몬스터")]
    [SerializeField] GameObject monsterPrefab;
    [Header("스폰 시작 시간")]
    [SerializeField] int startTime; 
    [Header("스폰 중지 시간")]
    [SerializeField] int endTime;
    [Header("스폰 주기")]
    [Tooltip("몇 초마다 스폰시킬것인지 지정합니다.")]
    [SerializeField] float spawnRate;
    [Header("스폰 마리 수")]
    [SerializeField] int spawnMobCount;

    private Coroutine spawnCoroutine;

    public GameObject MonsterPrefab { get => monsterPrefab; set => monsterPrefab = value; }
    public int StartTime { get => startTime; set => startTime = value; }
    public int SpawnMobCount { get => spawnMobCount; set => spawnMobCount = value; }
    public float SpawnRate { get => spawnRate; set => spawnRate = value; }
    public int EndTime { get => endTime; set => endTime = value; }
        
    private void SpawnMonster()
    {
        GameObject spawnedMob;
        Player player = GameManager.Instance.Player;
        Monster monster = monsterPrefab.GetComponent<Monster>();
        string spawnTarget = monster.MonsterName;
        spawnedMob = ObjectPoolManager.Instance.Instantiate(spawnTarget, monster.gameObject);
        spawnedMob.transform.parent = ObjectPoolManager.Instance.transform;

        Vector2 getRandomPos = UnityEngine.Random.insideUnitCircle.normalized * 10f;
        Vector2 spawnPos = new Vector2(player.transform.position.x + getRandomPos.x, player.transform.position.y + getRandomPos.y);
        spawnedMob.transform.position = spawnPos;
    }

    public IEnumerator SpawnMobCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < spawnMobCount; i++)
            {
                SpawnMonster();
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
