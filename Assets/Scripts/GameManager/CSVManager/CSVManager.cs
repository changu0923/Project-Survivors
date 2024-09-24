using Mono.CompilerServices.SymbolWriter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVManager : MonoBehaviour
{
    [SerializeField] string dataMonsterPath;

    private Dictionary<string, MonsterData> monsterDataDict = new Dictionary<string, MonsterData>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
       ReadMonsterData();
    }

    private void ReadMonsterData()
    {
        StreamReader reader = new StreamReader(Application.dataPath + "/" + dataMonsterPath);
        bool checkEOF = false;

        reader.ReadLine();

        while (!checkEOF) 
        {
            string line = reader.ReadLine();

            if(line == null)
            {
                checkEOF = true;
                break;  
            }

            MonsterData data = new MonsterData();
            var splitData = line.Split(',');
            string keyValue = splitData[0];
            data.monsterName = keyValue;
            data.maxHP = int.Parse(splitData[1]);
            data.damage = int.Parse(splitData[2]);
            data.moveSpeed = int.Parse(splitData[3]);

            monsterDataDict.Add(keyValue, data);
        }
    }    

    public MonsterData GetMonsterData(string monsterName)
    {
        return monsterDataDict[monsterName];
    }
}