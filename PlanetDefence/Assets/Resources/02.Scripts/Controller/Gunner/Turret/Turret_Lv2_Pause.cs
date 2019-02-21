using UnityEngine;
using System.Collections;

public class Turret_Lv2_Pause : TurretCtrl
{
    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv2_Pause;
        m_bulletPool = BulletPool.Turret;

        m_turretType = Turret.Lv2_Pause;

        m_blasterSound = AudioManager.eBulletSFX.PauseSFX;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
