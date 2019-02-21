using UnityEngine;
using System.Collections;

public class SpaceShip_PirateCtrl : SpaceShipCtrl
{
    void Start()
    {
        m_bulletType = Bullet.Spc_Pirate;
        m_bulletPool = BulletPool.SpaceShip;

        m_blasterSound = AudioManager.eSpaceshipSFX.NomalSFX;


        SpaceShipType = MobType.Pirate;

        base.Init();
    }

    void Update()
    {
        MoveBody();
    }
}
