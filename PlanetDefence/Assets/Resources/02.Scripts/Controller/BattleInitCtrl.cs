using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitCtrl : MonoBehaviour
{
    private void Awake()
    {
        GlobalGameObjectMgr.Inst.SetGameObectActive("Choice", false);
        GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", true);

        GlobalGameObjectMgr.Inst.MoveGameObjectToScene("Battle", "Battle");

        //BattleGameObjectMgr.Inst.Init();

        GameObject turrets = GlobalGameObjectMgr.Inst.FindGameObject("Turrets");

        // 테스트 환경 용.씬에서 직접 프리팹을 추가해서 테스트 하는경우에는 GlobalGameObjectMgr.Inst 에 추가가 안되있다
        if (turrets == null)
        {
            turrets = GameObject.Find("Turrets");       
        }

        if(turrets)
        {
            // 잠깐 TurretMgr에서 검색할수 있도록 켰다 끈다
            turrets.SetActive(true);
            TurretMgr.Inst.Init();
            turrets.SetActive(false);
        }
    }
}
