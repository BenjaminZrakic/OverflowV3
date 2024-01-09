using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackLogic : MonoBehaviour
{
    public Boss boss;
    public GameObject waveSpawner;

    public void StopSpinning(){
        print("Stopping spinning");
        boss.spinning = false;
        print(boss.spinning);
    }


    public void StartPhaseTwo(){
        boss.gameObject.SetActive(false);
        waveSpawner.SetActive(true);
    }

    public void StartPhaseThree(){
        print("Starting phase three");
        boss.gameObject.transform.position = boss.spawnPoint;
        waveSpawner.SetActive(false);
        boss.gameObject.SetActive(true);
        Debug.Log(waveSpawner.activeSelf);
        Debug.Log(boss.gameObject.activeSelf);
        
        
        HealthSystemForDummies bossHealth = boss.gameObject.GetComponent<HealthSystemForDummies>();
        bossHealth.AddToCurrentHealth(bossHealth.MaximumHealth);
    }
}
