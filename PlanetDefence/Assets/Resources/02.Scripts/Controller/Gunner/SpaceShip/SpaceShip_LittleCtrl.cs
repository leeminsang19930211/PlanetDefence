using UnityEngine;
using System.Collections;

public class SpaceShip_LittleCtrl : SpaceShipCtrl
{
    void Start()
    {
        m_bulletType = Bullet.Spc_Little;
        m_bulletPool = BulletPool.SpaceShip;

        SpaceShipType = MobType.Little;

        base.Init();
    }

    void Update()
    {
        MoveBody();
    }
}
