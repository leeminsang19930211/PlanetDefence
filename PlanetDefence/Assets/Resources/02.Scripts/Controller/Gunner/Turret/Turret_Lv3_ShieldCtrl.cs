using UnityEngine;
using System.Collections;

public class Turret_Lv3_ShieldCtrl : TurretCtrl
{
    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.End;
        m_bulletPool = BulletPool.End;
        m_effectType = Effect.End;
        m_effectPool = EffectPool.End;

        m_turretType = Turret.Lv3_Shield;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}