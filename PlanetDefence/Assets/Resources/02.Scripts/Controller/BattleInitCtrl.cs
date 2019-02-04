using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitCtrl : MonoBehaviour
{
    private void Awake()
    {
        GlobalGameObjectMgr.Inst.SetGameObectActive("Choice", false);
        GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", true);
        GlobalGameObjectMgr.Inst.SetGameObectActive("BattleStatic", true);

        if(GlobalGameObjectMgr.Inst.Battling == false)
        {
            // TEMP : 나중에 배틀이 정말 끝난는지 판단하여 작성해야함. 
            GlobalGameObjectMgr.Inst.Battling = true;

            // Awake 함수 호출을 위해 여기서 인스탄트한다
            BattleGameObjectMgr.Inst.Instantiate();
            TurretMgr.Inst.Instantiate();
            SpaceShipMgr.Inst.Instantiate();

            WavesMob[] infos = new WavesMob[3];

            infos[0].eMobType = MobType.Normal;
            infos[0].nMobNum = 5;
            infos[0].fDelayTime = 2f;
            infos[0].fFirstDelayTime = 0f;

            infos[1].eMobType = MobType.Kamikaze;
            infos[1].nMobNum = 4;
            infos[1].fDelayTime = 2f;
            infos[1].fFirstDelayTime = 3f;

            infos[2].eMobType = MobType.Pirate;
            infos[2].nMobNum = 2;
            infos[2].fDelayTime = 3f;
            infos[2].fFirstDelayTime = 4f;

            // 이 함수에 웨이브 정보를 넘기면 웨이브가 생성이 시작된다.
            SpaceShipMgr.Inst.StartCreatingWaves(infos);
        }
   
    }
}
