using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnQueue
{
    [Header("스폰할 몬스터")]
    [SerializeField] GameObject monsterPrefab;
    [Header("스폰 시작 시간")]
    [SerializeField] int startTime;
    [Header("스폰 주기")]
    [Tooltip("몇 초마다 스폰시킬것인지 지정합니다.")]
    [SerializeField] float spawnRate;
    [Header("스폰 마리 수")]
    [SerializeField] int spawnMobCount;


    public GameObject MonsterPrefab { get => monsterPrefab; set => monsterPrefab = value; }
    public int StartTime { get => startTime; set => startTime = value; }
    public int SpawnMobCount { get => spawnMobCount; set => spawnMobCount = value; }
    public float SpawnRate { get => spawnRate; set => spawnRate = value; }
}
