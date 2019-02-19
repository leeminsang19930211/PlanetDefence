using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCtrl: MonoBehaviour
{
    private static bool m_onStart = false;

    private void Awake()
    {
        SceneLoader.OnStartScene();

        _OnStart();
        _OnNewBattle();
        _OnBattle();
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

    private void _OnStart()
    {
        if(m_onStart == false)
        {
            BattleGameObjectMgr.Inst._OnStart();
            BulletMgr.Inst._OnStart();
            EffectMgr.Inst._OnStart();
            TurretMgr.Inst._OnStart();
            SpaceShipMgr.Inst._OnStart();
            Player.Inst._OnStart();

            m_onStart = true;
        } 
    }

    private void _OnNewBattle()
    {
        if (GlobalGameObjectMgr.Inst.Battle == false)
        {
            GlobalGameObjectMgr.Inst.Battle = true;

            WavesMob[] infos = new WavesMob[2];

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

            infos[1].eMobType = MobType.GhostShip;
            infos[1].nMobNum = 2;
            infos[1].fDelayTime = 3f;
            infos[1].fFirstDelayTime = 5f;

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

            EffectMgr.Inst.AllocateEffects(Effect.Explosion0, 70);
            EffectMgr.Inst.AllocateEffects(Effect.Explosion1, 70);
            EffectMgr.Inst.AllocateEffects(Effect.ShieldHit0, 70);

            BattleGameObjectMgr.Inst._OnNewBattle();
            TurretMgr.Inst._OnNewBattle();

            SpaceShipMgr.Inst.StartCreatingWaves(infos);
        }
    }

    private void _OnBattle()
    {

    }
}
