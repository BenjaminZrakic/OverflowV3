using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Vector3 spawnLocation;
    public GameObject player;

    public List<GameObject> EnemyList = new List<GameObject>();
    public List<MonsterSpawner> SpawnerList = new List<MonsterSpawner>();

    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = player.transform.position;
    }

    public void ResetLevel(){
        for(int i=0; i<EnemyList.Count;i++){
            Destroy(EnemyList[i]);
        }
        
        for(int i=0;i<SpawnerList.Count;i++){
            SpawnerList[i].currentWave = 0;
            SpawnerList[i].SpawnWave();
        }
    }
}
