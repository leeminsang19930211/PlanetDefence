using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_DummyCtrl : SpaceShipCtrl
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
