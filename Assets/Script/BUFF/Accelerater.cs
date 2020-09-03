using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerater : MonoBehaviour
{
    public float time = 5;
    float originGapTime;
    PlayerMovement player;
    Coroutine acc_c;
    void Start()
    {
        player = gameObject.GetComponent<PlayerMovement>();
        originGapTime = player.walkGapTime;

        acc_c = StartCoroutine(Acc_coro());
    }

    private void OnDestroy()
    {
        player.walkGapTime = originGapTime;
    }


    IEnumerator Acc_coro()
    {
        player.walkGapTime = 0.1f;
        yield return new WaitForSeconds(time);
        player.walkGapTime = 0.2f;
        Destroy(this);
    }
}
