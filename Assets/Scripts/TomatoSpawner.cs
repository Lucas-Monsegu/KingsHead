using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSpawner : MonoBehaviour {
    public Vector3 center;
    public float disty;
    public float distx;
    public float delay;
    public GameObject player;
    // Use this for initialization
    public GameObject[] tab;

    public GameObject guard;


    List<GameObject> guardlist = new List<GameObject>();
    Vector3 GetRPos()
    {
        float r = Random.Range(0, 2 * Mathf.PI);
        return new Vector3(center.x + (Mathf.Cos(r) * distx), 2, center.z + Mathf.Sin(r) * disty);
    }
    public void SpawnGuard(float pos)
    {
        GameObject k = Instantiate(guard, new Vector3(pos, 0, -24), Quaternion.identity);
        guardlist.Add(k);
    }
    public void KillGuards()
    {
        foreach(GameObject k in guardlist)
        {
            Destroy(k);
        }
    }
	public IEnumerator spawner()
    {
        int nb_tomates = 0;
        yield return new WaitForSeconds(0.5f);
        while(true)
        {
            
            int i = Random.Range(0, tab.Length);
            GameObject k = Instantiate(tab[i], GetRPos(), Quaternion.identity);
            k.GetComponent<Projectile>().ThrowTomato(player.transform.position);
            Destroy(k, 5);
            if (MainMenu.currentStage >= 3)
            {
                yield return new WaitForSeconds(Random.Range(0f, 0.2f));
                int i2 = Random.Range(0, tab.Length);
                GameObject k2 = Instantiate(tab[i2], GetRPos(), Quaternion.identity);
                k2.GetComponent<Projectile>().ThrowTomato(player.transform.position);
                Destroy(k2, 5);
            }
            if (MainMenu.currentStage >= 7)
            {
                yield return new WaitForSeconds(Random.Range(0f, 0.2f));
                int i2 = Random.Range(0, tab.Length);
                GameObject k2 = Instantiate(tab[i2], GetRPos(), Quaternion.identity);
                k2.GetComponent<Projectile>().ThrowTomato(player.transform.position);
                Destroy(k2, 5);
            }
            if (MainMenu.currentStage >= 1 && nb_tomates % 10 == 0)
                SpawnGuard(-10);
            if (MainMenu.currentStage >= 5 && nb_tomates % 10 == 0)
                SpawnGuard(0);
            if (MainMenu.currentStage >= 9 && nb_tomates % 10 == 0)
                SpawnGuard(10);
            nb_tomates++;
            yield return new WaitForSeconds((delay + Random.Range(0f, 0.2f)) -
                                            ((MainMenu.currentStage + 1) * 0.1f));
        }
    }

}
