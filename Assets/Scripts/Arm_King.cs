using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_King : MonoBehaviour {

    public IEnumerator move()
    {
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * 5;
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, -(i * 200) * (i - 1));
            yield return null;
        }
    }
}
