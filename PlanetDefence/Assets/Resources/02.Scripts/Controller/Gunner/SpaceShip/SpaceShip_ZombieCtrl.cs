using UnityEngine;
using System.Collections;

public class SpaceShip_ZombieCtrl : SpaceShipCtrl
{
    public float m_durationForRecovery = 0; // 회복상태로 되기 위해 피격 없이 기다려야하는 시간 
    public float m_recoveryDelay = 0; // 회복상태일때 회복간의 딜레이
    public float m_recoveryRate = 0;  // 회복되는 전체체력의 비례량

    protected override void _OnHit()
    {
        StopCoroutine("RecoverHPWithDelay");
        StopCoroutine("StayForRecovery");
        StartCoroutine("StayForRecovery");
    }

    private IEnumerator StayForRecovery()
    {
        yield return new WaitForSeconds(m_durationForRecovery);

        StartCoroutine("RecoverHPWithDelay");
    }

    private IEnumerator RecoverHPWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_recoveryDelay);

            RecoverHP(m_recoveryRate);
        }
    }

    void Start()
    {
        m_bulletType = Bullet.Spc_Zombie;
        m_bulletPool = BulletPool.SpaceShip;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.SpaceShip;

        SpaceShipType = MobType.ZombieShip;

        base.Init();
    }

    void Update()
    {
        MoveBody();
    }
}
