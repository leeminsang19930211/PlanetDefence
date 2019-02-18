using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCtrl: MonoBehaviour
{
    private void Awake()
    {
        SceneLoader.OnStartScene();

        InitManagers();

        InitBattleScene();
    }

    public static void Release_Clear()
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

    public static void Release_Fail()
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

    private void InitManagers()
    {
        BattleGameObjectMgr.Inst.Init();
        BulletMgr.Inst.Init();
        EffectMgr.Inst.Init();
        TurretMgr.Inst.Init();
        SpaceShipMgr.Inst.Init();
        Player.Inst.Init();
    }

    private void InitBattleScene()
    {
        if (GlobalGameObjectMgr.Inst.Battle == false)
        {
            GlobalGameObjectMgr.Inst.Battle = true;

            WavesMob[] infos = new WavesMob[1];

            infos[0].eMobType = MobType.Normal;
            infos[0].nMobNum = 2;
            infos[0].fDelayTime = 4f;
            infos[0].fFirstDelayTime = 0f;

            //infos[1].eMobType = MobType.Kamikaze;
            //infos[1].nMobNum = 10;
            //infos[1].fDelayTime = 3f;
            //infos[1].fFirstDelayTime = 3f;

            //infos[2].eMobType = MobType.DummyShip;
            //infos[2].nMobNum = 10;
            //infos[2].fDelayTime = 6f;
            //infos[2].fFirstDelayTime = 6f;

            //infos[3].eMobType = MobType.Pirate;
            //infos[3].nMobNum = 10;
            //infos[3].fDelayTime = 4f;
            //infos[3].fFirstDelayTime = 9f;

            //infos[4].eMobType = MobType.Little;
            //infos[4].nMobNum = 10;
            //infos[4].fDelayTime = 2f;
            //infos[4].fFirstDelayTime = 2f;

            //infos[5].eMobType = MobType.ZombieShip;
            //infos[5].nMobNum = 10;
            //infos[5].fDelayTime = 5f;
            //infos[5].fFirstDelayTime = 2f;

            //infos[6].eMobType = MobType.GhostShip;
            //infos[6].nMobNum = 10;
            //infos[6].fDelayTime = 3f;
            //infos[6].fFirstDelayTime = 5f;

            //infos[7].eMobType = MobType.BattleShip;
            //infos[7].nMobNum = 10;
            //infos[7].fDelayTime = 4f;
            //infos[7].fFirstDelayTime = 2f;

            int spaceShipsNum = 0;

            foreach (WavesMob info in infos)
            {
                // TEMP : 생성될 자식까지 포함하여 풀을 생성해야해서 임시로 해놓음
                if (info.eMobType == MobType.BattleShip)
                {
                    spaceShipsNum += info.nMobNum * 10;
                }
                else
                {
                    spaceShipsNum += info.nMobNum;
                }
            }

            BulletMgr.Inst.AllocateBulletPool(BulletPool.Turret, 20);
            BulletMgr.Inst.AllocateBulletPool(BulletPool.SpaceShip, spaceShipsNum);
            EffectMgr.Inst.AllocateEffectPool(EffectPool.Turret, 20);
            EffectMgr.Inst.AllocateEffectPool(EffectPool.SpaceShip, spaceShipsNum);

            TurretMgr.Inst.OnNewBattle();

            SpaceShipMgr.Inst.StartCreatingWaves(infos);
        }
    }
}
