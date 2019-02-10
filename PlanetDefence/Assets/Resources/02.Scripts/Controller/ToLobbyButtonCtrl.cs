using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLobbyButtonCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopUpToLobby();

        BattleGameObjectMgr.Inst.PopDownToLobby();
    }
}
