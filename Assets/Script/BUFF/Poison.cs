using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public float damageAmount = 5;
    public bool instance = true;
    public float dot = 10;
    void Start()
    {
        StartCoroutine(Damage_dot_coro());
    }

    // Update is called once per frame

    IEnumerator Damage_dot_coro()
    {
        int i = 0;
        while (i < dot)
        {
            i++;
            HitableObj.Hit_event_c(gameObject, damageAmount);
            yield return new WaitForSeconds(1);
        }
        Destroy(this);
    }
}
