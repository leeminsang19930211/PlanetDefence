using UnityEngine;
using System.Collections;

public class EndingMgr : MonoBehaviour
{
    private static EndingMgr m_inst = null;

    private int m_leftEnemies = int.MaxValue;
    private int m_leftPlanetHP = int.MaxValue;
    private bool m_launchingSpaceShip = false;
    private bool m_clear = false;

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
                m_clear = true;

                if(m_launchingSpaceShip == false)
                {
                    if (GlobalGameObjectMgr.Inst.LeftDays > 0)
                    {
                        PopUpResultPanel();
                    }
                    else
                    {
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
                m_clear = false;

                m_resultPopUpPanel.SetActive(false);
            }
        }
    }

    public bool LaunchingSpaceShip
    {
        set
        {
            m_launchingSpaceShip = value;
            m_clear = true;
        }
    }

    public void PopUpResultPanel()
    {
        m_resultPopUpPanel.SetActive(true);
    }

    public void PopDownResultPanel()
    {
        m_resultPopUpPanel.SetActive(false);
    }

    public void ReleaseBattleScene()
    {
        if (m_clear)
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
        PlanetCtrl.Inst.Release_Clear();

        GlobalGameObjectMgr.Inst.Battle = false;
        GlobalGameObjectMgr.Inst.IncreaseDay();

        FileMgr.Inst.SaveGlobaData();
        FileMgr.Inst.SavePlayerData();
    }

    private void Release_Fail()
    {
        Player.Inst.Release_Fail();
        SpaceShipMgr.Inst.Release_Fail();
        TurretMgr.Inst.Release_Fail();
        EffectMgr.Inst.Release_Fail();
        BulletMgr.Inst.Release_Fail();
        BattleGameObjectMgr.Inst.Release_Fail();
        PlanetCtrl.Inst.Release_Fail();

        GlobalGameObjectMgr.Inst.Battle = false;
        GlobalGameObjectMgr.Inst.CurDay = 0;

        FileMgr.Inst.SavePlayerData();
        FileMgr.Inst.SaveGlobaData();
    }
}
