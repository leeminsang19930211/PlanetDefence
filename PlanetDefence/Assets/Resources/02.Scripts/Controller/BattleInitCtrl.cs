using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitCtrl : MonoBehaviour
{
    private void Awake()
    {
        if(GlobalGameObjectMgr.Inst.Battle == false)
        {
            // TEMP : 나중에 배틀이 정말 끝난는지 판단하여 작성해야함. 
            GlobalGameObjectMgr.Inst.Battle = true;

            BattleGameObjectMgr.Inst.Init();
            TurretMgr.Inst.Init();
            SpaceShipMgr.Inst.Init();
            BulletMgr.Inst.Init();
            Player.Inst.Instantiate();

            WavesMob[] infos = new WavesMob[2];

            infos[0].eMobType = MobType.Normal;
            infos[0].nMobNum = 30;
            infos[0].fDelayTime = 2f;
            infos[0].fFirstDelayTime = 0f;

            infos[1].eMobType = MobType.Kamikaze;
            infos[1].nMobNum = 10;
            infos[1].fDelayTime = 6f;
            infos[1].fFirstDelayTime = 3f;

            //infos[2].eMobType = MobType.Pirate;
            //infos[2].nMobNum = 2;
            //infos[2].fDelayTime = 3f;
            //infos[2].fFirstDelayTime = 4f;

            // 이 함수에 웨이브 정보를 넘기면 웨이브가 생성이 시작된다.
            SpaceShipMgr.Inst.StartCreatingWaves(infos);

            BulletMgr.Inst.ClearBulletPool(BulletPool.SpaceShip);
            BulletMgr.Inst.AllocateBulletPool(BulletPool.SpaceShip, SpaceShipMgr.Inst.MaxSpaceShipCnt);

            BulletMgr.Inst.ClearBulletPool(BulletPool.Turret);
            BulletMgr.Inst.AllocateBulletPool(BulletPool.Turret, 20);
        }

    }
}
