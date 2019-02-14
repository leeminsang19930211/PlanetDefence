using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyRepairCtrl : MonoBehaviour
{
    public void OnClick()
    {
        Player.Inst.BuyRepair(gameObject);
    }
}
