using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public GameObject sword;
    public float swordSpeed;
    bool swinging = false;
    public float damage;
    Vector3 startP;
    Vector3 endP = new Vector3(90, 0, 0);
    Movements mvts;
    // Use this for initialization
	void Start () {
        startP = sword.transform.eulerAngles;
        mvts = GetComponent<Movements>();
	}
    
    
}
