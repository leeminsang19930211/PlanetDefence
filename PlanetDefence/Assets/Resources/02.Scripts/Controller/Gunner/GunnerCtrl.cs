﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    public int m_maxHP = 0;
    public int m_maxBullets = 0;            // BulletPool 에 할당할 총알의 수
    public int m_maxEffects = 0;            // EffectPool 에 할당할 이펙트의 수
    public int m_maxFires = -1;             // 총알을 총 발사할 수, 음수이면 무한 발사
    public float m_fanAngle = 45f;          // 타겟을 탐색할때 사용할 각도값
    public float m_fireDelay = 0;           // 총알 발사 딜레이
    public float m_fireDistAlignUp = 0;     // up 방향으로 총알을 쏠 위치까지의 거리
    public Bullet m_bulletType = Bullet.End;
    public BulletPool m_bulletPool = BulletPool.End;
    public Effect m_effectType = Effect.End;
    public EffectPool m_effectPool = EffectPool.End;
    public UnitHPBarCtrl m_unitHPBarCtrl = null;

    protected int m_curHP = 0;
    protected Transform m_trsf = null;

    public bool Clone { get; set; } = false; // 원본인지 복사된 객체인지를 구분하는 값
    public int BulletPoolIdx { get; set; } = -1;// BulletPool에 할당할때 사용할 idx

    public int CurHP
    {
        get { return m_curHP; }
    }

    public Vector3 Position
    {
        get
        {
            if (m_trsf == null)
                m_trsf = GetComponent<Transform>();

            return m_trsf.position;
        }
    }

    public Vector3 Up
    {
        get
        {
            if (m_trsf == null)
                m_trsf = GetComponent<Transform>();

            return m_trsf.up;
        }
    }

    public virtual void Hit(int damage)
    {
        m_curHP -= damage;

        if (m_curHP < 0)
        {
            m_curHP = 0;
            _OnZeroHP();
        }

        m_unitHPBarCtrl.UpdateHP(m_curHP, m_maxHP);
    }


    // Destory 함수 대신 이함수 호출해야함.
    public void Die()
    {
        if (Clone)
        {
            _OnDying();

            if (m_maxBullets > 0)
            {
                ClearBullets();
            }

            if (m_maxEffects > 0)
            {
                ClearEffects();
            }

            Destroy(this.gameObject);
        }
    }

    // 초기화 함수. 상속받은 클래스에서 꼭 호출해줄것
    protected void Init()
    {
        m_trsf = GetComponent<Transform>();
        m_curHP = m_maxHP;

        if(m_maxBullets > 0)
        {
            StartCoroutine("FireWithDelay");
            AllocateBullets();
        }

        if(m_maxEffects >0 )
        {
            AllocateEffects();
        }
    }


    // 데미지를 받아 죽기 이전에 호출되는 함수
    protected virtual void _OnZeroHP()
    {

    }

    // 파괴되기 이전에  호출되는 함수.
    protected virtual void _OnDying()
    {

    }

    // FireWithDelay 에서 딜레이 마다 호출되는 함수. 총알 생성하는 로직을 짜주면 됨.
    protected virtual void CreateBullet()
    {

    }

    private void AllocateBullets()
    {
        if (BulletPoolIdx < 0)
            return;

        BulletMgr.Inst.AllocateBullets(m_bulletType, m_bulletPool, BulletPoolIdx, m_maxBullets);
    }

    private void AllocateEffects()
    {
        if (BulletPoolIdx < 0)
            return;

        EffectMgr.Inst.AllocateEffects(m_effectType, m_effectPool, BulletPoolIdx, m_maxEffects);
    }

    private void ClearBullets()
    {
        if (BulletPoolIdx < 0)
            return;

        BulletMgr.Inst.ClearBullets(m_bulletPool, BulletPoolIdx);
    }

    private void ClearEffects()
    {
        if (BulletPoolIdx < 0)
            return;

        EffectMgr.Inst.ClearEffects(m_effectPool, BulletPoolIdx);
    }

    private IEnumerator FireWithDelay()
    {
        int curFires = 0;

        if (m_maxFires > 0)
        {
            while (curFires < m_maxFires)
            {
                yield return new WaitForSeconds(m_fireDelay);

                CreateBullet();

                curFires++;
            }
        }
        else
        {
            while (true)
            {
                yield return new WaitForSeconds(m_fireDelay);

                CreateBullet();
            }
        }
    }
}
