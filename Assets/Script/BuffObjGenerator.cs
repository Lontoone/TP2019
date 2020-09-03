using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObjGenerator : MonoBehaviour
{
    public Transform max, min;

    public float generateGapTime = 3;
    Coroutine cWait;
    public GameObject[] buffs;
    private void FixedUpdate()
    {
        if (cWait == null)
        {
            cWait = StartCoroutine(Generate());
        }
    }
    IEnumerator Generate()
    {
        Vector2 ranPos = new Vector2(Random.Range(min.position.x, max.position.x),
                                    Random.Range(min.position.y, max.position.y));

        int ranBuff = Random.Range(0, buffs.Length);
        GameObject b = Instantiate(buffs[ranBuff], ranPos, Quaternion.identity);
        yield return new WaitForSeconds(generateGapTime);
        cWait = null;
    }

}
