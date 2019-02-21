using UnityEngine;
using System.Collections;

public class Turret_Lv3_KingSlayerCtrl : TurretCtrl
{
    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv3_KingSlayer;
        m_bulletPool = BulletPool.Turret;

        m_turretType = Turret.Lv3_KingSlayer;

        m_blasterSound = AudioManager.eBulletSFX.KingSlayerSFX;


        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void CreateBullet()
    {
        Gunner target = null;

        target = SpaceShipMgr.Inst.FindBossTargetInFan(m_planetAngle, m_fanAngle, m_trsf.position, m_minDistToAttack);

        if (target != null)
        {
            AudioManager.Inst.playBulletSFX(m_trsf.position, m_blasterSound);
            BulletMgr.Inst.FireBullet(m_bulletPool, BulletPoolIdx, m_trsf.position + m_trsf.up * m_fireDistAlignUp, m_trsf.localEulerAngles, this, target);
            return;
        }

        target = SpaceShipMgr.Inst.FindFirstTargetInFan(m_planetAngle, m_fanAngle, m_trsf.position, m_minDistToAttack);

        if (target != null)
        {
            AudioManager.Inst.playBulletSFX(m_trsf.position, m_blasterSound);
        }

        BulletMgr.Inst.FireBullet(m_bulletPool, BulletPoolIdx, m_trsf.position + m_trsf.up * m_fireDistAlignUp, m_trsf.localEulerAngles, this, target);
    }
}
