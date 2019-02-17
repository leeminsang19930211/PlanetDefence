using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TurretCtrl : Gunner
{
    public float m_minDistToAttack = 0;             // 공격하기 위한 적 우주선과의 최소 거리
    public float m_distFromTurretSupport = 0;       // 터렛 서포트로부터 위치 할 거리

    protected float m_planetAngle = 0;
    protected Turret m_turretType = Turret.End;

    public Turret TurretType { get { return m_turretType; } }

    public abstract ITurretData TurretData { get; set; }

    protected new void Init()
    {
        base.Init();

        m_trsf.position += m_trsf.up * m_distFromTurretSupport;

        m_planetAngle = (BulletPoolIdx / 5)*90f;    // BulletPoolIdx 는 터렛 지지대 인덱스와 같음

        TurretMgr.Inst.CheckShieldToShow(BulletPoolIdx);
    }

    protected override void _OnZeroHP()
    {
        Die();
    }

    protected override void _OnDying()
    {
        PlanetArea area = (PlanetArea)(BulletPoolIdx / 5);

        BattleGameObjectMgr.Inst.FlashMiniPlanet(area);
        TurretMgr.Inst.CheckShieldToShow(BulletPoolIdx, BulletPoolIdx);
    }

    protected override void CreateBullet()
    {
        Gunner target = null;

        target = SpaceShipMgr.Inst.FindFirstTargetInFan(m_planetAngle, m_fanAngle, m_trsf.position, m_minDistToAttack);

        BulletMgr.Inst.FireBullet(m_bulletPool, BulletPoolIdx, m_trsf.position + m_trsf.up * m_fireDistAlignUp, m_trsf.localEulerAngles, this, target);
    }
}
