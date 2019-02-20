using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenSpaceShipButtonCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpLab();
    }
}
