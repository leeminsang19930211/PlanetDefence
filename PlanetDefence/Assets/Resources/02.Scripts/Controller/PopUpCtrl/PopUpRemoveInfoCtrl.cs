using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpRemoveInfoCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpRemoveInfo();
    }
}
