using UnityEngine;
using System.Collections;

public class Turret_Lv3_MissileCtrl : TurretCtrl
{
    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv3_Missile;
        m_bulletPool = BulletPool.Turret;

        m_turretType = Turret.Lv3_Missile;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

