using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpRepairInfosCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpRepairInfos(gameObject);
    }
}
