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
   
        }

        if(Input.GetKeyDown(KeyCode.F3))
        {
            FileMgr.Inst.SavePlayerData();
        }

    }
}
