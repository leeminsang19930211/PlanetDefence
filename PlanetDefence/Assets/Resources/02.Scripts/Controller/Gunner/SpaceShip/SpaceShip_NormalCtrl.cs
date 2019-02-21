using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_NormalCtrl : SpaceShipCtrl
{
    void Start()
    {
        m_bulletType = Bullet.Spc_Normal;
        m_bulletPool = BulletPool.SpaceShip;

        m_blasterSound = AudioManager.eSpaceshipSFX.NomalSFX;


        SpaceShipType = MobType.Normal;

        base.Init();
    }

    void Update()
    {
        MoveBody();
    }
}
