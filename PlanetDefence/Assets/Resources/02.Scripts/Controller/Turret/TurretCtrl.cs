using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretCtrl : MonoBehaviour
{
    public int m_maxHP = 0;
    public int m_curHP = 0;
    public int m_angleForTargetting = 0;
    public float m_fireDelay = 0;
    public float m_bodyDistFromTurretSupport = 0;
    public float m_hpBarDistFromTurretSupport = 0;
    public GameObject m_body = null;
    public GameObject m_hpBar_front = null;
    public GameObject m_hpBar_back = null;

    protected float m_minAngleForTargeting = 0;
    protected float m_maxAngleForTargeting = 0;

    private float m_maxHpBar_frontScale = 0;
    private float m_prevHpBar_frontScale = 0;
    private PlanetArea m_area = PlanetArea.outside;
    private Transform m_hpBar_frontTrsf = null;
    private Image m_hpBar_frontImg= null;

    public PlanetArea Area
    {
        get
        {
            return m_area;
        }

        set
        {
            float halfAngle = m_angleForTargetting / 2f;

            switch (value)
            {
                case PlanetArea.Up:
                    m_minAngleForTargeting = -halfAngle;
                    m_maxAngleForTargeting = halfAngle;
                    break;
                case PlanetArea.Left:
                    m_minAngleForTargeting = 90f - halfAngle;
                    m_maxAngleForTargeting = 90f + halfAngle;
                    break;
                case PlanetArea.Down:
                    m_minAngleForTargeting = -1f*(180f - halfAngle);
                    m_maxAngleForTargeting = 180f - halfAngle;
                    break;
                case PlanetArea.Right:
                    m_minAngleForTargeting = -1f* ( 90f + halfAngle);
                    m_maxAngleForTargeting = -1f *( 90f - halfAngle) ;
                    break;
            }

            m_area = value;
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
    } 

    protected IEnumerator FireWithDelay()
    {
        while(true)
        {
            yield return new WaitForSeconds(m_fireDelay);

            CreateBullet();
        }
    }

    // FireWithDelay 에서 호출되는 함수. 터렛별 총알을 생성하는 로직을 짜면된다.
    protected virtual void CreateBullet()
    {

    }
}
