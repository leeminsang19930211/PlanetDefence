using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoadingInitCtrl : MonoBehaviour
{ 
    private void Awake()
    {
        Screen.SetResolution(Screen.width, Screen.height, true);

#if UNITY_ANDROID

        Application.targetFrameRate = 60;

#endif
        FileMgr.Inst.LoadGlobalData();
        FileMgr.Inst.LoadPlayerData();
    }
}
