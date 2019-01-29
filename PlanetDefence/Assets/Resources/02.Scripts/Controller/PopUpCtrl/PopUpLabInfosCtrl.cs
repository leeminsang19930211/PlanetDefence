using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpLabInfosCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpLabInfos();
    }
}
