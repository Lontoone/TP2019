using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbarControl : MonoBehaviour
{
    public UnityEngine.UI.Image HpBar;
    float MaxHP;
    HitableObj hitable;
    void Start()
    {
        hitable = gameObject.GetComponent<HitableObj>();
        MaxHP = hitable.HP;
        hitable.gotHit_event += UpdateHPbar;
        hitable.Die_event += Die;
    }
    private void OnDestroy()
    {
        hitable.gotHit_event -= UpdateHPbar;
        hitable.Die_event -= Die;
    }

    //更新HP條
    void UpdateHPbar()
    {
        HpBar.fillAmount = hitable.HP / MaxHP;
    }
    //死亡時...
    void Die()
    {
        HpBar.fillAmount = 0;
        //TODO???
        Debug.Log(gameObject.name + "死掉了");
    }

}
