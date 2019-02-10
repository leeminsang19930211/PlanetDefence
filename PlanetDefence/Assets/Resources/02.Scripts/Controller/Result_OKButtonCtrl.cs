using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_OKButtonCtrl : MonoBehaviour
{
    public bool Clear { get; set; } = false;

    public void OnClick()
    {
        if(Clear == true)
        {
            BattleCtrl.Release_Clear();
            SceneLoader.LoadScene("Choice");
        }
        else
        {
            BattleCtrl.Release_Fail();
            SceneLoader.LoadScene("Lobby");
        }

        BattleGameObjectMgr.Inst.PopDownResult();
    }
}
