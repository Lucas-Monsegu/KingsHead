using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    public GameObject player;
    public GameObject king;
    public GameObject kingHead;
    public GameObject kingCrown;
    public MainMenu MM;
    public ParticleSystem blood;

    void ResetPlayer()
    {
        player.transform.position = new Vector3(0, 0.7f, -7);
        player.transform.eulerAngles = Vector3.zero;
        player.SetActive(true);
    }
    void ResetKing()
    {
        king.GetComponent<King>().HP = 100;
        kingHead.transform.localPosition = new Vector3(-1.47f, 0.62f,0);
        kingHead.transform.eulerAngles = Vector3.zero;
        kingHead.GetComponent<Rigidbody>().isKinematic = true;
        kingCrown.GetComponent<Rigidbody>().isKinematic = true;
        kingCrown.transform.eulerAngles = Vector3.zero;
        kingCrown.transform.localPosition = new Vector3(-2.51f, 0.59f, 0);
    }
    public void ResetEverything()
    {
        ResetKing();
        ResetPlayer();
        blood.Clear();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}