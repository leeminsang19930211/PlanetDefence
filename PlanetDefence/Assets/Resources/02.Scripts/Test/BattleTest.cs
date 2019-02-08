using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTest : MonoBehaviour
{
    private int m_maxEnemCnt = 30;
    private int m_actvEnemCnt = 0;


    private int m_maxPlanetHP = 2000;
    private int m_curPlanetHP = 2000;

    private int m_gearCnt = 20;
    private int m_eleCircuitCnt = 20;
    private int m_coinCnt = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneLoader.LoadScene("Lobby");
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            m_actvEnemCnt += 1;

            if (m_actvEnemCnt > m_maxEnemCnt)
                m_actvEnemCnt = 0;

            BattleGameObjectMgr.Inst.UpdateEnemyCnt(m_maxEnemCnt, m_actvEnemCnt);

            m_curPlanetHP -= 50;

            if (m_curPlanetHP < 0)
                m_curPlanetHP = m_maxPlanetHP;

            BattleGameObjectMgr.Inst.UpdatePlanetHP(m_maxPlanetHP, m_curPlanetHP);

            m_gearCnt -= 1;
            m_eleCircuitCnt -= 1;
            m_coinCnt -= 1;

            if (m_gearCnt < 0)
                m_gearCnt = 20;
            if (m_eleCircuitCnt < 0)
                m_eleCircuitCnt = 20;
            if (m_coinCnt < 0)
                m_coinCnt = 20;

            BattleGameObjectMgr.Inst.UpdateJunkCnt(m_gearCnt);
            BattleGameObjectMgr.Inst.UpdateEleCircuitCnt(m_eleCircuitCnt);
            BattleGameObjectMgr.Inst.UpdateCoinCnt(m_coinCnt);

            TurretMgr.Inst.HitTurret(0, 10);
            BattleGameObjectMgr.Inst.FlashMiniPlanet(MiniPlanetCtrl.AREA_DIR.LEFT);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            TurretMgr.Inst.CreateTurretOnTurretSupport("Turret_Lv1_Laser");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            TurretMgr.Inst.RemoveTurretOnTurretSupport();
        }

    }
}
