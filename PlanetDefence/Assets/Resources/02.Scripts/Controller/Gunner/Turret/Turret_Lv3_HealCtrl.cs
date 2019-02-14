using UnityEngine;
using System.Collections;

public class Turret_Lv3_HealCtrl : TurretCtrl
{
    public float m_healDelay = 0;
    public float m_healRate = 0;

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
