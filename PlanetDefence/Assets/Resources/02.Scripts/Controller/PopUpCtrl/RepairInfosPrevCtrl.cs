using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairInfosPrevCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.RepairInfosPrev();
    }
}
