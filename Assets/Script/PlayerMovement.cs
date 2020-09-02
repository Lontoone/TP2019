using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    //玩家AB
    public PlayerType playerType;
    //public float speed = 5;
    public event Action<Vector2, Vector2> eShoot; //射出(目前位置,方向)
    Rigidbody2D rigid;
    Vector2 velocity = new Vector2();
    public GameObject ball;
    float powerTime = 0;
    public float poewerStoreSpeed = 5;
    [Range(0.01f, 1)]
    public float ringSize = 0.2f;

    public float walkGapTime = 0.2f;
    public float moveGapDistance = 1;
    Coroutine cWalkGap;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        //WASD按鍵移動
        if (playerType == PlayerType.A)
        {
            //velocity = new Vector2(Mathf.CeilToInt(Input.GetAxis("AHorizontal")), Mathf.CeilToInt(Input.GetAxis("AVertical"))) * speed;
            if (cWalkGap == null)
            {
                if (Input.GetKey(KeyCode.A)) { cWalkGap = StartCoroutine(WalkTo_Coro(new Vector2(-1, 0) * moveGapDistance)); }
                else if (Input.GetKey(KeyCode.D)) { cWalkGap = StartCoroutine(WalkTo_Coro(new Vector2(1, 0) * moveGapDistance)); }
                else if (Input.GetKey(KeyCode.W)) { cWalkGap = StartCoroutine(WalkTo_Coro(new Vector2(0, 1) * moveGapDistance)); }
                else if (Input.GetKey(KeyCode.S)) { cWalkGap = StartCoroutine(WalkTo_Coro(new Vector2(0, -1) * moveGapDistance)); }
            }

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
            //velocity = new Vector2(Input.GetAxis("BHorizontal"), Input.GetAxis("BVertical")) * speed;
            if (cWalkGap == null)
            {
                if (Input.GetKey(KeyCode.LeftArrow)) { cWalkGap = StartCoroutine(WalkTo_Coro(new Vector2(-1, 0) * moveGapDistance)); }
                else if (Input.GetKey(KeyCode.RightArrow)) { cWalkGap = StartCoroutine(WalkTo_Coro(new Vector2(1, 0) * moveGapDistance)); }
                else if (Input.GetKey(KeyCode.UpArrow)) { cWalkGap = StartCoroutine(WalkTo_Coro(new Vector2(0, 1) * moveGapDistance)); }
                else if (Input.GetKey(KeyCode.DownArrow)) { cWalkGap = StartCoroutine(WalkTo_Coro(new Vector2(0, -1) * moveGapDistance)); }
            }
            //續力
            if (Input.GetKey(KeyCode.M))
            {
                powerTime += Time.deltaTime * poewerStoreSpeed;
                ball.transform.localPosition = new Vector2(Mathf.Cos(powerTime) * ringSize, Mathf.Sin(powerTime) * ringSize);
            }
            else
            {
                powerTime = Mathf.Clamp(powerTime -= Time.deltaTime, 0, 1);
            }
            //放手
            if (Input.GetKeyUp(KeyCode.M))
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


    IEnumerator WalkTo_Coro(Vector2 dir)
    {
        Vector2 goal_pos = (Vector2)transform.position + dir;
        /*
        while (Vector2.Distance((Vector2)transform.position, goal_pos) > 0.01f)
        {
            transform.position = new Vector2(
                Mathf.Lerp(transform.position.x, goal_pos.x, speed * Time.deltaTime),
                Mathf.Lerp(transform.position.x, goal_pos.x, speed * Time.deltaTime)
            );
            Debug.Log(goal_pos);
            yield return new WaitForFixedUpdate();

        }
        */
        transform.position = goal_pos;
        yield return new WaitForSeconds(walkGapTime);
        cWalkGap = null;

    }

}

