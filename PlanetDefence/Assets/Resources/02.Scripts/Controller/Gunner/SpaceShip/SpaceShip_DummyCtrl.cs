using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_DummyCtrl : SpaceShipCtrl
{
    void Start()
    {
        m_bulletType = Bullet.End;
        m_bulletPool = BulletPool.End;


        SpaceShipType = MobType.DummyShip;


        base.Init();
    }

    void Update()
    {
        MoveBody();
    }
}
