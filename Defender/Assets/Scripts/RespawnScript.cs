using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class MonsterRoundInfo
{
    public GameObject Monster;
    public int Round;
    public int Count;
}

public class RespawnScript : MonoBehaviour 
{
    public GameObject[] RespawnNodes;
    public MonsterRoundInfo[] Monsters;
    public float BeginAfter;
    public float WavesTimeOut = 10;
    public float RandomTimeMonsterCreation = 2.5f;
    
    int CurrentWave = 0;
    bool IsInited;
    bool IsGameBegan;
    float BeginAt;
    float NextEnemyCreationTime;
    int MaxWaves = 0;
    //int LastRespawnNode = 0;
    // Use this for initialization
    void Start()
    {
        if (Monsters == null || Monsters.Length == 0)
        {
            return;
        }

        MaxWaves = Monsters[0].Round;
        foreach(var m in Monsters)
        {
            if(m.Round > MaxWaves)
            {
                MaxWaves = m.Round;
            }
        }

        BeginAt = Time.realtimeSinceStartup;

        IsInited = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInited) return;
        if (CurrentWave > MaxWaves)
        {
            Debug.Log("Уже волны прошли");
            IsInited = false;
            return;
        }

        if (!IsGameBegan)
        {
            IsGameBegan = Time.realtimeSinceStartup - BeginAt > BeginAfter;
            if (IsGameBegan)
            {
                NextEnemyCreationTime = Time.realtimeSinceStartup;
            }
            return;
        }

        if(Time.realtimeSinceStartup > NextEnemyCreationTime)
        {
            //Ищем монстра
            GameObject monter = null;
            for(int i = 0; i < Monsters.Length; i++)
            {
                if(Monsters[i].Round == CurrentWave && Monsters[i].Count != 0)
                {
                    monter = Monsters[i].Monster;
                    Monsters[i].Count --;
                    break;
                }
            }

            if(monter == null)
            {
                NextEnemyCreationTime = Time.realtimeSinceStartup + WavesTimeOut;
                CurrentWave++;
                return;
            }
            else
            {
                var r = new System.Random();
                NextEnemyCreationTime = Time.realtimeSinceStartup + r.Next(0, (int)(RandomTimeMonsterCreation * 1000)) / 1000;
                GameObject newMonster = (GameObject)GameObject.Instantiate(monter);

                var selectedLine = r.Next(0, RespawnNodes.Length);
                newMonster.transform.position = RespawnNodes[selectedLine].transform.position;
            }
        }
        
    }

}
