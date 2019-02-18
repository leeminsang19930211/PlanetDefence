using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Lv1_MissileCtrl : TurretCtrl
{
    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv1_Missile;
        m_bulletPool = BulletPool.Turret;

        m_turretType = Turret.Lv1_Missile;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
