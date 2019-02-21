using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretCtrl : Gunner
{
    public float m_minDistToAttack = 0;             // 공격하기 위한 적 우주선과의 최소 거리
    public float m_distFromTurretSupport = 0;       // 터렛 서포트로부터 위치 할 거리

    protected float m_planetAngle = 0;
    protected Turret m_turretType = Turret.End;
    protected AudioManager.eBulletSFX m_blasterSound = AudioManager.eBulletSFX.Max;

    public Turret TurretType { get { return m_turretType; } }

    public virtual TurretData TurretData
    {
        get
        {
            TurretData turretData = new TurretData();

            turretData.maxHP = m_maxHP;

            return turretData;
        }

        set
        {
            m_maxHP = value.maxHP;
        }
    }

    public void UpdateTurretData()
    {
        TurretData = Player.Inst.GetTurretData(TurretType);
    }

    protected new void Init()
    {
        base.Init();

        m_trsf.position += m_trsf.up * m_distFromTurretSupport;

        m_planetAngle = (BulletPoolIdx / 5)*90f;    // BulletPoolIdx 는 터렛 지지대 인덱스와 같음

        TurretMgr.Inst.CheckShieldToShow(BulletPoolIdx);

        m_explosionSound = AudioManager.eExplosionSFX.TurretBoomSFX;

        if (Clone)
        {
            UpdateTurretData();
        }
    }

    protected override void _OnZeroHP()
    {
        AudioManager.Inst.playExplosionSFX(m_trsf.position, m_explosionSound);
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

        if(target != null)
        {
            AudioManager.Inst.playBulletSFX(m_trsf.position, m_blasterSound);
        }

       
        BulletMgr.Inst.FireBullet(m_bulletPool, BulletPoolIdx, m_trsf.position + m_trsf.up * m_fireDistAlignUp, m_trsf.localEulerAngles, this, target);
    }
}
