using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCtrl: MonoBehaviour
{
    static bool m_init = false;

    private void Awake()
    {
        if(m_init == false) // 초기화 순서때문에 start, awake 함수 이용안하고 직접 호출함
        {
            BattleGameObjectMgr.Inst.Init();
            BulletMgr.Inst.Init();
            TurretMgr.Inst.Init();
            SpaceShipMgr.Inst.Init();
            Player.Inst.Init();

            m_init = true;
        }

        if(GlobalGameObjectMgr.Inst.Battle == false)
        {           
            GlobalGameObjectMgr.Inst.Battle = true;

            WavesMob[] infos = new WavesMob[2];

            infos[0].eMobType = MobType.Normal;
            infos[0].nMobNum = 30;
            infos[0].fDelayTime = 2f;
            infos[0].fFirstDelayTime = 0f;

            infos[1].eMobType = MobType.Kamikaze;
            infos[1].nMobNum = 10;
            infos[1].fDelayTime = 6f;
            infos[1].fFirstDelayTime = 3f;

            int spaceShipsNum = 0;

            foreach(WavesMob info in infos)
            {
                spaceShipsNum += info.nMobNum;
            }

            BulletMgr.Inst.ClearBulletPool(BulletPool.Turret);
            BulletMgr.Inst.AllocateBulletPool(BulletPool.Turret, 20);

            BulletMgr.Inst.ClearBulletPool(BulletPool.SpaceShip);
            BulletMgr.Inst.AllocateBulletPool(BulletPool.SpaceShip, spaceShipsNum);
            
            SpaceShipMgr.Inst.StartCreatingWaves(infos);
        }

    }

    public static void Release_Clear()
    {
        Player.Inst.Release_Clear();
        SpaceShipMgr.Inst.Release_Clear();
        TurretMgr.Inst.Release_Clear();
        BulletMgr.Inst.Release_Clear();
        BattleGameObjectMgr.Inst.Release_Clear();
        PlanetCtrl.Inst.Release_Clear();

        GlobalGameObjectMgr.Inst.Battle = false;
    }

    public static void Release_Fail()
    {
        Player.Inst.Release_Fail();
        SpaceShipMgr.Inst.Release_Fail();
        TurretMgr.Inst.Release_Fail();
        BulletMgr.Inst.Release_Fail();
        BattleGameObjectMgr.Inst.Release_Fail();
        PlanetCtrl.Inst.Release_Fail();


        GlobalGameObjectMgr.Inst.Battle = false;
    } 
}
