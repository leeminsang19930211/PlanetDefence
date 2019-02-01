using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRemoveWarningCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.ExitRemoveWarning();
    }
}
