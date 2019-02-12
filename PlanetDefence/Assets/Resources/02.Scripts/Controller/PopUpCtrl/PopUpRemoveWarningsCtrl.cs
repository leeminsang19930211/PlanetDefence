using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpRemoveWarningsCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpRemoveWarnings();
    }
}
