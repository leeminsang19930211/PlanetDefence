using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveInfotoBuildCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.RemoveInfotoBuild();
    }
}
