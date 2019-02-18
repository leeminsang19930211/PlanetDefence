using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNewButtonCtrl : MonoBehaviour
{
    public LobbyButtonsCtrl m_buttons = null;

    public void OnClick()
    {
        //Player.Inst._Reset();
        //SpaceShipMgr.Inst._Reset();
        //TurretMgr.Inst._Reset();
        //_EffectMgr.Inst._Reset();
        //BulletMgr.Inst._Reset();
        //BattleGameObjectMgr.Inst._Reset();

        FileMgr.Inst.ResetPlayerData();
        FileMgr.Inst.ResetGlobalData();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif

    }
}
