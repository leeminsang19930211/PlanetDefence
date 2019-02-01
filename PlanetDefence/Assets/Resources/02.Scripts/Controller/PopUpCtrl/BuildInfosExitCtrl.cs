using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInfosExitCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.BuildInfosExit();
    }
}
