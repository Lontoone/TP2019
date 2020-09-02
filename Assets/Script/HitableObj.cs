using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
//可攻擊物件
public class HitableObj : MonoBehaviour
{
    private static event Action<GameObject, float> Hit_event;
    //public GameObject hit_numUI_prefab;//跳傷害的UI
    //public static event Action<GameObject> Hit_effect_event;
    public event Action Die_event;
    public event Action gotHit_event;
    public float HP = 20;
    public bool isDead = false;
    public bool isHitable = true;

    private void Start()
    {
        Hit_event += Hit;
        //TODO:自動產生血條
    }
    private void OnDestroy()
    {
        Hit_event -= Hit;
    }

    public static void Hit_event_c(GameObject t, float d)
    {
        if (Hit_event != null)
        {
            Hit_event(t, d);
        }

    }


    void Hit(GameObject target, float damage)
    {
        if (target == gameObject)
        {
            Debug.Log(gameObject.name + " 受到 " + damage + " 傷害");

            if (isHitable)
            {
                HP -= damage;
                //特效:
                Hit_effect();
                //HitNumUIManager.Pop_Damage_UI(target, damage);

            }

            //判斷死亡
            if (HP <= 0)
            {
                if (Die_event != null && !isDead)
                {
                    isDead = true;
                    Die_event();
                }
            }
            else
            {
                if (gotHit_event != null)
                {
                    Debug.Log("<color=green>HURT</color>");
                    gotHit_event();
                }
            }

        }
    }

    void Hit_effect()
    {
        //晃鏡頭
        //GameObject.FindObjectOfType<CameraFollow>().CameraShake(0.25f,0.1f,2.5f);
        //CameraFollow.CameraShake_c(0.25f, 0.1f, 2.5f);
    }
}
