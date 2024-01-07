using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AttackSO", menuName = "OVERFLOW/AttackSO", order = 0)]
public class AttackSO : ScriptableObject {
    public AnimatorOverrideController animatorOv;
    public float damage;
}

