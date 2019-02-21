using UnityEngine;
using System.Collections;

public class Turret_Lv3_BerserkerCtrl : TurretCtrl
{
    public float m_fireScale_beserker = 1f;
    public float m_beserkerHPRate = 0f;

    private void Start()
    {
        m_bulletType = Bullet.Lv3_Berserker;
        m_bulletPool = BulletPool.Turret;


        m_turretType = Turret.Lv3_Berserker;

        m_blasterSound = AudioManager.eBulletSFX.GatlingSFX;

        base.Init();
    }

    private void Update()
    {
        if(CurHP  < m_maxHP * m_beserkerHPRate)
        {
            m_fireDelay = m_fireDelay_ori * (1 /m_fireScale_beserker);
        }
    }
}
