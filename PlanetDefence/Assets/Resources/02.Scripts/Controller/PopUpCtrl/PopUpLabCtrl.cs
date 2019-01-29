using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpLabCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpLab();
    }
}
