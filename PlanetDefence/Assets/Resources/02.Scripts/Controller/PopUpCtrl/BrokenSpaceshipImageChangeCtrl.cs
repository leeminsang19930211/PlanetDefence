using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenSpaceshipImageChangeCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BattleGameObjectMgr.Inst.BrokenSpaceshipImageChange();
    }
}
