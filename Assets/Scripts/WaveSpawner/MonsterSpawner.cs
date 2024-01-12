using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WaveContent{
        [SerializeField] GameObject[] monsterSpawn;
        
        public GameObject[] GetMonsterSpawnList(){
            return monsterSpawn;
        }
    }

    [SerializeField] WaveContent[] waves;
    public int currentWave;
    bool waveHasBeenSpawned = false;
    public float spawnRange = 10;
    public List<GameObject> currentMonsters;

    public GameObject SpawnAfterWavesObject;

    public bool bossMonsterSpawner = false;


    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMonsters.Count == 0 && waveHasBeenSpawned){
            currentWave++;
            waveHasBeenSpawned = false;
            if (currentWave < waves.Length)
                SpawnWave();
            else{
                if(bossMonsterSpawner){
                    bossMonsterSpawner = false;
                    print("Supposedly starting next phase");
                    GetComponentInParent<BossAttackLogic>().ChangePhase(true);
                    
                    
                }
                else if (SpawnAfterWavesObject != null)
                    SpawnAfterWavesObject.SetActive(true);
            }
        }
    }

    public void SpawnWave(){
        for(int i = 0; i < waves[currentWave].GetMonsterSpawnList().Length;i++){
            
            GameObject newSpawn = Instantiate(waves[currentWave].GetMonsterSpawnList()[i], FindSpawnLoc(), Quaternion.identity);
            currentMonsters.Add(newSpawn);

            Enemy monster = newSpawn.GetComponent<Enemy>();
            monster.isWaveSpawn = true;
            monster.monsterSpawner = this;
        }

        waveHasBeenSpawned = true;
    }

    Vector3 FindSpawnLoc(){
        Vector3 SpawnPosition;

        float xLoc = Random.Range(-spawnRange,spawnRange)+transform.position.x;
        float zLoc = Random.Range(-spawnRange,spawnRange)+transform.position.z;
        float yLoc = transform.position.y;

        SpawnPosition = new Vector3(xLoc,yLoc,zLoc);
        if(Physics.Raycast(SpawnPosition,Vector3.down,5)){
            return SpawnPosition;
        }
        else{
            return FindSpawnLoc();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRange);

    }
}
