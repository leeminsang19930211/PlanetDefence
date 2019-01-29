using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /* 여기서 부터 필요한 내용들을 작성하면 된다 */
    private MainCameraCtrl m_mainCameraCtrl = null;
    private EnemyCntCtrl m_enemyCntCtrl = null;
    private PlanetHPCtrl m_planetHpCtrl = null;
    private ResourceCtrl m_resourceCtrl = null;
    private MiniPlanetCtrl m_miniPlanetCtrl = null;
    private Transform m_planetTransform = null;

    private GameObject m_laboratoryPopUp = null;
    private GameObject m_labScroll = null;
    private GameObject m_repairScroll = null;
    private GameObject m_labInfoScroll = null;
    private GameObject m_rightArrow = null;
    private GameObject m_leftArrow = null;

    public Vector3 PlanetPos
    {
        get
        {
           return  m_planetTransform.position;
        }
    }

    public void Init()
    {      
        m_mainCameraCtrl = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<MainCameraCtrl>();
        m_enemyCntCtrl = GameObject.FindGameObjectWithTag("ENEMY_CNT")?.GetComponent<EnemyCntCtrl>();
        m_planetHpCtrl = GameObject.FindGameObjectWithTag("PLANET_HP")?.GetComponent<PlanetHPCtrl>();
        m_resourceCtrl = GameObject.FindGameObjectWithTag("RESOURCE")?.GetComponent<ResourceCtrl>();
        m_miniPlanetCtrl = GameObject.FindGameObjectWithTag("MINIPLANET")?.GetComponent<MiniPlanetCtrl>();
        m_planetTransform = GameObject.FindGameObjectWithTag("PLANET")?.GetComponent<Transform>();
    }

    public void Start()
    {
        m_laboratoryPopUp = GameObject.FindGameObjectWithTag("LABORATORY_POPUP");
        m_labScroll = GameObject.Find("LabScroll");
        m_repairScroll = GameObject.Find("RepairScroll");
        m_labInfoScroll = GameObject.Find("LabInfoScroll");
        m_rightArrow = GameObject.Find("RightArrow");
        m_leftArrow = GameObject.Find("LeftArrow");

        m_laboratoryPopUp.SetActive(false);
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
        m_miniPlanetCtrl.RotateToTarget(targetDir);
    }

    public void UpdateEnemyCnt(int maxEnemyCnt, int destroyedEnemyCnt)
    {
        m_enemyCntCtrl.MaxEnemyCnt = maxEnemyCnt;
        m_enemyCntCtrl.DestroyedEnemyCnt = destroyedEnemyCnt;
    }

    public void UpdatePlanetHP(int maxHP, int curHP)
    {
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
        m_labInfoScroll.SetActive(false);
    }

    public void PopUpRepair()
    {
        m_labScroll.SetActive(false);
        m_repairScroll.SetActive(true);
        m_labInfoScroll.SetActive(false);
    }

    public void PopUpExit()
    {
        m_laboratoryPopUp.SetActive(false);
        m_rightArrow.SetActive(true);
        m_leftArrow.SetActive(true);
    }

    public void PopUpLabInfo()
    {
        m_labInfoScroll.SetActive(true);
    }

    public void PopUpLabInfoExit()
    {
        m_labInfoScroll.SetActive(false);
    }
}
