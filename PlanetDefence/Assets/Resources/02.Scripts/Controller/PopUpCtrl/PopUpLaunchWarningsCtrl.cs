using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpLaunchWarningsCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpLaunchWarnings();
    }
}
