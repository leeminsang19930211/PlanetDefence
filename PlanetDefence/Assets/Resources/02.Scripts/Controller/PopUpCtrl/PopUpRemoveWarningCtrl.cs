using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpRemoveWarningCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpRemoveWarning();
    }
}
