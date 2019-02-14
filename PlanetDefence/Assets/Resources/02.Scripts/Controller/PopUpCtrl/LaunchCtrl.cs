using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.Launch();
    }
}
