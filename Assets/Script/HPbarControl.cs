using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbarControl : MonoBehaviour
{

    public GameObject endCanvas;
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
    public void UpdateHPbar()
    {
        HpBar.fillAmount = hitable.HP / MaxHP;
    }
    //死亡時...
    void Die()
    {
        HpBar.fillAmount = 0;
        //TODO???
        Debug.Log(gameObject.name + "死掉了");
        endCanvas.SetActive(true);
        endCanvas.GetComponent<EndUI>().End(gameObject.name);

    }

}
