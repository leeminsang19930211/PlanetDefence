using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpLabInfoCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpLabInfo();
    }
}
