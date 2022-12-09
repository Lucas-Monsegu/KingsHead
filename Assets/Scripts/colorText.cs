using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorText : MonoBehaviour {
    Material col;
    Color startCol;
    public float speed;
    public Color endCol;
	// Use this for initialization
	void Start () {
        startCol = GetComponent<MeshRenderer>().material.color;
        StartCoroutine(ColChange());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ColChange()
    {
        float i = 0;
        Renderer mr = transform.GetComponent<Renderer>();
        Vector3 endp = transform.localScale;

        transform.localScale = Vector3.zero;
        while( i < 1 )
        {
            i += Time.deltaTime * speed ;
            transform.localScale = Vector3.Lerp(transform.localScale, endp, i);
            mr.material.color = Color.Lerp(mr.material.color, endCol, 1f - Mathf.Cos(i * Mathf.PI * 0.5f));
            
            yield return null;
        }
    }
}
