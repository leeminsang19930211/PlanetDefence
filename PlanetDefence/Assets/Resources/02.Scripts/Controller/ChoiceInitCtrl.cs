using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceInitCtrl : MonoBehaviour
{
    private void Awake()
    {
        GlobalGameObjectMgr.Inst.SetGameObectActive("Lobby", false);
        GlobalGameObjectMgr.Inst.SetGameObectActive("Choice", true);

        GlobalGameObjectMgr.Inst.MoveGameObjectToScene("Choice", "Choice");
    }
}
