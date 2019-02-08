using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretCtrl : MonoBehaviour
{
    public int m_maxBulletInstCnt = 0;  // 총알 오브젝트 풀에 할당할 총알수
    public int m_maxBulletCnt = -1;      // 총 발사할 수 음수이면 무한 발사이다
    public int m_maxHP = 0;
    public int m_curHP = 0;
    public int m_angleForTargetting = 0;
    public float m_minDistToAttack = 0; // 공격하기 위한 적 우주선과의 최소 거리
    public float m_fireDelay = 0;
    public float m_bodyDistFromTurretSupport = 0;
    public float m_hpBarDistFromTurretSupport = 0;
    public float m_bulletDistFromtTurret = 0;
    public Bullet m_bulletType = Bullet.end;
    public GameObject m_body = null;
    public GameObject m_hpBar_front = null;
    public GameObject m_hpBar_back = null;

    protected float m_minAngleForTargeting = 0;
    protected float m_maxAngleForTargeting = 0;
    protected Transform m_trsf = null;
    protected PlanetArea m_planetArea = PlanetArea.outside;

    private int m_turretSupportIdx = 0;
    private float m_maxHpBar_frontScale = 0;
    private float m_prevHpBar_frontScale = 0;
    private Transform m_hpBar_frontTrsf = null;
    private Image m_hpBar_frontImg = null;

    public Vector3 BulletStartPos
    {
        get
        {
            return m_trsf.position + m_trsf.up * m_bulletDistFromtTurret;
        }
    }

    public int TurretSupportIdx
    {
        get
        {
            return m_turretSupportIdx;
        }

        set
        {
            m_planetArea = IdxToArea(value);

            float halfAngle = m_angleForTargetting / 2f;

            switch (m_planetArea)
            {
                case PlanetArea.Up:
                    m_minAngleForTargeting = halfAngle;
                    m_maxAngleForTargeting = 360 - halfAngle;
                    break;
                case PlanetArea.Left:
                    m_minAngleForTargeting = 90f - halfAngle;
                    m_maxAngleForTargeting = 90f + halfAngle;
                    break;
                case PlanetArea.Down:
                    m_minAngleForTargeting = 180f - halfAngle;
                    m_maxAngleForTargeting = 180f + halfAngle;
                    break;
                case PlanetArea.Right:
                    m_minAngleForTargeting = 270f - halfAngle;
                    m_maxAngleForTargeting = 270f + halfAngle;
                    break;
            }

            m_turretSupportIdx = value;
        }
    }

    public void Hit(int damage)
    {
        m_curHP -= damage;

        if (m_curHP < 0)
        {
            m_curHP = 0;
        }

        if (m_maxHP == 0)
            return;

        Vector3 localScale = m_hpBar_frontTrsf.localScale;
        localScale.x = m_curHP / (float)m_maxHP * m_maxHpBar_frontScale;

        m_hpBar_frontTrsf.localScale = localScale;

        Vector3 pos = m_hpBar_frontTrsf.position;
        pos.x -= (m_prevHpBar_frontScale - localScale.x) * 16f * 0.5f;

        m_hpBar_frontTrsf.position = pos;

        m_prevHpBar_frontScale = localScale.x;
    }

    protected void Init()
    {
        m_trsf = GetComponent<Transform>();

        Transform trsf = null;

        trsf = m_body.GetComponent<Transform>();
        trsf.position += trsf.up.normalized * m_bodyDistFromTurretSupport;

        trsf = m_hpBar_back.GetComponent<Transform>();
        trsf.position -= trsf.up.normalized * m_hpBarDistFromTurretSupport;

        m_hpBar_frontTrsf = m_hpBar_front.GetComponent<Transform>();
        m_hpBar_frontTrsf.position -= trsf.up.normalized * m_hpBarDistFromTurretSupport;

        m_maxHpBar_frontScale = m_hpBar_frontTrsf.localScale.x;
        m_prevHpBar_frontScale = m_maxHpBar_frontScale;

        m_hpBar_frontImg = m_hpBar_front.GetComponent<Image>();

        StartCoroutine("FireWithDelay");

        BulletMgr.Inst.AllocateBullets(m_bulletType, m_turretSupportIdx, m_maxBulletInstCnt);
    }

    protected IEnumerator FireWithDelay()
    {
        int curBullet = 0;

        if (m_maxBulletCnt > 0)
        {
            while (curBullet < m_maxBulletCnt)
            {
                yield return new WaitForSeconds(m_fireDelay);

                CreateBullet();

                curBullet++;
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

    // FireWithDelay 에서 호출되는 함수. 터렛별 총알을 생성하는 로직을 짜면된다.
    protected virtual void CreateBullet()
    {
        SpaceShipCtrl target = null;

        if (m_planetArea == PlanetArea.Up)
        {
            target = SpaceShipMgr.Inst.FindFirstTargetInFan(0, m_minAngleForTargeting, m_trsf.position, m_minDistToAttack);

            if (target == null)
            {
                target = SpaceShipMgr.Inst.FindFirstTargetInFan(m_maxAngleForTargeting, 360f, m_trsf.position, m_minDistToAttack);
            }
        }
        else
        {
            target = SpaceShipMgr.Inst.FindFirstTargetInFan(m_minAngleForTargeting, m_maxAngleForTargeting, m_trsf.position, m_minDistToAttack);
        }

        BulletMgr.Inst.FireBullet(TurretSupportIdx, BulletStartPos, m_trsf.localEulerAngles, target);
    }

    private PlanetArea IdxToArea(int idx)
    {
        int num = idx / 5; //5 단위로 위/왼쪽/아래/오른쪽으로 나뉨

        switch (num)
        {
            case 0:
                return PlanetArea.Up;
            case 1:
                return PlanetArea.Left;
            case 2:
                return PlanetArea.Down;
            case 3:
                return PlanetArea.Right;
        }

        return PlanetArea.outside;
    }
}
