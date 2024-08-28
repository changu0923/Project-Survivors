using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("스폰 큐 목록")]
    [SerializeField] private List<SpawnQueue> spawnQueueList = new List<SpawnQueue>();

    private Dictionary<int, SpawnQueue> startSpawnDict = new Dictionary<int, SpawnQueue>();
    private Dictionary<int, SpawnQueue> endSpawnDict = new Dictionary<int, SpawnQueue>();   
    private Dictionary<int, Coroutine> activeSpawnerDict = new Dictionary<int, Coroutine>();    

    private int roundEndTime = 0;
    private int currentTime = 0;

    private void Awake()
    {
        GameManager.Instance.OnRoundTimeChanged += CheckRoundTime;
        roundEndTime = GameManager.Instance.RoundEndTime;
        foreach (var queue in spawnQueueList)
        {
            startSpawnDict.Add(queue.StartTime , queue);
            if (queue.EndTime != 0)
            {
                endSpawnDict.Add(queue.EndTime, queue);
            }
        }
    }

    private void CheckRoundTime()
    {
        currentTime = GameManager.Instance.RoundTime;
        foreach (var queue in startSpawnDict)
        {
            if(queue.Key == currentTime)
            {
                Coroutine spawnerCoroutine = StartCoroutine(queue.Value.SpawnMobCoroutine());
                activeSpawnerDict.Add(queue.Key, spawnerCoroutine);
            }
        }
        int tempQueue = -1;
        foreach (var queue in endSpawnDict)
        {
            if(queue.Key == currentTime)
            {
                if(activeSpawnerDict.TryGetValue(queue.Value.StartTime, out Coroutine targetCoroutine))
                {
                    StopCoroutine(targetCoroutine);
                    activeSpawnerDict.Remove(queue.Value.StartTime);
                    tempQueue = queue.Key;
                }
            }
        }
        if(tempQueue != -1)
            endSpawnDict.Remove(tempQueue);
    }

    IEnumerator CheckSpawnQueue()
    {
        CheckRoundTime();
        yield return new WaitForSeconds(1);
    }
}
