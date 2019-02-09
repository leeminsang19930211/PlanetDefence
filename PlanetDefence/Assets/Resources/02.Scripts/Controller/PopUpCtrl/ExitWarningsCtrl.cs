using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitWarningsCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.ExitWarnings();
    }
}
