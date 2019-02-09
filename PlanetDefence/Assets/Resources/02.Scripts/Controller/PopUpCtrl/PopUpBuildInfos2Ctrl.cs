using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBuildInfos2Ctrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpBuildInfos2(gameObject);
    }
}
