using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    #region Singleton
    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ObjectPoolManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("ObjectPoolManager");
                    instance = obj.AddComponent<ObjectPoolManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    private Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();

    public void CreatePool(string poolName, GameObject prefab, int initSize)
    {
        if (poolDict.ContainsKey(poolName) == false)
        {
            Queue<GameObject> newPool = new Queue<GameObject>();
            for (int i = 0; i < initSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                newPool.Enqueue(obj);
            }
            poolDict[poolName] = newPool;
        }
    }

    public GameObject Instantiate(string poolName)
    {
        if(poolDict.ContainsKey(poolName))
        {
            Queue<GameObject> pool = poolDict[poolName];
            if(pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                Debug.LogError($"{poolName} ĳ�۽�Ƽ ����. �� �ø�����.");
                return null;
            }
        }
        else
        {
            Debug.LogError($"������Ʈ Ǯ�� {poolName}�� �ش��ϴ� ť�� �����ϴ�.");
            return null;
        }
    }
}
