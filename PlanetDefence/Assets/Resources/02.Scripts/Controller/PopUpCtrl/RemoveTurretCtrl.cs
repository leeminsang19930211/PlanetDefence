using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTurretCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpExit();
        TurretMgr.Inst.RemoveTurretOnTurretSupport();
    }
}
