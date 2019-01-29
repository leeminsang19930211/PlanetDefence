using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArrowCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.RotateCameraToLeft();
    }
}
