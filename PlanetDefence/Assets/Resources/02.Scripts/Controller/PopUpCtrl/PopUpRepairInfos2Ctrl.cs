using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpRepairInfos2Ctrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpRepairInfos2(gameObject);
    }
}
