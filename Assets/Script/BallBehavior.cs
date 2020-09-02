using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public GameObject PS;
    Rigidbody2D rigid;
    Vector2 dir;
    public float reflectSpeed = 10;
    public float damage = 5;
    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        dir = rigid.velocity.normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 inDirection = dir;
        Vector2 inNormal = collision.contacts[0].normal.normalized;
        Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);

        Debug.DrawLine(collision.transform.position, (Vector2)collision.transform.position + inNormal * 5, Color.green, 1);
        Debug.DrawLine(collision.transform.position, (Vector2)collision.transform.position + newVelocity * 5, Color.cyan, 1);

        rigid.velocity = newVelocity * reflectSpeed;
        //rigid.AddForce(newVelocity * reflectSpeed);

        Debug.DrawLine(transform.position, (Vector2)transform.position + rigid.velocity, Color.gray, 1);

        HitableObj.Hit_event_c(collision.gameObject, damage);

        GameObject effect=Instantiate(PS, transform.position, transform.rotation);
        effect.transform.rotation= Quaternion.FromToRotation(effect.transform.up, inNormal) * transform.rotation;
    }


}
