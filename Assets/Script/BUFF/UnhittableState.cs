using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnhittableState : MonoBehaviour
{
    public float duration = 5;
    HitableObj hitable;
    SpriteRenderer sprite;
    private void Start()
    {
        hitable = gameObject.GetComponent<HitableObj>();
        StartCoroutine(Unhittable_coro());
        sprite = gameObject.GetComponent<SpriteRenderer>();


    }
    float h, s = 1, v = 1;
    private void FixedUpdate()
    {
        h += Time.deltaTime;
        Debug.Log(h);
        Color color = Color.HSVToRGB(h % 1, s, v);
        sprite.color = color;

    }
    private void OnDestroy()
    {
        Color color = Color.HSVToRGB(0, 0, 1);
        sprite.color = color;

    }
    IEnumerator Unhittable_coro()
    {
        hitable.isHitable = false;
        yield return new WaitForSeconds(5);
        hitable.isHitable = true;
        Destroy(this);
    }
}
