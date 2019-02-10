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
