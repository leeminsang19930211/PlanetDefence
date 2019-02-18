using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    [System.Serializable]
    public struct DotInfo
    {
        public int damage;
        public int count;
        public float delay;
    }

    [System.Serializable]
    public struct SlowMoveInfo
    {
        public float slowScale;
        public float duration;
    }

    public int m_maxHP = 0;
    public int m_maxBullets = 0;            // BulletPool 에 할당할 총알의 수
    public int m_maxFires = -1;             // 총알을 총 발사할 수, 음수이면 무한 발사
    public float m_fanAngle = 45f;          // 타겟을 탐색할때 사용할 각도값
    public float m_fireDelay = 0;           // 총알 발사 딜레이
    public float m_fireDistAlignUp = 0;     // up 방향으로 총알을 쏠 위치까지의 거리
    public UnitHPBarCtrl m_unitHPBarCtrl = null;

    protected int m_curHP = 0;
    protected float m_fireDelay_ori = 0;
    protected Bullet m_bulletType = Bullet.End;
    protected BulletPool m_bulletPool = BulletPool.End;
    protected Transform m_trsf = null;

    public bool Clone { get; set; } = false; // 원본인지 복사된 객체인지를 구분하는 값
    public int BulletPoolIdx { get; set; } = -1;// BulletPool에 할당할때 사용할 idx

    private bool m_dead = false;

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

    public void RecoverHP(float ratePerMaxHP)
    {
        m_curHP += (int)(m_maxHP * ratePerMaxHP);

        if (m_curHP > m_maxHP)
            m_curHP = m_maxHP;

        m_unitHPBarCtrl.UpdateHP(m_curHP, m_maxHP);
    }

    public virtual void Hit(int damage)
    {
        m_curHP -= damage;

        _OnHit();

        if (m_curHP < 0)
        {
            m_curHP = 0;
            _OnZeroHP();
        }

        m_unitHPBarCtrl.UpdateHP(m_curHP, m_maxHP);
    }

    public void HitToDie()
    {
        m_curHP = 0;
        _OnZeroHP();
        m_unitHPBarCtrl.UpdateHP(m_curHP, m_maxHP);
    }

    public virtual void Hit_Dot(DotInfo dotInfo)
    {
        StartCoroutine("Hit_Dot_Coroutine", dotInfo);
    }

    public virtual void SlowMove(SlowMoveInfo slowInfo)
    {

    }

    public void StopFire(float duration)
    {
        StopCoroutine("StopFire_Coroutine");
        StartCoroutine("StopFire_Coroutine", duration);
    }

    // Destory 함수 대신 이함수 호출해야함.
    public void Die()
    {
        if (m_dead)
            return;

        m_dead = true;

        if (Clone)
        {
           
            _OnDying();

            if (m_maxBullets > 0)
            {
                ClearBullets();
            }


            Destroy(this.gameObject);
        }
    }

    // 새로운 배틀이 시작되었을때 호출되는 함수
    public void OnNewBattle()
    {
        if (m_maxBullets > 0)
        {
            AllocateBullets();
        }
    }

    // 초기화 함수. 상속받은 클래스에서 꼭 호출해줄것
    protected void Init()
    {
        m_trsf = GetComponent<Transform>();
        m_curHP = m_maxHP;
        m_fireDelay_ori = m_fireDelay;

        if (m_maxBullets > 0)
        {
            AllocateBullets();
        }
    }

    protected virtual void _OnHit()
    {

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

    private void ClearBullets()
    {
        if (BulletPoolIdx < 0)
            return;

        BulletMgr.Inst.ClearBullets(m_bulletPool, BulletPoolIdx);
    }

    private IEnumerator Hit_Dot_Coroutine(DotInfo dotInfo)
    {
        int curCount = 0;

        while (curCount < dotInfo.count)
        {
            yield return new WaitForSeconds(dotInfo.delay);

            Hit(dotInfo.damage);

            curCount++;
        }
    }

    private IEnumerator StopFire_Coroutine(float duration)
    {
        if(m_maxBullets  > 0)
        {            
            StopCoroutine("FireWithDelay");

            yield return new WaitForSeconds(duration);

            StartCoroutine("FireWithDelay");
        }
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

    private void OnEnable()
    {
        if (m_maxBullets > 0)
        {
            StartCoroutine("FireWithDelay");
        }
    }
}
