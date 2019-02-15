using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ToLobby_OkButtonCtrl : MonoBehaviour
{
    public void OnClick()
    {
      

        SceneLoader.LoadScene("Lobby");

        BattleGameObjectMgr.Inst.PopDownToLobby();
    }
}
