using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_NormalCtrl : SpaceShipCtrl
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
