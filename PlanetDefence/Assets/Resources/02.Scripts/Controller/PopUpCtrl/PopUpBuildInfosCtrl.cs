using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBuildInfosCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpBuildInfos(gameObject);
    }
}
