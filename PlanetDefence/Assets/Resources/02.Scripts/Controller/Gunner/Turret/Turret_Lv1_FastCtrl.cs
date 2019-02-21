using UnityEngine;
using System.Collections;

public class Turret_Lv1_FastCtrl : TurretCtrl
{
    public override TurretData TurretData
    {
        get
        {
            TurretData_Fast turretData = new TurretData_Fast();

            turretData.maxHP = m_maxHP;
            turretData.fireDelay = m_fireDelay;

            return turretData;
        }
        set
        {
            TurretData_Fast turretData = (TurretData_Fast)value;

            m_maxHP = turretData.maxHP;
            m_fireDelay = turretData.fireDelay;
        }
    }

    private void Start()
    {
        m_bulletType = Bullet.Lv1_Fast;
        m_bulletPool = BulletPool.Turret;

        m_turretType = Turret.Lv1_Fast;

        m_blasterSound = AudioManager.eBulletSFX.GatlingSFX;


        base.Init();
    }

    void Update()
    {

    }
}
