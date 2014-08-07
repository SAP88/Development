using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RespawnScript : MonoBehaviour 
{
    private class RespawnNode
    {
        public bool IsWaveSplitter { get; set; }
        public float NextTimeout { get; set; }
        public EnemyType EnemyType { get; set; }
    }
    
    public float BeginAfter; // Через скока монстры должны пойти
    private float BeginAt; // Во скока началось
    bool IsGameBegan = false;
    private bool IsInited { get; set; }

    //Стек врагов
    private Stack<RespawnNode> RespawnQueue;

    private float LastEnemyCreationTime;
    public EnemyWave[] Waves { get; set; }
    private RespawnNode CurrentNode = null;

	// Use this for initialization
	void Start () {
        if (Waves == null || Waves.Length == 0)
        {
            return;
        }

        BeginAt = Time.realtimeSinceStartup;

        RespawnQueue = new Stack<RespawnNode>();

        for (int w = Waves.Length - 1; w >= 0 ; w -- )
        {
            for(int eg = Waves[w].GroupWave.Length - 1; eg >= 0 ; eg --)
            {
                for (int i = 0; i < Waves[w].GroupWave[eg].Count; i++)
                {
                    RespawnQueue.Push(new RespawnNode
                    {
                        NextTimeout = Waves[w].GroupWave[eg].CreationTimeout,
                        EnemyType = Waves[w].GroupWave[eg].EnemyType
                    });
                }
            }

            RespawnQueue.Push(new RespawnNode 
            { 
                NextTimeout = Waves[w].NextWaveTimeout, 
                IsWaveSplitter = true 
            });
        }

        CurrentNode = RespawnQueue.Pop();
        IsInited = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!IsInited || CurrentNode == null) return;

	    if(!IsGameBegan)
        {
            IsGameBegan = Time.realtimeSinceStartup - BeginAt > BeginAfter;
            if(IsGameBegan)
            {
                LastEnemyCreationTime = Time.realtimeSinceStartup;
            }
            return;
        }

        
        if (Time.realtimeSinceStartup - LastEnemyCreationTime > CurrentNode.NextTimeout)
        {
            if (!CurrentNode.IsWaveSplitter)
            {
                CreateEnemy();    
            }

            if (RespawnQueue.Count == 0)
            {
                CurrentNode = null;
                return;
            }
            CurrentNode = RespawnQueue.Pop();
            LastEnemyCreationTime = Time.realtimeSinceStartup;
        }

	}

    public void CreateEnemy()
    {
        //var path = "Assets/Prefabs/" + CurrentNode.EnemyType.ToString();
        //var enemyTest = (GameObject)Resources.Load(path);
        var enemy = Resources.Load<GameObject>(CurrentNode.EnemyType.ToString());
        if(enemy != null)
        {
            GameObject.Instantiate(enemy, this.transform.position, Quaternion.identity);
            //enemy.GetComponent<MonsterAI_Movement>().Star
        }
        else
        {
            Debug.LogError(string.Format("\"{0}\" Not found", CurrentNode.EnemyType));
        }
    }

    public Field[] GameField { get; set; }

    
}
