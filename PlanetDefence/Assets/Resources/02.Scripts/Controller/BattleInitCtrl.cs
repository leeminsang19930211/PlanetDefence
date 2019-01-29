using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitCtrl : MonoBehaviour
{
    private void Awake()
    {
        GlobalGameObjectMgr.Inst.SetGameObectActive("Choice", false);
        GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", true);

        GlobalGameObjectMgr.Inst.MoveGameObjectToScene("Battle", "Battle");

        BattleGameObjectMgr.Inst.Init();
    }
}
