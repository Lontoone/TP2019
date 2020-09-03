using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndUI : MonoBehaviour
{
    public Sprite p1, p2;
    public Image winner;

    bool isFin = false;

    public void End(string name)
    {
        if (!isFin)
        {
            isFin = true;
            if (name == "P1")
            {
                winner.sprite = p2;
            }
            else
            {
                winner.sprite = p1;
            }
        }
    }
}
