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


    public void ChangePhase(bool isAlive){
        if (!boss.phaseTwo){
            boss.isAttacking = false;
            boss.gameObject.SetActive(false);
            waveSpawner.SetActive(true);
            Debug.Log(waveSpawner.activeSelf);
            Debug.Log(boss.gameObject.activeSelf);

            boss.phaseTwo = true;
        }

        else if(boss.phaseThree && !isAlive){
            Destroy(boss.gameObject);
        }

    }

    public void StartPhaseThree(){
        print("Starting phase three");
        waveSpawner.SetActive(false);
        boss.gameObject.GetComponent<Animator>().Play("Chase");
        boss.gameObject.transform.position = boss.spawnPoint;
        boss.gameObject.SetActive(true);


        Debug.Log(waveSpawner.activeSelf);
        Debug.Log(boss.gameObject.activeSelf);
        
        
        HealthSystemForDummies bossHealth = boss.gameObject.GetComponent<HealthSystemForDummies>();
        bossHealth.ReviveWithMaximumHealth();

        boss.phaseThree = true;
    }
}
