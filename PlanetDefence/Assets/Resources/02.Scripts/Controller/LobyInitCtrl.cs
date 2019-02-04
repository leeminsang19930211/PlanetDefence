using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobyInitCtrl : MonoBehaviour
{
    private void Awake()
    {
        GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", false);
        GlobalGameObjectMgr.Inst.SetGameObectActive("Lobby", true);
    }

}
