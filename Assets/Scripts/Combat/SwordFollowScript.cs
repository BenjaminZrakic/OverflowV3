using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFollowScript : MonoBehaviour
{

    public GameObject following;
    [Range(0.0f, 1.0f)]
    public float interested; //how interested the follower is in the thing it's following :)

    void LateUpdate () {
        transform.position = Vector3.MoveTowards(transform.position, following.transform.position, interested);
        //transform.rotation = Quaternion.Euler(0, -following.transform.rotation.y, 0);
    }


}
