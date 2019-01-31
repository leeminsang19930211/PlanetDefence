using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAACtrl : SpaceShipCtrl
{ 
    void Start()
    {
        base.Init();
    }

    void Update()
    {
        MoveBody();
    }
}
