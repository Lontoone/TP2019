using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public float healAmount = 50;

    HitableObj player;
    private void Start()
    {
        player = gameObject.GetComponent<HitableObj>();


        player.Heal(healAmount);
        player.GetComponent<HPbarControl>().UpdateHPbar();


    }


    /*
    private void OnTriggerStay2D(Collider2D other)
    {
        HitableObj player = other.GetComponent<HitableObj>();
        if (player != null)
        {
            //player.gameObject.AddComponent<Heal>();
            player.Heal(healAmount);
            player.GetComponent<HPbarControl>().UpdateHPbar();
            Destroy(gameObject);
        }

    }*/
}
