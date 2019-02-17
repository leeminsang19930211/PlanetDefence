using UnityEngine;
using System.Collections;

public class Turret_Lv3_HealCtrl : TurretCtrl
{
    public float m_healDelay = 0;
    public float m_healRate = 0;

    public override ITurretData TurretData
    {
        get
        {
            TurretData_Heal turretData = new TurretData_Heal();

            turretData.MaxHP = m_maxHP;
            turretData.healAmount = m_healRate;

            return turretData;
        }
        set
        {
            TurretData_Heal turretData = (TurretData_Heal)value;

            m_maxHP = turretData.MaxHP;
            m_healRate = turretData.healAmount;
        }
    }

    private void Start()
    {
        m_bulletType = Bullet.End;
        m_bulletPool = BulletPool.End;
        m_effectType = Effect.End;
        m_effectPool = EffectPool.End;

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
