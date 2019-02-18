using UnityEngine;
using System.Collections;

public class Turret_Lv3_GatlingCtrl : TurretCtrl
{
    private void Start()
    {
        m_bulletType = Bullet.Lv3_Gatling;
        m_bulletPool = BulletPool.Turret;

        m_turretType = Turret.Lv3_Gatling;

        base.Init();
    }

    void Update()
    {

    }
}
