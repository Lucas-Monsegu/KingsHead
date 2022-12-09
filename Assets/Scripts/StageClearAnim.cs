using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearAnim : MonoBehaviour {

    public RectTransform t1;
    public RectTransform t2;
    public RectTransform crown;
    public GameObject stagePan;

    public void PlayAnim()
    {
        stagePan.SetActive(true);
        StartCoroutine("lol");
    }

	IEnumerator lol()
    {
        
        float i = 0;
        float j = -0.6f;
        float p = -1.2f;
        Vector3 startp = Vector3.zero;
        Vector3 endp = Vector3.one;
        while(p < 1)
        {
            i += Time.deltaTime * 3;
            j += Time.deltaTime * 3;
            p += Time.deltaTime * 3;
            float a = Mathf.Lerp(0, 1, i);
            t1.localScale = new Vector3(a, a, a);
            float b = Mathf.Lerp(0, 1, j);
            crown.localScale = new Vector3(b, b, b);
            float c = Mathf.Lerp(0, 1, p);
            t2.localScale = new Vector3(c,c,c);
            yield return null;
        }
    }
}
