using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTest : MonoBehaviour
{
    private int m_maxPlanetHP = 2000;
    private int m_curPlanetHP = 2000;

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
            m_curPlanetHP -= 50;

            if (m_curPlanetHP < 0)
                m_curPlanetHP = m_maxPlanetHP;

            BattleGameObjectMgr.Inst.UpdatePlanetHP(m_maxPlanetHP, m_curPlanetHP);        

            TurretMgr.Inst.HitTurret(0, 10);
            
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Player.Inst.BuyTurret(Turret.Lv1_Laser, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Player.Inst.SellTurret(20, 20);
        }

    }
}
