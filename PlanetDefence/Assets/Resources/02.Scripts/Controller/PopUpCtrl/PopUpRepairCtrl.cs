using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpRepairCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpRepair();
    }
}
