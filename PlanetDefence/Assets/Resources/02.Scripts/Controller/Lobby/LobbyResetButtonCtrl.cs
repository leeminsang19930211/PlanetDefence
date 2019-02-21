using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyResetButtonCtrl : MonoBehaviour
{
    public LobbyButtonsCtrl m_buttons = null;

    public void OnClick()
    {
        FileMgr.Inst.ResetPlayerData();
        FileMgr.Inst.ResetGlobalData();
        FileMgr.Inst.ResetTurretData();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
