using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBuildCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpBuild();
    }
}
