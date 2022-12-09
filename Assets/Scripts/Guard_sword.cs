using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard_sword : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            
            col.GetComponent<PlayerHP>().SIGKILLPLAYER();
        }
    }
}
