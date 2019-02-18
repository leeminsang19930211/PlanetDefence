using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Lv2_PosionCtrl : TurretCtrl
{
    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv2_Poison;
        m_bulletPool = BulletPool.Turret;

        m_turretType = Turret.Lv2_Poison;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
