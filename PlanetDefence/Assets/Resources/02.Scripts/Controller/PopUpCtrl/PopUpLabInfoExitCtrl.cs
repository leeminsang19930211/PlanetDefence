using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpLabInfoExitCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpLabInfoExit();
    }
}
