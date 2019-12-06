using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public GameObject FollowObject;
    public float speed;
    private float waitTime;
    private float startTime;

    public void Start()
    {
        speed = Random.value * 2 + 2;
        FollowObject = GameObject.Find("Player");
        waitTime = 3f + Time.time;
        
    }
	
	// Update is called once per frame
	void Update () {

        if(Time.time > waitTime)
        {
            transform.LookAt(FollowObject.transform);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        
	}
}
