using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpLabInfos2Ctrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpLabInfos2(gameObject);
    }
}
