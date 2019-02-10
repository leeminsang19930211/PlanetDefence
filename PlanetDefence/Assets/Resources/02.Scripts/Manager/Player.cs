using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private static Player m_inst = null;
    private int m_junk = 0;
    private int m_eleCircuit = 0;
    private int m_coin = 0;

    private TurretInfo[] m_turretInfos = new TurretInfo[(int)Turret.End];
    private LabInfo[] m_labInfos = new LabInfo[(int)Lab.End];
    private SpaceShipPartInfo[] m_spcPartInfos = new SpaceShipPartInfo[(int)SpaceShipPart.End];

    public static Player Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "Player";
                m_inst = container.AddComponent<Player>() as Player;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }

    public void Init()
    {
        UpdateRsrc();
    }

    public void Release_Clear()
    {

    }

    public void Release_Fail()
    {
        m_junk = 0;
        m_eleCircuit = 0;
        m_coin = 0;

        UpdateRsrc();
    }

    public void AddJunk(int junk)
    {
        m_junk += junk;
        BattleGameObjectMgr.Inst.UpdateJunkCnt(m_junk);
    }

    public void AddEleCircuit(int eleCircuit)
    {
        m_eleCircuit += eleCircuit;
        BattleGameObjectMgr.Inst.UpdateEleCircuitCnt(m_eleCircuit);
    }

    public void AddCoin(int coin)
    {
        m_coin += coin;
        BattleGameObjectMgr.Inst.UpdateCoinCnt(m_coin);
    }

    /* 해금여부를 반환 하는 함수. 해금되있으면 true 반환*/
    public bool CheckUnLock(Turret turret)
    {
        return !m_turretInfos[(int)turret]._lock;
    }

    public bool CheckUnLock(Lab lab)
    {
        return !m_labInfos[(int)lab]._lock;
    }

    public bool CheckUnLock(SpaceShipPart spcPart)
    {
        return !m_spcPartInfos[(int)spcPart]._lock;
    }

    // 연구 누적값을 반환한다 
    public int GetLabStacks(Lab lab)
    {
        return m_labInfos[(int)lab].stacks;
    }

    // 터렛을 구매하고 생성한다. 포탑이 설치되어있는지 부터 체크한다.
    public BuyErr BuyTurret(Turret turret, int junk, int eleCircuit)
    {
        if (true == TurretMgr.Inst.CheckTurretOnTurretSupport())
            return BuyErr.AlreadySetUp;

        if(junk > m_junk || eleCircuit > m_eleCircuit)
            return BuyErr.NotEnoughRsrc;

        m_junk -= junk;
        m_eleCircuit -= eleCircuit;

        UpdateRsrc();

        TurretMgr.Inst.CreateTurretOnTurretSupport(turret);

        return BuyErr.NoError;
    }

    // 터렛을 판매하고 삭제한다. 삭제할 터렛이 없을경우 false 리턴한다.
    public bool SellTurret(int junk, int eleCircuit)
    {
        if (false == TurretMgr.Inst.CheckTurretOnTurretSupport())
            return false;

        m_junk += junk;
        m_eleCircuit += eleCircuit;

        UpdateRsrc();

        TurretMgr.Inst.RemoveTurretOnTurretSupport();

        return true;
    }

    private void UpdateRsrc()
    {
        BattleGameObjectMgr.Inst.UpdateJunkCnt(m_junk);
        BattleGameObjectMgr.Inst.UpdateEleCircuitCnt(m_eleCircuit);
        BattleGameObjectMgr.Inst.UpdateCoinCnt(m_coin);
    }

}
