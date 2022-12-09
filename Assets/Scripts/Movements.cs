using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour {
    public CharacterController cc;
    public float speed = 2;

    public float rotateSpeed = 5f;
    Vector3 directionDeplacement = Vector3.zero;
    Vector3 last_dir = Vector3.zero;

    float time_move_anim = float.MaxValue;
    bool on_movement = false;
    Animator animator;
    public bool canAttack = true;
    float damage = 10;
    LayerMask lm;

    ParticleSystem poussiere;

    public MainMenu MM;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.CrossFadeInFixedTime("Idle", 0.2f);
        lm = LayerMask.GetMask("Hitbox","guard");
        poussiere = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

	void Update () {
        if (MainMenu.currentState == MainMenu.STATE.MM || MainMenu.currentState == MainMenu.STATE.ST)
        {
            animator.SetBool("moving", false);
            return;
        }
        Vector3 movement = Vector3.zero;

		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q))
        {
            movement += (Vector3.left);
        }
        if(Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.back;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && time_move_anim > 10f)
        {
            StartCoroutine(delayRay());
            animator.SetTrigger("Attack");
            time_move_anim = 0.4f;
        }

        if (!on_movement && movement != Vector3.zero)
        {
            on_movement = true;
            poussiere.Play();
            //animator.CrossFadeInFixedTime("Running", 0.2f);
            animator.SetBool("moving", true);
        }
        else if (on_movement && movement == Vector3.zero)
        {
            on_movement = false;
            poussiere.Stop();
            animator.SetBool("moving", false);
            //animator.CrossFadeInFixedTime("Idle", 0.2f);
        }

        if (movement != Vector3.zero && directionDeplacement != movement)
        {
            directionDeplacement = movement;
            StopCoroutine("Turn");
            StartCoroutine("Turn");
        }

        time_move_anim -= Time.deltaTime;
        if (time_move_anim < 0)
        {
            time_move_anim = float.MaxValue;
            //animator.CrossFadeInFixedTime(on_movement ? "Running" : "Idle", 0.2f);
        }
        movement.Normalize();

        movement = movement * Time.deltaTime * speed;
        if (time_move_anim < 10)
            movement *= 0.7f; //Ralenti si en train d'attaquer ou prendre des degats
        cc.Move(movement);
        transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);
	}
    IEnumerator delayRay()
    {
        yield return new WaitForSeconds(0.1f);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 3, lm.value))
        {
            print(hit.transform.name);
            if (hit.transform.tag == "king")
            {
                King.GetHit(damage);
            }
            else if(hit.transform.tag == "guard" )
            {
                hit.transform.GetComponent<Guard>().GetHit(damage);
            }
            else if(hit.transform.name == "sword" )
            {
                hit.transform.root.GetComponent<Guard>().GetHit(damage);
            }
        }
    }
    IEnumerator Turn()
    {
        float time = 0;
        while (time * rotateSpeed < 1)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                                    Quaternion.Euler(0,
                                        (directionDeplacement.x < 0 ? -1 : 1) * Vector3.Angle(Vector3.forward, directionDeplacement),
                                        0),
                                    time * rotateSpeed);
            yield return null;
        }
    }
}
