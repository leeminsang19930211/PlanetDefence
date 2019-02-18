using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class Player : MonoBehaviour
{
    private static Player m_inst = null;
    private int m_junk = 999;
    private int m_eleCircuit = 999;
    private int m_coin = 999;

    private TurretInfo[] m_turretInfos = new TurretInfo[(int)Turret.End];
    public LabInfo[] m_labInfos = new LabInfo[(int)Lab.End];
    public SpaceShipPartInfo[] m_spcPartInfos = new SpaceShipPartInfo[(int)SpaceShipPart.End];

    private int m_sourcePlanetMaxHP = 0;
    private int[] m_sourceTurretJunkCosts = { 10,20,30,40,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50};
    private GlobalData m_globalData = new GlobalData();
    private BulletData[] m_bulletDatas = null;
    private SpaceShipData[] m_spaceShipDatas = null;
    private TurretData[] m_turretDatas = null;
   
    private delegate void LabProc(int idx);
    LabProc[] m_labProcs = new LabProc[(int)Lab.End];

    public int[] TurretJunkCosts =
    {
        10,20,30,40,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50
    };
    public int[] TurretCircuitCosts =
    {
        1,2,3,4,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5
    };
    public int[] LabCoinCosts =
    {
        10,20,30,40,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50
    };
    public int[] LabMaxStacks =
    {
        2,5,5,5,5,5,1,1,5,5,5,5,5,5,2,2,5,5,5,5
    };
    public int[] RepairCoinCosts =
    {
        10,20,30,40,50
    };

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
        m_sourcePlanetMaxHP = PlanetCtrl.Inst.m_maxHP;

        if (FileMgr.Inst.PlayerReset== true)
        {
            SetUpNewDatas();
        }
        else
        {
            UpdateAllByLabInfos();
        }

        SetUpLabProcFuncs();

        UpdateRsrc();
    }

    public void Release_Clear()
    {

    }

    public void Release_Fail()
    {
        // TEMP
        m_junk = 999;
        m_eleCircuit = 999;
        m_coin = 999;

        for (int i = 0; i < m_labInfos.Length; ++i)
        {
            m_labInfos[i].stacks = 0;
        }

        m_globalData.planetHP = m_sourcePlanetMaxHP;
        TurretJunkCosts = (int[])m_sourceTurretJunkCosts.Clone();

        m_bulletDatas = BulletMgr.Inst.GetSourceBulletDatas();
        m_spaceShipDatas = SpaceShipMgr.Inst.GetSourceSpaceShipDatas();
        m_turretDatas = TurretMgr.Inst.GetSourceTurretDatas();

        UpdateRsrc();
       UpdateAllByLabInfos();
    }

    public bool SaveData(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        if(fileStream == null)
        {
            Debug.LogError("File open for saving player data failed");
            return false;
        }

        BinaryFormatter binFormatter = new BinaryFormatter();

        binFormatter.Serialize(fileStream, m_junk);
        binFormatter.Serialize(fileStream, m_eleCircuit);
        binFormatter.Serialize(fileStream, m_coin);
        binFormatter.Serialize(fileStream, m_turretInfos);
        binFormatter.Serialize(fileStream, m_labInfos);
        binFormatter.Serialize(fileStream, m_spcPartInfos);
        binFormatter.Serialize(fileStream, m_globalData);
        binFormatter.Serialize(fileStream, m_bulletDatas);
        binFormatter.Serialize(fileStream, m_spaceShipDatas);
        binFormatter.Serialize(fileStream, m_turretDatas);
        binFormatter.Serialize(fileStream, TurretJunkCosts);

        fileStream.Close();

        return true;
    }

    public bool LoadData(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open);

        if (fileStream == null)
        {
            Debug.LogError("File open for loading player data failed");
            return false;
        }

        BinaryFormatter binFormatter = new BinaryFormatter();

        m_junk = (int)binFormatter.Deserialize(fileStream);
        m_eleCircuit =(int)binFormatter.Deserialize(fileStream);
        m_coin = (int)binFormatter.Deserialize(fileStream);
        m_turretInfos = (TurretInfo[])binFormatter.Deserialize(fileStream);
        m_labInfos = (LabInfo[])binFormatter.Deserialize(fileStream);
        m_spcPartInfos = (SpaceShipPartInfo[])binFormatter.Deserialize(fileStream);
        m_globalData = (GlobalData)binFormatter.Deserialize(fileStream);
        m_bulletDatas = (BulletData[])binFormatter.Deserialize(fileStream);
        m_spaceShipDatas = (SpaceShipData[])binFormatter.Deserialize(fileStream);
        m_turretDatas = (TurretData[]) binFormatter.Deserialize(fileStream);
        TurretJunkCosts = (int[])binFormatter.Deserialize(fileStream);

        fileStream.Close();

        return true;
    }

    public BulletData GetBulletData(Bullet bullet)
    {
        return m_bulletDatas[(int)bullet];
    }

    public SpaceShipData GetSpaceShipData(MobType spaceShip)
    {
        return m_spaceShipDatas[(int)spaceShip];
    }

    public TurretData GetTurretData(Turret turret)
    {
        return m_turretDatas[(int)turret];
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

        if (junk > m_junk || eleCircuit > m_eleCircuit)
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

        // 터렛이 중복 설치되는 경우를 방지한다.
        Turret turretType = (Turret)BuildStartButtonIdx;

        if (turretType == Turret.Lv2_Shield || turretType == Turret.Lv3_Shield)
        {
            if(TurretMgr.Inst.FindShieldTurret() != null)
            {
                return BuyErr.DoubleShield;
            }
        }

        if (TurretJunkCosts[BuildStartButtonIdx] > m_junk || TurretCircuitCosts[BuildStartButtonIdx] > m_eleCircuit)
        {
            BattleGameObjectMgr.Inst.m_BuildWarningNoRsrc.SetActive(true);
            return BuyErr.NotEnoughRsrc;
        }

        m_junk -= TurretJunkCosts[BuildStartButtonIdx];
        m_eleCircuit -= TurretCircuitCosts[BuildStartButtonIdx];

        UpdateRsrc();

        TurretMgr.Inst.CreateTurretOnTurretSupport((Turret)BuildStartButtonIdx);
        BattleGameObjectMgr.Inst.BuildInfosExit();
        BattleGameObjectMgr.Inst.PopUpExit();

        return BuyErr.NoError;
    }

    public bool SellTurret()
    {
        TurretCtrl turret = TurretMgr.Inst.FocusedTurret;

        if (turret == null)
        {
            BattleGameObjectMgr.Inst.m_RemoveWarningYet.SetActive(true);
            return false;
        }

        if (turret.TurretType == Turret.End)
        {
            Debug.LogError("The turretType in turretCtrl is turret.End");
            return false;
        }

        if (turret.m_maxHP == 0)
        {
            Debug.LogError("The turret maxHP in turretCtrl is zero");
            return false;
        }

        // 수정

        if (Player.Inst.m_labInfos[7]._lock == true)
        {
            int turretIdx = (int)turret.TurretType;
            float ratio = turret.CurHP / (float)turret.m_maxHP;

            m_junk += (int)(TurretJunkCosts[turretIdx] * ratio);
            m_eleCircuit += (int)(TurretCircuitCosts[turretIdx] * ratio);

            UpdateRsrc();
        }

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

    public LabErr BuyLab(GameObject ThisLabStartButton)
    {
        int LabStartButtonIdx = System.Array.IndexOf(BattleGameObjectMgr.Inst.m_LabStartButtons, ThisLabStartButton);

        if (LabStartButtonIdx >= 12 && LabStartButtonIdx < 16)
        {
            if (false == Player.Inst.CheckUnLock((Turret)LabStartButtonIdx - 4))
            {
                BattleGameObjectMgr.Inst.m_LabWarningNoBP.SetActive(true);
                return LabErr.NoBP;
            }
        }

        else if (LabStartButtonIdx >= 16 && LabStartButtonIdx < BattleGameObjectMgr.Inst.m_LabStartButtons.Length)
        {
            if (false == Player.Inst.CheckUnLock((Turret)LabStartButtonIdx + 4))
            {
                BattleGameObjectMgr.Inst.m_LabWarningNoBP.SetActive(true);
                return LabErr.NoBP;
            }
        }

        if (m_labInfos[LabStartButtonIdx].stacks >= LabMaxStacks[LabStartButtonIdx])
        {
            BattleGameObjectMgr.Inst.m_LabWarningMax.SetActive(true);
            return LabErr.Max;
        }


        if (LabCoinCosts[LabStartButtonIdx] > m_coin)
        {
            BattleGameObjectMgr.Inst.m_LabWarningNoRsrc.SetActive(true);
            return LabErr.NotEnoughRsrc;

        }

        else
        {
            m_coin -= LabCoinCosts[LabStartButtonIdx];
            UpdateRsrc();
            m_labInfos[LabStartButtonIdx].stacks += 1;

            // 추가
            BattleGameObjectMgr.Inst.LabInfosExit();
            m_labProcs[LabStartButtonIdx](LabStartButtonIdx);
            // TEMP
            return LabErr.NoError;
        }
    }

    public RepairErr BuyRepair(GameObject ThisRepairStartButton)
    {
        int RepairStartButtonIdx = System.Array.IndexOf(BattleGameObjectMgr.Inst.m_RepairStartButtons, ThisRepairStartButton);

        if (false == Player.Inst.CheckUnLock((SpaceShipPart)RepairStartButtonIdx))
        {
            BattleGameObjectMgr.Inst.m_LabWarningNoBP.SetActive(true);
            return RepairErr.NoBP;
        }

        if (true == m_spcPartInfos[RepairStartButtonIdx]._repaired)
        {
            BattleGameObjectMgr.Inst.m_RepairWarningAlready.SetActive(true);
            return RepairErr.AlreadyRepaired;
        }

        if (RepairCoinCosts[RepairStartButtonIdx] > m_coin)
        {
            BattleGameObjectMgr.Inst.m_LabWarningNoRsrc.SetActive(true);
            return RepairErr.NotEnoughRsrc;
        }

        else
        {
            m_coin -= RepairCoinCosts[RepairStartButtonIdx];
            UpdateRsrc();
            m_spcPartInfos[RepairStartButtonIdx]._repaired = true;

            // 추가
            BattleGameObjectMgr.Inst.RepairInfosExit();

            return RepairErr.NoError;

        }
    }

    private void LabProc_IncTurretSupports(int idx)
    {
        BattleGameObjectMgr.Inst.UpdateTurretSupports(m_labInfos[idx].stacks);
    }

    private void LabProc_IncTurretHelth(int idx)
    {
        TurretData[] sourceDatas = TurretMgr.Inst.GetSourceTurretDatas();

        for(int i=0; i<(int)Turret.End; ++i)
        {
            if (sourceDatas[i] == null)
                continue;

            m_turretDatas[i].maxHP = sourceDatas[i].maxHP + (int)(sourceDatas[i].maxHP * m_labInfos[idx].stacks * 0.1f);
        }

        TurretMgr.Inst.UpdateTurretDatas();
    }

    private void LabProc_IncPlanetHelth(int idx)
    {
        m_globalData.planetHP = m_sourcePlanetMaxHP + (int)(m_sourcePlanetMaxHP * m_labInfos[idx].stacks * 0.1f);

        PlanetCtrl.Inst.MaxHP = m_globalData.planetHP;
    }

    private void LabProc_DecSpaceShipHelth(int idx)
    {
        SpaceShipData[] sourceDatas = SpaceShipMgr.Inst.GetSourceSpaceShipDatas();

        for(int i=0; i<(int)MobType.End; ++i)
        {
            if (sourceDatas[i] == null)
                continue;

            m_spaceShipDatas[i].maxHP = sourceDatas[i].maxHP - (int)(sourceDatas[i].maxHP * m_labInfos[idx].stacks * 0.1f);
        }
    }

    private void LabProc_IncJunkDrops(int idx)
    {
        SpaceShipData[] sourceDatas = SpaceShipMgr.Inst.GetSourceSpaceShipDatas();

        for (int i = 0; i < (int)MobType.End; ++i)
        {
            if (sourceDatas[i] == null)
                continue;

            m_spaceShipDatas[i].junkDrops = sourceDatas[i].junkDrops + (int)(sourceDatas[i].junkDrops * m_labInfos[idx].stacks * 0.1f);
        }
    }

    private void LabProc_IncCoinDrops(int idx)
    {
        SpaceShipData[] sourceDatas = SpaceShipMgr.Inst.GetSourceSpaceShipDatas();

        for (int i = 0; i < (int)MobType.End; ++i)
        {
            if (sourceDatas[i] == null)
                continue;

            m_spaceShipDatas[i].coinDrops = sourceDatas[i].coinDrops + (int)(sourceDatas[i].coinDrops * m_labInfos[idx].stacks * 0.1f);
        }
    }

    private void LabProc_DecJunkConsumtion(int idx)
    {
        for(int i=0; i<TurretJunkCosts.Length; ++i)
        {
            TurretJunkCosts[i] = m_sourceTurretJunkCosts[i] - (int)(m_sourceTurretJunkCosts[i] * m_labInfos[(int)Lab.DecJunkConsumtion].stacks * 0.1f);
        }
    }

    private void LabProc_ReturnTurretRsrc(int idx)
    {

    }

    private void LabProc_IncGatlingDamage(int idx)
    {
        BulletData sourceData0 = (BulletData)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv1_Gatling);
        BulletData sourceData1 = (BulletData)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv2_Gatling);
        BulletData sourceData2 = (BulletData)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv3_Gatling);

        m_bulletDatas[(int)Bullet.Lv1_Gatling].damage = sourceData0.damage + (int)(sourceData0.damage * m_labInfos[idx].stacks * 0.1f);
        m_bulletDatas[(int)Bullet.Lv2_Gatling].damage = sourceData1.damage + (int)(sourceData1.damage * m_labInfos[idx].stacks * 0.1f);
        m_bulletDatas[(int)Bullet.Lv3_Gatling].damage = sourceData2.damage + (int)(sourceData2.damage * m_labInfos[idx].stacks * 0.1f);
    }

    private void LabProc_IncFastFireSpeed(int idx)
    {
        TurretData_Fast sourceData0 = (TurretData_Fast)TurretMgr.Inst.GetSourceTurretData(Turret.Lv1_Fast);
        TurretData_Fast sourceData1 = (TurretData_Fast)TurretMgr.Inst.GetSourceTurretData(Turret.Lv2_Fast);
        TurretData_Fast sourceData2 = (TurretData_Fast)TurretMgr.Inst.GetSourceTurretData(Turret.Lv3_Fast);

        ((TurretData_Fast)m_turretDatas[(int)Turret.Lv1_Fast]).fireDelay = sourceData0.fireDelay - (sourceData0.fireDelay * m_labInfos[idx].stacks * 0.1f);
        ((TurretData_Fast)m_turretDatas[(int)Turret.Lv2_Fast]).fireDelay = sourceData1.fireDelay - (sourceData1.fireDelay * m_labInfos[idx].stacks * 0.1f);
        ((TurretData_Fast)m_turretDatas[(int)Turret.Lv3_Fast]).fireDelay = sourceData2.fireDelay - (sourceData2.fireDelay * m_labInfos[idx].stacks * 0.1f);
                                                                                                 
        TurretMgr.Inst.UpdateTurretDatas();
    }

    private void LabProc_IncSplashRange(int idx)
    {
        BulletData_Missile sourceData0 = (BulletData_Missile)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv1_Missile);
        BulletData_Missile sourceData1 = (BulletData_Missile)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv2_Missile);
        BulletData_Missile sourceData2 = (BulletData_Missile)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv3_Missile);

        ((BulletData_Missile)m_bulletDatas[(int)Bullet.Lv1_Missile]).splashRange = sourceData0.splashRange + (sourceData0.splashRange * m_labInfos[idx].stacks * 0.1f);
        ((BulletData_Missile)m_bulletDatas[(int)Bullet.Lv2_Missile]).splashRange = sourceData1.splashRange + (sourceData1.splashRange * m_labInfos[idx].stacks * 0.1f);
        ((BulletData_Missile)m_bulletDatas[(int)Bullet.Lv3_Missile]).splashRange = sourceData2.splashRange + (sourceData2.splashRange * m_labInfos[idx].stacks * 0.1f);
    }

    private void LabProc_IncLaserDuration(int idx)
    {
        BulletData_Laser sourceData0 = (BulletData_Laser)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv1_Laser);
        BulletData_Laser sourceData1 = (BulletData_Laser)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv2_Laser);
        BulletData_Laser sourceData2 = (BulletData_Laser)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv3_Laser);

        ((BulletData_Laser)m_bulletDatas[(int)Bullet.Lv1_Laser]).duration = sourceData0.duration + (sourceData0.duration * m_labInfos[idx].stacks * 0.1f);
        ((BulletData_Laser)m_bulletDatas[(int)Bullet.Lv2_Laser]).duration = sourceData1.duration + (sourceData1.duration * m_labInfos[idx].stacks * 0.1f);
        ((BulletData_Laser)m_bulletDatas[(int)Bullet.Lv3_Laser]).duration = sourceData2.duration + (sourceData2.duration * m_labInfos[idx].stacks * 0.1f);
    }

    private void LabProc_DecShieldHitDamage(int idx)
    {
        TurretData_Shield sourceData0 = (TurretData_Shield)TurretMgr.Inst.GetSourceTurretData(Turret.Lv2_Shield);
        TurretData_Shield sourceData1 = (TurretData_Shield)TurretMgr.Inst.GetSourceTurretData(Turret.Lv3_Shield);

        ((TurretData_Fast)m_turretDatas[(int)Turret.Lv2_Shield]).fireDelay = sourceData0.hitDamageScale - (sourceData0.hitDamageScale * m_labInfos[idx].stacks * 0.1f);
        ((TurretData_Fast)m_turretDatas[(int)Turret.Lv3_Shield]).fireDelay = sourceData1.hitDamageScale - (sourceData1.hitDamageScale * m_labInfos[idx].stacks * 0.1f);

        TurretMgr.Inst.UpdateTurretDatas();
    }

    private void LabProc_IncPoisonDotDamage(int idx)
    {
        BulletData_Poison sourceData0 = (BulletData_Poison)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv2_Poison);
        BulletData_Poison sourceData1 = (BulletData_Poison)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv3_Poison);

        ((BulletData_Poison)m_bulletDatas[(int)Bullet.Lv2_Poison]).dotDamage = sourceData0.dotDamage + (int)(sourceData0.dotDamage * m_labInfos[idx].stacks * 0.1f);
        ((BulletData_Poison)m_bulletDatas[(int)Bullet.Lv3_Poison]).dotDamage = sourceData1.dotDamage + (int)(sourceData1.dotDamage * m_labInfos[idx].stacks * 0.1f);
    }

    private void LabProc_IncSlowDuration(int idx)
    {
        BulletData_Slow sourceData0 = (BulletData_Slow)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv2_Slow);
        BulletData_Slow sourceData1 = (BulletData_Slow)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv3_Slow);

        ((BulletData_Slow)m_bulletDatas[(int)Bullet.Lv2_Slow]).duration = sourceData0.duration + sourceData0.duration * m_labInfos[idx].stacks * 1;
        ((BulletData_Slow)m_bulletDatas[(int)Bullet.Lv3_Slow]).duration = sourceData1.duration + sourceData1.duration * m_labInfos[idx].stacks * 1;
    }

    private void LabProc_IncPauseDuration(int idx)
    {
        BulletData_Pause sourceData0 = (BulletData_Pause)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv2_Pause);
        BulletData_Pause sourceData1 = (BulletData_Pause)BulletMgr.Inst.GetSourceBulletData(Bullet.Lv3_Pause);

        ((BulletData_Pause)m_bulletDatas[(int)Bullet.Lv2_Pause]).duration = sourceData0.duration + sourceData0.duration * m_labInfos[idx].stacks * 1;
        ((BulletData_Pause)m_bulletDatas[(int)Bullet.Lv3_Pause]).duration = sourceData1.duration + sourceData1.duration * m_labInfos[idx].stacks * 1;
    }

    private void LabProc_IncSniperFireSpeed(int idx)
    {
        TurretData_Sniper sourceData0 = (TurretData_Sniper)TurretMgr.Inst.GetSourceTurretData(Turret.Lv3_Sniper);

        ((TurretData_Sniper)m_turretDatas[(int)Turret.Lv3_Sniper]).fireDelay = sourceData0.fireDelay - (sourceData0.fireDelay * m_labInfos[idx].stacks * 0.1f);
  
        TurretMgr.Inst.UpdateTurretDatas();
    }

    private void LabProc_IncHealAmount(int idx)
    {
        BulletData sourceData0 = BulletMgr.Inst.GetSourceBulletData(Bullet.Lv3_Heal);

        (m_bulletDatas[(int)Bullet.Lv3_Heal]).damage = sourceData0.damage + m_labInfos[idx].stacks * 10;

        if(m_labInfos[idx].stacks == 1)
        {
            TurretData_Heal sourceData = (TurretData_Heal)TurretMgr.Inst.GetSourceTurretData(Turret.Lv3_Heal);

            ((TurretData_Heal)(m_turretDatas[(int)Turret.Lv3_Heal])).fire = true;

            TurretMgr.Inst.UpdateTurretDatas();
        }
    }

    private void LabProc_incBerserkerDamage(int idx)
    {
        BulletData sourceData0 = BulletMgr.Inst.GetSourceBulletData(Bullet.Lv3_Berserker);

        (m_bulletDatas[(int)Bullet.Lv3_Berserker]).damage = sourceData0.damage + (int)(sourceData0.damage * m_labInfos[idx].stacks * 0.1f);
    }

    private void LabProc_IncKingSlayerFireSpeed(int idx)
    {

    }

    private void UpdateAllByLabInfos()
    {
        LabProc_IncTurretSupports((int)Lab.IncTurretSupports);
        LabProc_IncPlanetHelth((int)Lab.IncPlanetHelth);
    }

    private void SetUpNewDatas()
    {
        for (int i = 0; i < (int)Turret.End; ++i)
        {
            m_turretInfos[i]._lock = false;
        }

        for (int i = 0; i < (int)Lab.End; ++i)
        {
            m_labInfos[i]._lock = false;
            m_labInfos[i].stacks = 0;
        }

        for (int i = 0; i < (int)SpaceShipPart.End; i++)
        {
            m_spcPartInfos[i]._repaired = false;
        }
        
        m_globalData.planetHP = PlanetCtrl.Inst.m_maxHP;
        m_bulletDatas = BulletMgr.Inst.GetSourceBulletDatas();
        m_spaceShipDatas = SpaceShipMgr.Inst.GetSourceSpaceShipDatas();
        m_turretDatas = TurretMgr.Inst.GetSourceTurretDatas();
    }

    private void SetUpLabProcFuncs()
    {
        m_labProcs[(int)Lab.IncTurretSupports] = LabProc_IncTurretSupports;
        m_labProcs[(int)Lab.IncTurretHelth] = LabProc_IncTurretHelth;
        m_labProcs[(int)Lab.IncPlanetHelth] = LabProc_IncPlanetHelth;
        m_labProcs[(int)Lab.DecSpaceShipHelth] = LabProc_DecSpaceShipHelth;
        m_labProcs[(int)Lab.IncJunkDrops] = LabProc_IncJunkDrops;
        m_labProcs[(int)Lab.IncCoinDrops] = LabProc_IncCoinDrops;
        m_labProcs[(int)Lab.DecJunkConsumtion] = LabProc_DecJunkConsumtion;
        m_labProcs[(int)Lab.ReturnTurretRsrc] = LabProc_ReturnTurretRsrc;

        m_labProcs[(int)Lab.IncGatlingDamage] = LabProc_IncGatlingDamage;
        m_labProcs[(int)Lab.IncFastFireSpeed] = LabProc_IncFastFireSpeed;
        m_labProcs[(int)Lab.IncSplashRange] = LabProc_IncSplashRange;
        m_labProcs[(int)Lab.IncLaserDuration] = LabProc_IncLaserDuration;
        m_labProcs[(int)Lab.DecShieldHitDamage] = LabProc_DecShieldHitDamage;
        m_labProcs[(int)Lab.IncPoisonDotDamage] = LabProc_IncPoisonDotDamage;
        m_labProcs[(int)Lab.IncSlowDuration] = LabProc_IncSlowDuration;
        m_labProcs[(int)Lab.IncPauseDuration] = LabProc_IncPauseDuration;
        m_labProcs[(int)Lab.IncSniperFireSpeed] = LabProc_IncSniperFireSpeed;
        m_labProcs[(int)Lab.IncHealAmount] = LabProc_IncHealAmount;
        m_labProcs[(int)Lab.incBerserkerDamage] = LabProc_incBerserkerDamage;
        m_labProcs[(int)Lab.IncKingSlayerFireSpeed] = LabProc_IncKingSlayerFireSpeed;
    }
}
