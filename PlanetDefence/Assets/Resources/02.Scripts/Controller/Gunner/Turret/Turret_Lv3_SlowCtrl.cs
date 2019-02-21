using UnityEngine;
using System.Collections;

public class Turret_Lv3_SlowCtrl : TurretCtrl
{
    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv3_Slow;
        m_bulletPool = BulletPool.Turret;

        m_turretType = Turret.Lv3_Slow;

        m_blasterSound = AudioManager.eBulletSFX.SlowSFX;


        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

