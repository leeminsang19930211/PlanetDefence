using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;   // 추가

public class BattleGameObjectMgr : MonoBehaviour
{
    private static BattleGameObjectMgr m_inst = null;

    public static BattleGameObjectMgr Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "BattleGameObjectMgr";
                m_inst = container.AddComponent<BattleGameObjectMgr>() as BattleGameObjectMgr;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }

 
    private bool m_init = false;

    /* 여기서 부터 필요한 내용들을 작성하면 된다 */
    private MainCameraCtrl m_mainCameraCtrl = null;
    private EnemyCntCtrl m_enemyCntCtrl = null;
    private PlanetHPCtrl m_planetHpCtrl = null;
    private ResourceCtrl m_resourceCtrl = null;
    private MiniPlanetCtrl m_miniPlanetCtrl = null;
    private ShieldCtrl m_shieldCtrl = null;

    private GameObject m_toLobbyPopUpPanel = null;
    private GameObject m_resultPopUpPanel = null;

    private GameObject m_laboratoryPopUp = null;

    private GameObject m_buildPopUp = null;
    private GameObject m_labScroll = null;
    private GameObject m_repairScroll = null;
    private GameObject m_buildScroll = null;
    private GameObject m_rightArrow = null;
    private GameObject m_leftArrow = null;
    private GameObject m_ButtonLabFake = null;
    private GameObject m_ButtonRepairFake = null;
    private GameObject m_ButtonBuildFake = null;

    private GameObject[] m_LabButtons;
    private GameObject[] m_LabButtons_Black;
    private GameObject[] m_LabInfoScrolls;
    private GameObject[] m_LabImages_Black;
    public GameObject[] m_LabStartButtons;
    public GameObject m_LabWarningNoBP;
    public GameObject m_LabWarningMax;
    public GameObject m_LabWarningNoRsrc;
    private GameObject[] m_RepairButtons;
    private GameObject[] m_RepairButtons_Black;
    private GameObject[] m_RepairInfoScrolls;
    private GameObject[] m_RepairImages_Black;
    public GameObject[] m_RepairStartButtons;
    public GameObject m_RepairWarningAlready;
    private GameObject m_LaunchWarningNotRepaired;
    private GameObject m_LaunchWarning;

    private GameObject[] m_BuildButtons;
	private GameObject[] m_BuildButtons_Black;
    private GameObject[] m_BuildInfoScrolls;
    private GameObject[] m_BuildImages_Black;
    public GameObject[] m_BuildStartButtons;
    public GameObject m_BuildWarningNoBP;
    public GameObject m_BuildWarningAlready;
    public GameObject m_BuildWarningNoRsrc;


    private GameObject m_RemoveInfoScroll;
 	public GameObject m_RemoveWarningYet;
    private GameObject m_RemoveWarning;
    private GameObject[] m_TurretSupports;

    public GameObject[] m_BrokenSpaceships;
    public GameObject[] m_Rockets;

    // 추가
    private GameObject m_RepairedSpaceShip;

    public void Instantiate()
    {
        if (m_inst == null)
        {
            GameObject container = new GameObject();
            container.name = "BattleGameObjectMgr";
            m_inst = container.AddComponent<BattleGameObjectMgr>() as BattleGameObjectMgr;
            DontDestroyOnLoad(container);
        }
    }

    public void Init()
    {
        if (m_init)
            return;
        else
            m_init = true;

        m_mainCameraCtrl = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<MainCameraCtrl>();
        m_enemyCntCtrl = GameObject.FindGameObjectWithTag("ENEMY_CNT")?.GetComponent<EnemyCntCtrl>();
        m_planetHpCtrl = GameObject.FindGameObjectWithTag("PLANET_HP")?.GetComponent<PlanetHPCtrl>();
        m_resourceCtrl = GameObject.FindGameObjectWithTag("RESOURCE")?.GetComponent<ResourceCtrl>();
        m_miniPlanetCtrl = GameObject.FindGameObjectWithTag("MINIPLANET")?.GetComponent<MiniPlanetCtrl>();
        m_shieldCtrl = GameObject.Find("Shield")?.GetComponent<ShieldCtrl>();
    
        m_toLobbyPopUpPanel = GameObject.Find("ToLobbyPopUpPanel");
        m_resultPopUpPanel = GameObject.Find("ResultPopUpPanel");
        
        m_laboratoryPopUp = GameObject.FindGameObjectWithTag("LABORATORY_POPUP");
        m_buildPopUp = GameObject.FindGameObjectWithTag("BUILD_POPUP");
        m_labScroll = GameObject.Find("LabScroll");
        m_repairScroll = GameObject.Find("RepairScroll");
        m_buildScroll = GameObject.Find("BuildScroll");
        m_rightArrow = GameObject.Find("RightArrow");
        m_leftArrow = GameObject.Find("LeftArrow");
        m_ButtonLabFake = GameObject.Find("Button_Lab_fake");
        m_ButtonRepairFake = GameObject.Find("Button_Repair_fake");
        m_ButtonBuildFake = GameObject.Find("Button_Build_fake");
        m_LabButtons = GameObject.FindGameObjectsWithTag("LABBUTTON");
        m_LabButtons_Black = GameObject.FindGameObjectsWithTag("LABBUTTON_BLACK");
        m_LabInfoScrolls = GameObject.FindGameObjectsWithTag("LABINFO");
        m_LabImages_Black = GameObject.FindGameObjectsWithTag("LABIMAGE_BLACK");
        m_LabStartButtons = GameObject.FindGameObjectsWithTag("LABSTARTBUTTON");
        m_LabWarningNoBP = GameObject.Find("LabWarningNoBP");
        m_LabWarningMax = GameObject.Find("LabWarningMax");
        m_LabWarningNoRsrc = GameObject.Find("LabWarningNoRsrc");
        m_RepairButtons = GameObject.FindGameObjectsWithTag("REPAIRBUTTON");
        m_RepairButtons_Black = GameObject.FindGameObjectsWithTag("REPAIRBUTTON_BLACK");
        m_RepairInfoScrolls = GameObject.FindGameObjectsWithTag("REPAIRINFO");
        m_RepairImages_Black = GameObject.FindGameObjectsWithTag("REPAIRIMAGE_BLACK");
        m_RepairStartButtons = GameObject.FindGameObjectsWithTag("REPAIRSTARTBUTTON");
        m_RepairWarningAlready = GameObject.Find("RepairWarningAlready");
        m_LaunchWarningNotRepaired = GameObject.Find("LaunchWarningNotRepaired");
        m_LaunchWarning = GameObject.Find("LaunchWarningPanel");

        m_BuildButtons = GameObject.FindGameObjectsWithTag("BUILDBUTTON");
 		m_BuildButtons_Black = GameObject.FindGameObjectsWithTag("BUILDBUTTON_BLACK");
        m_BuildInfoScrolls = GameObject.FindGameObjectsWithTag("BUILDINFO");
        m_BuildImages_Black = GameObject.FindGameObjectsWithTag("BUILDIMAGE_BLACK");
        m_BuildStartButtons = GameObject.FindGameObjectsWithTag("BUILDSTARTBUTTON");

        m_BuildWarningNoBP = GameObject.Find("BuildWarningNoBP");
        m_BuildWarningAlready = GameObject.Find("BuildWarningAlready");
        m_BuildWarningNoRsrc = GameObject.Find("BuildWarningNoRsrc");

        m_RemoveInfoScroll = GameObject.FindGameObjectWithTag("REMOVEINFO");
        m_RemoveWarningYet = GameObject.Find("RemoveWarningYet");
        m_RemoveWarning = GameObject.Find("RemoveWarningPanel");

        m_TurretSupports = GameObject.FindGameObjectsWithTag("TURRET_SUPPORT");
     
        m_BrokenSpaceships = GameObject.FindGameObjectsWithTag("BROKENSPACESHIP");
        m_Rockets = GameObject.FindGameObjectsWithTag("ROCKET");
        m_RepairedSpaceShip = GameObject.Find("RepairedSpaceShip");

        // 시작시에 껏다 키는 것들 다 이 함수 안에 작성할것
        _InitContents();
    }

    public void _Reset()
    {
        if (m_inst == null)
            return;

        m_init = false;

        _InitContents();
    }

    private void _InitContents()
    {
        PopDownToLobby();
        PopDownResult();

        m_RepairedSpaceShip.SetActive(false);
        m_BrokenSpaceships[3].SetActive(false);
        m_BrokenSpaceships[2].SetActive(false);
        m_BrokenSpaceships[1].SetActive(false);
        for (int i = 0; i < m_Rockets.Length; i++)
        {
            m_Rockets[i].SetActive(false);
        }

        m_laboratoryPopUp.SetActive(false);
        m_buildPopUp.SetActive(false);
    }

    public void Release_Clear()
    {
        m_miniPlanetCtrl.AllToFalse();
    }

    public void Release_Fail()
    {
        m_miniPlanetCtrl.AllToFalse();
    }

    public void ShowShield(PlanetArea area, Turret turret)
    {
        m_shieldCtrl.ShowShield(area, turret);
    }

    public void HideShields(PlanetArea area)
    {
        m_shieldCtrl.HideAllShields(area);
    }

    public void FlashMiniPlanet(PlanetArea area)
    {
        m_miniPlanetCtrl.FlashArea(area);
    }

    public void RotateCameraToRight()
    {
        m_mainCameraCtrl.RotateToRight();
    }

    public void RotateCameraToLeft()
    {
        m_mainCameraCtrl.RotateToLeft();
    }

    public void RotateMiniPlanetSpotToTarget(Vector3 targetDir)
    {
        if(m_miniPlanetCtrl)
        {
            m_miniPlanetCtrl.RotateToTarget(targetDir);
        }
         
    }

    public void AddEnemyCnt(int add)
    {
        if (m_enemyCntCtrl)
            m_enemyCntCtrl.AddMaxEnemy(add);
    }

    public void UpdateEnemyCnt(int maxEnemyCnt)
    {
        if(m_enemyCntCtrl)
            m_enemyCntCtrl.MaxEnemyCnt = maxEnemyCnt;
    }

    public void AddDestroyedEnemy(int add)
    {
        if (m_enemyCntCtrl)
            m_enemyCntCtrl.AddDestroyedEnemy(add);
    }
    
    public void UpdatePlanetHP(int maxHP, int curHP)
    {
        if (m_planetHpCtrl == null)
            return;

        m_planetHpCtrl.MaxHP = maxHP;
        m_planetHpCtrl.CurHP = curHP; 
    }

    public void UpdateJunkCnt(int junkCnt)
    {
        m_resourceCtrl.JunkCnt = junkCnt;
    }

    public void UpdateEleCircuitCnt(int eleCircuitCnt)
    {
        m_resourceCtrl.EleCircuitCnt = eleCircuitCnt;
    }

    public void UpdateCoinCnt(int coinCnt)
    {
        m_resourceCtrl.CoinCnt = coinCnt;
    }

    public void PopUpLab()
    {
        m_laboratoryPopUp.SetActive(true);
        m_rightArrow.SetActive(false);
        m_leftArrow.SetActive(false);
        m_labScroll.SetActive(true);
        m_repairScroll.SetActive(false);
        m_ButtonLabFake.SetActive(true);
        m_ButtonRepairFake.SetActive(false);
        foreach (GameObject m_LabInfoScroll in m_LabInfoScrolls)
        {
            m_LabInfoScroll.SetActive(false);
        }

        foreach (GameObject m_RepairInfoScroll in m_RepairInfoScrolls)
        {
            m_RepairInfoScroll.SetActive(false);
        }

        m_LabWarningNoBP.SetActive(false);
        m_LabWarningMax.SetActive(false);
        m_LabWarningNoRsrc.SetActive(false);
        m_RepairWarningAlready.SetActive(false);
        m_LaunchWarningNotRepaired.SetActive(false);
        m_LaunchWarning.SetActive(false);

        for(int i=0;i<12;i++)
        {
            m_LabButtons_Black[i].SetActive(false);
        }
        for(int i=12;i<16;i++)
        {
            if(true==Player.Inst.CheckUnLock((Turret)i-4))
            {
                m_LabButtons_Black[i].SetActive(false);
            }
        }
        for(int i=16;i<m_LabButtons.Length;i++)
        {
            if(true==Player.Inst.CheckUnLock((Turret)i+4))
            {
                m_LabButtons_Black[i].SetActive(false);
            }
        }
    }

    public void PopUpRepair()
    {
        m_labScroll.SetActive(false);
        m_repairScroll.SetActive(true);
        m_ButtonLabFake.SetActive(false);
        m_ButtonRepairFake.SetActive(true);

        for (int i = 0; i < (int)SpaceShipPart.End; ++i)
        {
            if (true == Player.Inst.CheckUnLock((SpaceShipPart)i))
            {
                m_RepairButtons_Black[i].SetActive(false);
            }
        }
    }

    public void PopUpExit()
    {
        m_laboratoryPopUp.SetActive(false);

        m_buildPopUp.SetActive(false);

        m_rightArrow.SetActive(true);
        m_leftArrow.SetActive(true);
    }

    public void PopUpToLobby()
    {
        if (m_toLobbyPopUpPanel == null)
            return;

        m_toLobbyPopUpPanel.SetActive(true);
    }

    public void PopDownToLobby()
    {
        if (m_toLobbyPopUpPanel == null)
            return;

        m_toLobbyPopUpPanel.SetActive(false);
    }

    public void PopUpResult(bool clear)
    {
        m_resultPopUpPanel.SetActive(true);

        Result_OKButtonCtrl ctrl = m_resultPopUpPanel.GetComponentInChildren<Result_OKButtonCtrl>();

        ctrl.Clear = clear;
    }

    public void PopDownResult()
    { 
        m_resultPopUpPanel.SetActive(false);
    }

    public void PopUpLabInfos(GameObject ThisLabButton)
    {
        int LabButtonIdx = System.Array.IndexOf(m_LabButtons, ThisLabButton);

        m_LabInfoScrolls[LabButtonIdx].SetActive(true);


        if(LabButtonIdx>=0 && LabButtonIdx<12)
        {
            m_LabImages_Black[LabButtonIdx].SetActive(false);
        }
        else if(LabButtonIdx>=12 && LabButtonIdx<16)
        {
            if(true==Player.Inst.CheckUnLock((Turret)LabButtonIdx - 4))
            {
                m_LabImages_Black[LabButtonIdx].SetActive(false);
            }
               
        }
        else if(LabButtonIdx<m_LabButtons.Length)
        {
            if (true == Player.Inst.CheckUnLock((Turret)LabButtonIdx + 4))
            {
                m_LabImages_Black[LabButtonIdx].SetActive(false);
            }
        }

    }

    public void PopUpLabInfos2(GameObject ThisLabButtonB)
    {
        int LabButtonBIdx = System.Array.IndexOf(m_LabButtons_Black, ThisLabButtonB);

        m_LabInfoScrolls[LabButtonBIdx].SetActive(true);

        if (LabButtonBIdx >= 0 && LabButtonBIdx < 12)
        {
            m_LabImages_Black[LabButtonBIdx].SetActive(false);
        }
        else if (LabButtonBIdx >= 12 && LabButtonBIdx < 16)
        {
            if (true == Player.Inst.CheckUnLock((Turret)LabButtonBIdx - 4))
            {
                m_LabImages_Black[LabButtonBIdx].SetActive(false);
            }

        }
        else if (LabButtonBIdx < m_LabButtons_Black.Length)
        {
            if (true == Player.Inst.CheckUnLock((Turret)LabButtonBIdx + 4))
            {
                m_LabImages_Black[LabButtonBIdx].SetActive(false);
            }
        }

    }

    public void LabInfosExit()
    {
        for (int i = 0; i < m_LabInfoScrolls.Length; i++)
        {
            m_LabInfoScrolls[i].SetActive(false);
        }
    }

    public void LabInfosNext()
    {
        for (int i = 0; i < m_LabInfoScrolls.Length; i++)
        {
            if (m_LabInfoScrolls[i].activeSelf == true)
            {
                m_LabInfoScrolls[i].SetActive(false);
                m_LabInfoScrolls[i + 1].SetActive(true);

                if(i>=0 && i<11)
                {
                    m_LabImages_Black[i + 1].SetActive(false);
                }
                else if(i>=11 && i<15)
                {
                    if (true == Player.Inst.CheckUnLock((Turret)i - 3))
                    {
                        m_LabImages_Black[i + 1].SetActive(false);
                    }
                }
                else if(i>=15 && i<19)
                {
                    if (true == Player.Inst.CheckUnLock((Turret)i + 5))
                    {
                        m_LabImages_Black[i + 1].SetActive(false);
                    }
                }

                return;
            }

        }

    }

    public void LabInfosPrev()
    {
        for (int i = 0; i < m_LabInfoScrolls.Length; i++)
        {
            if (m_LabInfoScrolls[i].activeSelf == true)
            {
                m_LabInfoScrolls[i].SetActive(false);
                m_LabInfoScrolls[i - 1].SetActive(true);

                if (i > 0 && i < 13)
                {
                    m_LabImages_Black[i - 1].SetActive(false);
                }
                else if (i >= 13 && i < 17)
                {
                    if (true == Player.Inst.CheckUnLock((Turret)i - 5))
                    {
                        m_LabImages_Black[i - 1].SetActive(false);
                    }
                }
                else if (i >= 17 && i < m_LabInfoScrolls.Length)
                {
                    if (true == Player.Inst.CheckUnLock((Turret)i + 3))
                    {
                        m_LabImages_Black[i - 1].SetActive(false);
                    }
                }

                return;
            }

        }
    }

    public void PopUpRepairInfos(GameObject ThisRepairButton)
    {
        int RepairButtonIdx = System.Array.IndexOf(m_RepairButtons, ThisRepairButton);

        m_RepairInfoScrolls[RepairButtonIdx].SetActive(true);


        if (true == Player.Inst.CheckUnLock((SpaceShipPart)RepairButtonIdx))
        {
            m_RepairImages_Black[RepairButtonIdx].SetActive(false);
        }

    }

    public void PopUpRepairInfos2(GameObject ThisRepairButtonB)
    {
        int RepairButtonBIdx = System.Array.IndexOf(m_RepairButtons_Black, ThisRepairButtonB);

        m_RepairInfoScrolls[RepairButtonBIdx].SetActive(true);

        if (true == Player.Inst.CheckUnLock((SpaceShipPart)RepairButtonBIdx))
        {
            m_RepairImages_Black[RepairButtonBIdx].SetActive(false);
        }

    }

    public void RepairInfosExit()
    {
        for (int i = 0; i < m_RepairInfoScrolls.Length; i++)
        {
            m_RepairInfoScrolls[i].SetActive(false);
        }
    }

    public void RepairInfosNext()
    {
        for (int i = 0; i < m_RepairInfoScrolls.Length; i++)
        {
            if (m_RepairInfoScrolls[i].activeSelf == true)
            {
                m_RepairInfoScrolls[i].SetActive(false);
                m_RepairInfoScrolls[i + 1].SetActive(true);

                if (true == Player.Inst.CheckUnLock((SpaceShipPart)i))
                {
                    m_RepairImages_Black[i + 1].SetActive(false);
                }

                return;
            }

        }

    }

    public void RepairInfosPrev()
    {
        for (int i = 0; i < m_RepairInfoScrolls.Length; i++)
        {
            if (m_RepairInfoScrolls[i].activeSelf == true)
            {
                m_RepairInfoScrolls[i].SetActive(false);
                m_RepairInfoScrolls[i - 1].SetActive(true);

                if (true == Player.Inst.CheckUnLock((SpaceShipPart)i))
                {
                    m_RepairImages_Black[i - 1].SetActive(false);
                }

                return;
            }

        }
    }

    public void PopUpBuild()
    {
        m_buildPopUp.SetActive(true);
        m_rightArrow.SetActive(false);
        m_leftArrow.SetActive(false);
        m_buildScroll.SetActive(true);
        m_ButtonBuildFake.SetActive(true);
        foreach (GameObject m_BuildInfoScroll in m_BuildInfoScrolls)
        {
            m_BuildInfoScroll.SetActive(false);
        }

        m_BuildWarningNoBP.SetActive(false);
        m_BuildWarningAlready.SetActive(false);
        m_BuildWarningNoRsrc.SetActive(false);
        m_RemoveInfoScroll.SetActive(false);
        m_RemoveWarningYet.SetActive(false);
        m_RemoveWarning.SetActive(false);

        for (int i = 0; i < (int)Turret.End; ++i)
        {
            if (true == Player.Inst.CheckUnLock((Turret)i))
            {
                m_BuildButtons_Black[i].SetActive(false);
            }
        }

    }

    public void PopUpBuildInfos(GameObject ThisBuildButton)
    {
        int BuildButtonIdx = System.Array.IndexOf(m_BuildButtons, ThisBuildButton);

        m_BuildInfoScrolls[BuildButtonIdx].SetActive(true);

        if (true == Player.Inst.CheckUnLock((Turret)BuildButtonIdx))
        {
            m_BuildImages_Black[BuildButtonIdx].SetActive(false);
        }
        

    }

    public void PopUpBuildInfos2(GameObject ThisBuildButtonB)
    {
        int BuildButtonBIdx = System.Array.IndexOf(m_BuildButtons_Black, ThisBuildButtonB);

        m_BuildInfoScrolls[BuildButtonBIdx].SetActive(true);

        if (true == Player.Inst.CheckUnLock((Turret)BuildButtonBIdx))
        {
            m_BuildImages_Black[BuildButtonBIdx].SetActive(false);
        }

    }
    public void BuildInfosExit()
    {
        for (int i = 0; i < m_BuildInfoScrolls.Length; i++)
        {
            m_BuildInfoScrolls[i].SetActive(false);
            m_RemoveInfoScroll.SetActive(false);
        }
    }

    public void BuildInfosNext()
    {
        for (int i = 0; i < m_BuildInfoScrolls.Length; i++)
        {
            if (m_BuildInfoScrolls[i].activeSelf == true)
            {
                m_BuildInfoScrolls[i].SetActive(false);
                m_BuildInfoScrolls[i + 1].SetActive(true);

                if (true == Player.Inst.CheckUnLock((Turret)i))
                {
                    m_BuildImages_Black[i + 1].SetActive(false);
                }

                return;
            }

        }

    }

    public void BuildInfosPrev()
    {
        for (int i = 0; i < m_BuildInfoScrolls.Length; i++)
        {
            if (m_BuildInfoScrolls[i].activeSelf == true)
            {
                m_BuildInfoScrolls[i].SetActive(false);
                m_BuildInfoScrolls[i - 1].SetActive(true);

                if (true == Player.Inst.CheckUnLock((Turret)i))
                {
                    m_BuildImages_Black[i - 1].SetActive(false);
                }

                return;
            }

        }
    }

 public void PopUpBuildWarningNoBP()
    {
        m_BuildWarningNoBP.SetActive(true);
    }

    public void ExitWarnings()
    {
        m_BuildWarningNoBP.SetActive(false);
        m_BuildWarningAlready.SetActive(false);
        m_BuildWarningNoRsrc.SetActive(false);
        m_RemoveWarningYet.SetActive(false);
        m_RemoveWarning.SetActive(false);


        m_LabWarningNoBP.SetActive(false);
        m_LabWarningMax.SetActive(false);
        m_LabWarningNoRsrc.SetActive(false);
        m_RepairWarningAlready.SetActive(false);
        m_LaunchWarningNotRepaired.SetActive(false);
        m_LaunchWarning.SetActive(false);


        BuildInfosExit();
        LabInfosExit();
        RepairInfosExit();

    }

    public void PopUpBuildWarningAlready()
    {
        m_BuildWarningAlready.SetActive(true);
    }

    public void PopUpBuildWarningNoRsrc()
    {
        m_BuildWarningNoRsrc.SetActive(true);
    }
    public void PopUpRemoveInfo()
    {
        m_RemoveInfoScroll.SetActive(true);
    }

    public void RemoveInfotoBuild()
    {
        m_RemoveInfoScroll.SetActive(false);
        m_BuildInfoScrolls[23].SetActive(true);
    }

    public void BuildtoRemoveInfo()
    {
        m_BuildInfoScrolls[23].SetActive(false);
        m_RemoveInfoScroll.SetActive(true);
    }

    public void PopUpRemoveWarnings()
    {
        if (false == TurretMgr.Inst.CheckTurretOnTurretSupport())
        {
            m_RemoveWarningYet.SetActive(true);
        }

        else
        {
            m_RemoveWarning.SetActive(true);
        }
        
    }

    public void PopUpLaunchWarnings()
    {

        for (int i = 0; i < (int)SpaceShipPart.End; i++)
        {
            if (Player.Inst.m_spcPartInfos[i]._repaired == false)
            {
                m_LaunchWarningNotRepaired.SetActive(true);
                return;
            }

        }

        m_LaunchWarning.SetActive(true);
    }

    public void Launch()
    {
        PopUpExit();

        for (int i = 0; i < m_BrokenSpaceships.Length; i++)
        {
            Destroy(m_BrokenSpaceships[i]);
        }

        for (int i = 0; i < m_Rockets.Length; i++)
        {
            Destroy(m_Rockets[i]);
        }

        m_RepairedSpaceShip.SetActive(true);
    }

    // 추가
    public void ToEndingEscapeScene()
    {
        SceneManager.LoadScene("Ending_Escape");
    }

    public void BrokenSpaceshipImageChange()
    {
        if (Player.Inst.m_spcPartInfos[0]._repaired==true)
        {
            if (Player.Inst.m_spcPartInfos[1]._repaired == false)
            {
                m_BrokenSpaceships[0].SetActive(false);
                m_BrokenSpaceships[1].SetActive(true);
            }

        }

        if (Player.Inst.m_spcPartInfos[1]._repaired == true)
        {
            if(Player.Inst.m_spcPartInfos[0]._repaired == false)
            {
                m_BrokenSpaceships[0].SetActive(false);
                m_BrokenSpaceships[2].SetActive(true);
            }

            if (Player.Inst.m_spcPartInfos[0]._repaired == true)
            {
                m_BrokenSpaceships[1].SetActive(false);
                m_BrokenSpaceships[2].SetActive(false);
                m_BrokenSpaceships[3].SetActive(true);
            }
        }

        if (Player.Inst.m_spcPartInfos[2]._repaired == true)
        {
            m_Rockets[0].SetActive(true);
        }

        if (Player.Inst.m_spcPartInfos[3]._repaired == true)
        {
            m_Rockets[1].SetActive(true);
        }

        if (Player.Inst.m_spcPartInfos[4]._repaired == true)
        {
            m_Rockets[2].SetActive(true);
        }

    }

    public void OffTurretSupports(int num)
    {
        int max = num;

        if (max > 2)
            max = 2;

        for (int i = 0; i < 4; ++i)
        {
            for (int j = 3; j < 3 + max; ++j)
            {
                m_TurretSupports[i * 5 + j].SetActive(false);
            }
        }
    }

    public void UpdateTurretSupports(int num)
    {
        int max = num;

        if (max > 2)
            max = 2;

        for (int i = 0; i < 4; ++i)
        {
            for (int j = 3; j < 3 + 2; ++j)
            {
                m_TurretSupports[i * 5 + j].SetActive(false);
            }

            for (int j = 3; j < 3 + max; ++j)
            {
                m_TurretSupports[i * 5 + j].SetActive(true);
            }
        }
    }
}
