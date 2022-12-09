using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {
    public MainMenu MM;
    public GameObject pcg;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SIGKILLPLAYER()
    {
        ParticleSystem pc = Instantiate(pcg, transform.position + Vector3.up * 1f, Quaternion.identity).GetComponent<ParticleSystem>();
        pc.Emit(30);
        pc.collision.SetPlane(0,Ground.instance.transform);
        Destroy(pc.gameObject, 5);
        MM.WaitToChange(2);
        gameObject.SetActive(false);
    }

}
