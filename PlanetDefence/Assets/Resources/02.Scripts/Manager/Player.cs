using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private static Player m_inst = null;
    private int m_junk = 999    ;
    private int m_eleCircuit = 999;
    private int m_coin = 999;

    private TurretInfo[] m_turretInfos = new TurretInfo[(int)Turret.End];

    // 수정
    public LabInfo[] m_labInfos = new LabInfo[(int)Lab.End];
    public SpaceShipPartInfo[] m_spcPartInfos = new SpaceShipPartInfo[(int)SpaceShipPart.End];

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
       for(int i= 0; i<(int)Lab.End; ++i)
       {
            m_labInfos[i].stacks = 0;
       }

       // 추가
       for(int i=0;i<(int)SpaceShipPart.End;i++)
        {
            m_spcPartInfos[i]._repaired = false;
        }

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

    // ★ 이거 사용
    public BuyErr BuyTurret(GameObject ThisBuildStartButton)
    {
        int BuildStartButtonIdx = System.Array.IndexOf(BattleGameObjectMgr.Inst.m_BuildStartButtons, ThisBuildStartButton);

        if (false == Player.Inst.CheckUnLock((Turret)BuildStartButtonIdx))
        {
            BattleGameObjectMgr.Inst.m_BuildWarningNoBP.SetActive(true);
            return BuyErr.NoBP;
        }
        if (true == TurretMgr.Inst.CheckTurretOnTurretSupport())
        {
            BattleGameObjectMgr.Inst.m_BuildWarningAlready.SetActive(true);
            return BuyErr.AlreadySetUp;
        }
        if (TurretMgr.Inst.TurretJunkCosts[BuildStartButtonIdx] > m_junk || TurretMgr.Inst.TurretCircuitCosts[BuildStartButtonIdx] > m_eleCircuit)
        {
            BattleGameObjectMgr.Inst.m_BuildWarningNoRsrc.SetActive(true);
            return BuyErr.NotEnoughRsrc;
        }

        m_junk -= TurretMgr.Inst.TurretJunkCosts[BuildStartButtonIdx];
        m_eleCircuit -= TurretMgr.Inst.TurretCircuitCosts[BuildStartButtonIdx];

        UpdateRsrc();

        TurretMgr.Inst.CreateTurretOnTurretSupport((Turret)BuildStartButtonIdx);
        BattleGameObjectMgr.Inst.BuildInfosExit();
        BattleGameObjectMgr.Inst.PopUpExit();

        return BuyErr.NoError;
    }

    // ★ 이거 사용  <-- 변경
    public bool SellTurret()
    {
        TurretCtrl turret = TurretMgr.Inst.FocusedTurret;

        if (turret == null)
        {
            BattleGameObjectMgr.Inst.m_RemoveWarningYet.SetActive(true);
            return false;
        }
            
        if(turret.TurretType == Turret.End)
        {
            Debug.LogError("The turretType in turretCtrl is turret.End");
            return false;
        }

        if (turret.m_maxHP == 0)
        {
            Debug.LogError("The turret maxHP in turretCtrl is zero");
            return false;
        }

        int turretIdx = (int)turret.TurretType;
        float ratio = turret.CurHP / (float)turret.m_maxHP;

        m_junk += (int)(TurretMgr.Inst.TurretJunkCosts[turretIdx] * ratio);
        m_eleCircuit += (int)(TurretMgr.Inst.TurretCircuitCosts[turretIdx] * ratio);

        UpdateRsrc();

        TurretMgr.Inst.RemoveTurretOnTurretSupport();
        BattleGameObjectMgr.Inst.ExitWarnings();
        BattleGameObjectMgr.Inst.PopUpExit();

        return true;
    }

    private void UpdateRsrc()
    {
        BattleGameObjectMgr.Inst.UpdateJunkCnt(m_junk);
        BattleGameObjectMgr.Inst.UpdateEleCircuitCnt(m_eleCircuit);
        BattleGameObjectMgr.Inst.UpdateCoinCnt(m_coin);
    }

    // -------- Lab ----------

    

    public LabErr BuyLab(GameObject ThisLabStartButton)
    {
        int LabStartButtonIdx = System.Array.IndexOf(BattleGameObjectMgr.Inst.m_LabStartButtons, ThisLabStartButton);

        if (LabStartButtonIdx>=12 && LabStartButtonIdx < 16)
        {
            if (false == Player.Inst.CheckUnLock((Turret)LabStartButtonIdx - 4))
            {
                BattleGameObjectMgr.Inst.m_LabWarningNoBP.SetActive(true);
                return LabErr.NoBP;
            }
        }

        else if(LabStartButtonIdx>=16 && LabStartButtonIdx < BattleGameObjectMgr.Inst.m_LabStartButtons.Length)
        {
            if (false == Player.Inst.CheckUnLock((Turret)LabStartButtonIdx + 4))
            {
                BattleGameObjectMgr.Inst.m_LabWarningNoBP.SetActive(true);
                return LabErr.NoBP;
            }
        }

        if(m_labInfos[LabStartButtonIdx].stacks>=TurretMgr.Inst.LabMaxStacks[LabStartButtonIdx])
        {
            BattleGameObjectMgr.Inst.m_LabWarningMax.SetActive(true);
            return LabErr.Max;
        }



        if (TurretMgr.Inst.LabCoinCosts[LabStartButtonIdx]>m_coin)
        {
            BattleGameObjectMgr.Inst.m_LabWarningNoRsrc.SetActive(true);
            return LabErr.NotEnoughRsrc;

        }

        else
        {
            m_coin -= TurretMgr.Inst.LabCoinCosts[LabStartButtonIdx];
            UpdateRsrc();
            m_labInfos[LabStartButtonIdx].stacks += 1;

            // 추가
            BattleGameObjectMgr.Inst.LabInfosExit();

            TurretMgr.Inst.LabTempFunc(LabStartButtonIdx);
            return LabErr.NoError;
        }
    }

    // ------------ Repair -------------

    public RepairErr BuyRepair(GameObject ThisRepairStartButton)
    {
        int RepairStartButtonIdx = System.Array.IndexOf(BattleGameObjectMgr.Inst.m_RepairStartButtons, ThisRepairStartButton);

        if (false == Player.Inst.CheckUnLock((SpaceShipPart)RepairStartButtonIdx))
        {
            BattleGameObjectMgr.Inst.m_LabWarningNoBP.SetActive(true);
            return RepairErr.NoBP;
        }

        if(true==m_spcPartInfos[RepairStartButtonIdx]._repaired)
        {
            BattleGameObjectMgr.Inst.m_RepairWarningAlready.SetActive(true);
            return RepairErr.AlreadyRepaired;
        }

        if (TurretMgr.Inst.RepairCoinCosts[RepairStartButtonIdx] > m_coin)
        {
            BattleGameObjectMgr.Inst.m_LabWarningNoRsrc.SetActive(true);
            return RepairErr.NotEnoughRsrc;

        }

        else
        {
            m_coin -= TurretMgr.Inst.RepairCoinCosts[RepairStartButtonIdx];
            UpdateRsrc();
            m_spcPartInfos[RepairStartButtonIdx]._repaired = true;

            // 추가
            BattleGameObjectMgr.Inst.RepairInfosExit();

            return RepairErr.NoError;

        }
    }

}
