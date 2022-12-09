using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectateur : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Animator a = GetComponentInChildren<Animator>();
        a.Play("Throw", 0, Random.Range(0f, 1f));
        a.speed = 1 + Random.Range(-0.1f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
