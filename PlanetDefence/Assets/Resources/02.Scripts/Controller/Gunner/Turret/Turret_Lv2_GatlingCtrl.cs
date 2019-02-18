using UnityEngine;
using System.Collections;

public class Turret_Lv2_GatlingCtrl : TurretCtrl
{

    private void Start()
    {
        m_bulletType = Bullet.Lv2_Gatling;
        m_bulletPool = BulletPool.Turret;

        m_turretType = Turret.Lv2_Gatling;

        base.Init();
    }

    void Update()
    {

    }
}
