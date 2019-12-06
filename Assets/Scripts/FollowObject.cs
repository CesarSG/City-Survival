using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{

    public GameObject FollowPlayer;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - FollowPlayer.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = FollowPlayer.transform.position + offset;
    }
}