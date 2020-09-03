using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovement pm;

    public GameObject ball;
    public float shootSpeed = 5;
    public AudioSource shootAudio;
    //Rigidbody2D rigid;
    private void Start()
    {
        pm = gameObject.GetComponent<PlayerMovement>();
        if (pm != null)
            pm.eShoot += Shoot;

    }
    private void OnDestroy()
    {
        if (pm != null)
            pm.eShoot -= Shoot;

    }
    void Shoot(Vector2 pos, Vector2 dir)
    {
        GameObject ball_i = Instantiate(ball, pos, Quaternion.identity);
        ball_i.GetComponent<Rigidbody2D>().velocity = dir * shootSpeed;
        shootAudio.Play();

    }
}
