using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnQueue
{
    [Header("������ ����")]
    [SerializeField] GameObject monsterPrefab;
    [Header("���� ���� �ð�")]
    [SerializeField] int startTime;
    [Header("���� �ֱ�")]
    [Tooltip("�� �ʸ��� ������ų������ �����մϴ�.")]
    [SerializeField] float spawnRate;
    [Header("���� ���� ��")]
    [SerializeField] int spawnMobCount;


    public GameObject MonsterPrefab { get => monsterPrefab; set => monsterPrefab = value; }
    public int StartTime { get => startTime; set => startTime = value; }
    public int SpawnMobCount { get => spawnMobCount; set => spawnMobCount = value; }
    public float SpawnRate { get => spawnRate; set => spawnRate = value; }
}
