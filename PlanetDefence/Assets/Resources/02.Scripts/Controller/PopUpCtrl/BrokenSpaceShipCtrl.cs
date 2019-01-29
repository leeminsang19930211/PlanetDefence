using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenSpaceShipCtrl : MonoBehaviour
{
    public void OnMouseDown()
    {
        BattleGameObjectMgr.Inst.PopUpLab();
    }
}
