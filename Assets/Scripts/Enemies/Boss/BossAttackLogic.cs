using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackLogic : MonoBehaviour
{
    public Boss boss;

    public Collider bossCollider;

    public GameObject normalBossModel;
    public GameObject transparentBossModel;
    public GameObject swingySword;
    public GameObject waveSpawnerPhaseTwo;
    public GameObject waveSpawnerPhaseThree;
    public GameObject waveSpawnerPhaseFour;

    public GameObject spinnyBois;

    public bool phaseTwo = false;
    public bool phaseThree = false;
    public bool phaseFour = false;

    bool changingPhases = false;

    public void StopSpinning(){
        print("Stopping spinning");
        boss.spinning = false;
        print(boss.spinning);
    }


    public void ChangePhase(bool isAlive){
        if (!phaseTwo && !changingPhases){
            StartPhaseTwo();
        }

        else if(!phaseThree && phaseTwo && !changingPhases){
            StartPhaseThree();
        }

        else if(!phaseFour && phaseThree && !changingPhases){
            StartPhaseFour();
        }

        else if(phaseFour && !isAlive && !changingPhases){
            Destroy(this.gameObject);
        }

    }

    void StartPhaseTwo(){
        changingPhases = true;
        boss.isAttacking = false;
        boss.gameObject.SetActive(false);
        swingySword.SetActive(false);
        waveSpawnerPhaseTwo.SetActive(true);
        spinnyBois.SetActive(false);
        phaseTwo = true;
        changingPhases = false;
    }

    public void StartPhaseThree(){
        changingPhases = true;
        phaseThree = true;
        print("Starting phase three");
        waveSpawnerPhaseTwo.SetActive(false);
        waveSpawnerPhaseThree.SetActive(true);
        bossCollider.enabled = false;
        boss.gameObject.GetComponent<Animator>().Play("Chase");
        boss.gameObject.transform.position = boss.spawnPoint;
        transparentBossModel.SetActive(true);
        normalBossModel.SetActive(false);
        boss.gameObject.SetActive(true);
        
        
        
        
        HealthSystemForDummies bossHealth = boss.gameObject.GetComponent<HealthSystemForDummies>();
        bossHealth.ReviveWithMaximumHealth();
        changingPhases = false;
        
    }

    public void StartPhaseFour(){
        changingPhases = true;
        print("Starting phase four");
        waveSpawnerPhaseThree.SetActive(false);
        waveSpawnerPhaseFour.SetActive(true);
        transparentBossModel.SetActive(false);
        normalBossModel.SetActive(true);
        boss.gameObject.transform.position = boss.spawnPoint;
        bossCollider.enabled = true;

        
        
        phaseFour = true;
        changingPhases = false;
    }
}
