using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour {
    public ParticleSystem pc;
    float maxHP;
    public float HP;
    static King instance;
    Rigidbody crown;
    Rigidbody head;
    public float forcecrown;
    public float forcehead;
    public MainMenu MM;

    public Arm_King[] arms;
    void Start()
    {
        maxHP = HP;
        instance = this;
        Rigidbody[] rb = transform.parent.parent.GetComponentsInChildren<Rigidbody>();
        head = rb[0];
        crown = rb[1];
    }

    void pop_crown()
    {
        if (crown.isKinematic)
        {
            crown.isKinematic = false;
            crown.AddForce(Vector3.left * forcecrown, ForceMode.VelocityChange);
        }
    }

    void pop_head()
    {
        if (head.isKinematic)
        {
            head.isKinematic = false;
            head.AddForce(Vector3.left * forcehead, ForceMode.VelocityChange);
            MM.SetVictoryPanel();
        }
    }

    public static void GetHit(float damage)
    {
        instance.getHit(damage);
        foreach (var arm in instance.arms)
            arm.StartCoroutine("move");
    }
    void getHit(float damage)
    {
        pc.Emit(5);
        HP -= damage;
        if (HP <= maxHP / 2)
            pop_crown();
        if (HP <= 0)
            pop_head();
        
    }
}
