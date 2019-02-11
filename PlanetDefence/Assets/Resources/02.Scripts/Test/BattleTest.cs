using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTest : MonoBehaviour
{
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
            Player.Inst.BuyTurret(Turret.Lv2_Shield, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Player.Inst.BuyTurret(Turret.Lv1_Laser, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Player.Inst.SellTurret();
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            BattleGameObjectMgr.Inst.PopUpResult(false);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            BattleGameObjectMgr.Inst.PopUpResult(true);
        }

    }
}
