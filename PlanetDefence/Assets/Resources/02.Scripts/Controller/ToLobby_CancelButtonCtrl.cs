using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLobby_CancelButtonCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopDownToLobby();
    }
}
