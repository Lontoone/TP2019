using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffGiver : MonoBehaviour
{
    public BuffType btype;
    public GameObject effect;
    private void OnTriggerStay2D(Collider2D other)
    {
        HitableObj player = other.GetComponent<HitableObj>();
        if (player != null)
        {
            //無敵
            if (btype == BuffType.Unhittable)
            {
                UnhittableState playUnhit = player.gameObject.GetComponent<UnhittableState>();
                if (playUnhit != null)
                {
                    Destroy(playUnhit);
                }
                player.gameObject.AddComponent<UnhittableState>();
            }
            //加速
            else if (btype == BuffType.Accelerate)
            {
                Accelerater playAcc = player.gameObject.GetComponent<Accelerater>();
                if (playAcc != null)
                {
                    Destroy(playAcc);
                }
                player.gameObject.AddComponent<Accelerater>();
            }
            //治療
            else if (btype == BuffType.Healer)
            {
                Healer healer = player.gameObject.GetComponent<Healer>();
                if (healer != null)
                {
                    Destroy(healer);
                }
                player.gameObject.AddComponent<Healer>();
            }
            //中毒
            else if (btype == BuffType.Poison)
            {
                Poison poison = player.gameObject.GetComponent<Poison>();
                if (poison != null)
                {
                    Destroy(poison);
                }
                player.gameObject.AddComponent<Poison>();
            }

            //給特效
            if (effect != null)
            {
                GameObject _e = Instantiate(effect, player.transform.position, Quaternion.identity, player.transform);
                Destroy(_e, 5);
            }
            Destroy(gameObject);
        }
    }
    public enum BuffType
    {
        Unhittable, Accelerate, Healer, Poison
    }

    ///??
    public bool HasBuff<T>(GameObject player)
    {
        T currentBuff = player.GetComponent<T>();
        if (currentBuff != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
