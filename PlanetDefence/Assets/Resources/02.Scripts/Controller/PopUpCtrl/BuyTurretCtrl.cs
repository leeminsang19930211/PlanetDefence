using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTurretCtrl : MonoBehaviour
{
    public void OnClick()
    {
        Player.Inst.BuyTurret(gameObject);
    }
}
