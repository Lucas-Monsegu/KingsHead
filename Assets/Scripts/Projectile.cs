using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    Transform child;
    Vector3 randrot;
	// Use this for initialization
	void Start () {
        child = transform.GetChild(0);
        randrot = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5),0);
	}

	// Update is called once per frame
	void Update () {
        Vector3 np = transform.forward * speed * Time.deltaTime;
        np.y = 0;
        transform.position += np;
        
        child.eulerAngles += randrot * Time.deltaTime * speed * 10;
	}
    public void ThrowTomato(Vector3 pos)
    {
        transform.LookAt(pos);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            col.GetComponent<PlayerHP>().SIGKILLPLAYER();
        }
    }

}
