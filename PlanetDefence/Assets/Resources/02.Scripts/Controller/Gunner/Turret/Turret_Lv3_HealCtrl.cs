using UnityEngine;
using System.Collections;

public class Turret_Lv3_HealCtrl : TurretCtrl
{
    public float m_healDelay = 0;
    public float m_healRate = 0;

    public bool m_fire = false;

    public override TurretData TurretData
    {
        get
        {
            TurretData_Heal turretData = new TurretData_Heal();

            turretData.maxHP = m_maxHP;
            turretData.fire = m_fire;

            return turretData;
        }
        set
        {
            TurretData_Heal turretData = (TurretData_Heal)value;

            m_maxHP = turretData.maxHP;
            m_fire = turretData.fire;
        }
    }

    protected override void CreateBullet()
    {
        if(m_fire)
        {
            base.CreateBullet();
        }     
    }

    private void Start()
    {
        m_bulletType = Bullet.Lv3_Heal;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv3_Heal;

        base.Init();

        StartCoroutine("HealPlanet");
    }

    private IEnumerator HealPlanet()
    {
        while(true)
        {
            PlanetCtrl.Inst.RecoverHP(m_healRate);

            yield return new WaitForSeconds(m_healDelay);
        }
    }

}
