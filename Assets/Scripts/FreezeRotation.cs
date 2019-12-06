using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour {


    private Quaternion StratRotation;

	// Use this for initialization
	void Start () {
        StratRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = StratRotation;

    }
}
