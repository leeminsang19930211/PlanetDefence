using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyLabCtrl : MonoBehaviour
{
    public void OnClick()
    {
        Player.Inst.BuyLab(gameObject);
    }
}
