using UnityEngine;
using System.Collections;

public class EndingMgr : MonoBehaviour
{
    public enum eResult
    {
        Fail,
        Clear,
        Clear_Last,
        End,
    }

    private static EndingMgr m_inst = null;

    private int m_leftEnemies = int.MaxValue;
    private int m_leftPlanetHP = int.MaxValue;
    private bool m_launchingSpaceShip = false;
    private eResult m_result = eResult.End;

    private GameObject m_resultPopUpPanel = null;
    private GameObject m_ending30Days = null;

    public static EndingMgr Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "EndingMgr";
                m_inst = container.AddComponent<EndingMgr>() as EndingMgr;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }
    public eResult Result
    {
        get { return m_result; }
    }


    public void _OnStart()
    {
        m_resultPopUpPanel = GameObject.Find("ResultPopUpPanel");
        m_resultPopUpPanel.SetActive(false);

        m_ending30Days = GameObject.Find("Ending_30days_Panel");
        m_ending30Days.SetActive(false);
    }

    public void _OnBattle()
    {
        m_resultPopUpPanel.SetActive(false);
        m_ending30Days.SetActive(false);

        m_result = eResult.Fail;
    }

    public int LeftEnemies
    {
        set
        {
            if (value < 0)
                return;

            m_leftEnemies = value;

            if (m_leftEnemies == 0 && m_leftPlanetHP > 0)
            {               
                if(m_launchingSpaceShip == false)
                {
                    if (GlobalGameObjectMgr.Inst.LeftDays > 0)
                    {
                        m_result = eResult.Clear;
                        PopUpResultPanel();
                    }
                    else
                    {
                        m_result = eResult.Clear_Last;
                        m_ending30Days.SetActive(true);
                    }
                }
            }
        }
    }

    public int LeftPlanetHP
    {
        set
        {
            if (value < 0)
                return;

            m_leftPlanetHP = value;

            if (m_leftPlanetHP == 0)
            {
                m_result = eResult.Fail;

                m_resultPopUpPanel.SetActive(false);
            }
        }
    }

    public bool LaunchingSpaceShip
    {
        set
        {
            m_launchingSpaceShip = value;
            m_result  = eResult.Clear;
        }
    }

    public void PopUpResultPanel()
    {
        if (m_result == eResult.Clear || m_result == eResult.Clear_Last)
        {
            AudioManager.Inst.playClearSFX(AudioManager.eClearSFX.ClearSFX);
        }
        else
        {
            AudioManager.Inst.playClearSFX(AudioManager.eClearSFX.FailedSFX);
        }

        m_resultPopUpPanel.SetActive(true);
    }

    public void PopDownResultPanel()
    {


        m_resultPopUpPanel.SetActive(false);
    }

    public void ReleaseBattleScene()
    {
        if (m_result == eResult.Clear || m_result == eResult.Clear_Last)
        {
            Release_Clear();            
        }
        else
        {
            Release_Fail();
            
        }
    }

    private void Release_Clear()
    {
        Player.Inst.Release_Clear();
        SpaceShipMgr.Inst.Release_Clear();
        TurretMgr.Inst.Release_Clear();
        EffectMgr.Inst.Release_Clear();
        BulletMgr.Inst.Release_Clear();
        BattleGameObjectMgr.Inst.Release_Clear();

        GlobalGameObjectMgr.Inst.Battle = false;
        GlobalGameObjectMgr.Inst.IncreaseDay();

        FileMgr.Inst.SaveGlobaData();
        FileMgr.Inst.SavePlayerData();
        FileMgr.Inst.SaveTurretData();
    }

    private void Release_Fail()
    {
      
        Player.Inst.Release_Fail();
        SpaceShipMgr.Inst.Release_Fail();
        TurretMgr.Inst.Release_Fail();
        EffectMgr.Inst.Release_Fail();
        BulletMgr.Inst.Release_Fail();
        BattleGameObjectMgr.Inst.Release_Fail();
 

        GlobalGameObjectMgr.Inst.Battle = false;
        GlobalGameObjectMgr.Inst.CurDay = 0;

        FileMgr.Inst.SavePlayerData();
        FileMgr.Inst.SaveGlobaData();
        FileMgr.Inst.SaveTurretData();
    }
}
