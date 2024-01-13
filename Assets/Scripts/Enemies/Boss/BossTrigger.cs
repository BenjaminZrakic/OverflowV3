using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject BossDoor;
    public GameObject BossHealthBar;

    private void Start() {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

    }
    private void OnTriggerEnter(Collider other) {
        if(!levelManager.inBossFight)
        {
            GameObject.FindWithTag("BossHealthBar").GetComponent<Canvas>().enabled = true;
            levelManager.inBossFight = true;
            BossDoor.GetComponent<Animator>().SetTrigger("close_door"); 
        }
 
    }

}
