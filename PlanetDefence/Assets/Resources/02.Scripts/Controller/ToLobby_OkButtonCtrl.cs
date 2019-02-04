using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLobby_OkButtonCtrl : MonoBehaviour
{
    public void OnClick()
    {
        BattleGameObjectMgr.Inst.PopDownToLobby();
        SceneLoader.LoadScene("Lobby");
    }
}
