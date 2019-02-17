using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

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
            Player.Inst.BuyTurret(Turret.Lv2_Poison, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.F3))
        {
            FileStream fs = File.Open(Application.persistentDataPath + "TestData.bin", FileMode.Create);

            BinaryWriter wr = new BinaryWriter(fs);

            wr.Write(GlobalGameObjectMgr.Inst.CurDay +1);

            fs.Close();
        }


        if (Input.GetKeyDown(KeyCode.F10))
        {
            BattleGameObjectMgr.Inst.PopUpResult(false);
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            BattleGameObjectMgr.Inst.PopUpResult(true);
        }

    }
}
