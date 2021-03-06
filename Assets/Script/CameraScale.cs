﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    public GameObject playerA;
    public GameObject playerB;
    Vector3 playersCenter;
    public float maxDistance;
    Camera mainCamera;
    float minSacle = 5;
    public float maxScale = 15;
    public float scaleSpeed = 5;
    private void Start()
    {
        mainCamera = Camera.main;
        minSacle = mainCamera.orthographicSize;
    }
    private void Update()
    {
        //攝影機中心
        playersCenter = new Vector3(Mathf.Lerp(playerA.transform.position.x, playerB.transform.position.x, 0.5f),
                                                       Mathf.Lerp(playerA.transform.position.y, playerB.transform.position.y, 0.5f),
                                   -10);
        transform.position = new Vector3(Mathf.Lerp(mainCamera.transform.position.x, playersCenter.x, Time.deltaTime * scaleSpeed),
                                        Mathf.Lerp(mainCamera.transform.position.y, playersCenter.y, Time.deltaTime * scaleSpeed),
                                        playersCenter.z);

    }
    private void FixedUpdate()
    {
        //玩家超出最大距離時伸縮
        Debug.Log(Vector2.Distance(playerA.transform.position, playerB.transform.position));
        if (Vector2.Distance(playerA.transform.position, playerB.transform.position) > maxDistance)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, maxScale, Time.deltaTime * scaleSpeed);
        }
        else
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, minSacle, Time.deltaTime * scaleSpeed);
        }
    }
}
