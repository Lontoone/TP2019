using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    //玩家AB
    public PlayerType playerType;
    public float speed = 5;
    public event Action<Vector2, Vector2> eShoot; //射出(目前位置,方向)
    Rigidbody2D rigid;
    Vector2 velocity = new Vector2();
    public GameObject ball;
    float powerTime = 0;
    public float poewerStoreSpeed = 5;
    [Range(0.01f, 1)]
    public float ringSize = 0.2f;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //velocity = rigid.velocity;

        //WASD按鍵移動
        if (playerType == PlayerType.A)
        {
            velocity = new Vector2(Input.GetAxis("AHorizontal"), Input.GetAxis("AVertical")) * speed;
            //續力
            if (Input.GetKey(KeyCode.Space))
            {
                powerTime += Time.deltaTime * poewerStoreSpeed;
                ball.transform.localPosition = new Vector2(Mathf.Cos(powerTime) * ringSize, Mathf.Sin(powerTime) * ringSize);
            }
            else
            {
                powerTime = Mathf.Clamp(powerTime -= Time.deltaTime, 0, 1);
            }
            //放手
            if (Input.GetKeyUp(KeyCode.Space))
            {

                Vector2 dir = ball.transform.position - gameObject.transform.position;
                Debug.DrawLine(ball.transform.position, dir * 10, Color.red, 1);
                if (eShoot != null)
                {
                    eShoot(ball.transform.position, dir.normalized);
                }
            }
        }

        //上下左右移動
        if (playerType == PlayerType.B)
        {
            velocity = new Vector2(Input.GetAxis("BHorizontal"), Input.GetAxis("BVertical")) * speed;
            //續力
            if (Input.GetKey(KeyCode.L))
            {
                powerTime += Time.deltaTime * poewerStoreSpeed;
                ball.transform.localPosition = new Vector2(Mathf.Cos(powerTime) * ringSize, Mathf.Sin(powerTime) * ringSize);
            }
            else
            {
                powerTime = Mathf.Clamp(powerTime -= Time.deltaTime, 0, 1);
            }
            //放手
            if (Input.GetKeyUp(KeyCode.L))
            {
                if (eShoot != null)
                {
                    Vector2 dir = ball.transform.position - gameObject.transform.position;
                    eShoot(ball.transform.position, dir.normalized);
                }
            }
        }
        rigid.velocity = velocity;


    }


    public enum PlayerType
    {
        A, B
    }

}

