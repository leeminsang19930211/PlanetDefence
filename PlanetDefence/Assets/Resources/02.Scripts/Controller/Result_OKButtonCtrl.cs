using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_OKButtonCtrl : MonoBehaviour
{
    public void OnClick()
    {
        EndingMgr.Inst.ReleaseBattleScene();
        EndingMgr.Inst.PopDownResultPanel();
        SceneLoader.LoadScene("Lobby");

        
    }
}
