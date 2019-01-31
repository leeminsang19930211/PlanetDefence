using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairInfosExitCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.RepairInfosExit();
    }
}
