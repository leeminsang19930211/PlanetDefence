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
            EndingMgr.Inst._OnStart();


            m_onStart = true;
        } 
    }

    private void _OnNewBattle()
    {
        if (GlobalGameObjectMgr.Inst.Battle == false)
        {
            GlobalGameObjectMgr.Inst.Battle = true;

            WavesMob[] infos = new WavesMob[8];

            infos[0].eMobType = MobType.Normal;
            infos[0].nMobNum = 2;
            infos[0].fDelayTime = 4f;
            infos[0].fFirstDelayTime = 0f;

            infos[1].eMobType = MobType.Kamikaze;
            infos[1].nMobNum = 2;
            infos[1].fDelayTime = 3f;
            infos[1].fFirstDelayTime = 3f;

            infos[2].eMobType = MobType.DummyShip;
            infos[2].nMobNum = 2;
            infos[2].fDelayTime = 6f;
            infos[2].fFirstDelayTime = 6f;

            infos[3].eMobType = MobType.Pirate;
            infos[3].nMobNum = 2;
            infos[3].fDelayTime = 4f;
            infos[3].fFirstDelayTime = 9f;

            infos[4].eMobType = MobType.Little;
            infos[4].nMobNum = 2;
            infos[4].fDelayTime = 2f;
            infos[4].fFirstDelayTime = 2f;

            infos[5].eMobType = MobType.ZombieShip;
            infos[5].nMobNum = 2;
            infos[5].fDelayTime = 5f;
            infos[5].fFirstDelayTime = 2f;

            infos[1].eMobType = MobType.GhostShip;
            infos[1].nMobNum = 2;
            infos[1].fDelayTime = 3f;
            infos[1].fFirstDelayTime = 5f;

            infos[7].eMobType = MobType.BattleShip;
            infos[7].nMobNum = 2;
            infos[7].fDelayTime = 4f;
            infos[7].fFirstDelayTime = 2f;

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
            EffectMgr.Inst.AllocateEffects(Effect.Explosion2, 70);
            EffectMgr.Inst.AllocateEffects(Effect.ShieldHit0, 70);
            EffectMgr.Inst.AllocateEffects(Effect.Poison0, 70);

            BattleGameObjectMgr.Inst._OnNewBattle();
            TurretMgr.Inst._OnNewBattle();
            PlanetCtrl.Inst._OnNewBattle();


            SpaceShipMgr.Inst.StartCreatingWaves(infos);
        }
    }

    private void _OnBattle()
    {
        EndingMgr.Inst._OnBattle();
    }
}
