using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLobbySceneCtrl : MonoBehaviour
{
    public void PopUpResult()
    {
        EndingMgr.Inst.PopUpResultPanel();
    }

    public void ToLobbyScene()
    {
        SceneLoader.LoadScene("Lobby");       
    }
}
