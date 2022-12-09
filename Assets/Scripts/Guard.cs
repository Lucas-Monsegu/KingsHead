using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {

    public float speed;
    GameObject follow;
    public CharacterController cc;
    Animator animator;
    public float hp = 10;
    public GameObject pcg;
    // Use this for initialization
    void Start ()
    {
        follow = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (MainMenu.currentState == MainMenu.STATE.PA || MainMenu.currentState == MainMenu.STATE.MM || MainMenu.currentState == MainMenu.STATE.ST)
        {
            animator.SetTrigger("GoIddle");
            return;
        }
        transform.LookAt(follow.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if(follow.activeInHierarchy == false)
        {
            animator.SetTrigger("GoIddle");
        }
        else if (Vector3.Distance(transform.position, follow.transform.position) > 3)
        {
            cc.Move(transform.TransformVector(Vector3.forward) * speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 0.4f, transform.position.z);
        }
        else
        {
            animator.SetTrigger("Attack");
        }
    }
    public void GetHit(float damage)
    {
        hp -= 10;
        if (hp <= 0)
        {
            KillGuard();
        }
    }
    public void KillGuard()
    {
        ParticleSystem pc = Instantiate(pcg, transform.position + Vector3.up, Quaternion.identity).GetComponent<ParticleSystem>();
        pc.Emit(30);
        pc.collision.SetPlane(0, Ground.instance.transform);
        Destroy(pc.gameObject, 5);
        transform.gameObject.SetActive(false);
        Destroy(gameObject, 6);
    }
}
