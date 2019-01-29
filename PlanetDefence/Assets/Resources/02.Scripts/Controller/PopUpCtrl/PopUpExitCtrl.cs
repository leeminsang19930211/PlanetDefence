using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpExitCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpExit();
    }
}
