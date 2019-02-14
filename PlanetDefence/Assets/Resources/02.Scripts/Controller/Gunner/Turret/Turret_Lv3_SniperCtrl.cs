using UnityEngine;
using System.Collections;

public class Turret_Lv3_SniperCtrl: TurretCtrl
{
    protected override void CreateBullet()
    {
        Gunner target = null;

        target = SpaceShipMgr.Inst.FindMinHPTargetInFan(m_planetAngle, m_fanAngle, m_trsf.position, m_minDistToAttack);

        BulletMgr.Inst.FireBullet(m_bulletPool, BulletPoolIdx, m_trsf.position + m_trsf.up * m_fireDistAlignUp, m_trsf.localEulerAngles, this, target);
    }

    private void Start()
    {
        m_bulletType = Bullet.Lv3_Sniper;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv3_Sniper;

        base.Init();
    }

    private void Update()
    {

    }
}
