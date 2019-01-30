using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabInfosExitCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.LabInfosExit();
    }
}
