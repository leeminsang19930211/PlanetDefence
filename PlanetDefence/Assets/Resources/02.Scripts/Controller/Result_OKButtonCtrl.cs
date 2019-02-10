using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_OKButtonCtrl : MonoBehaviour
{
    public bool Clear { get; set; } = false;

    public void OnClick()
    {
        GlobalGameObjectMgr.Inst.Battle = false;

        if(Clear == true)
        {
            SceneLoader.LoadScene("Choice");
        }
        else
        {
            SceneLoader.LoadScene("Lobby");
        }       
    }
}
