using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretCtrl : MonoBehaviour
{
    public int m_maxHP = 0;
    public int m_curHP = 0;
    public float m_bodyDistFromTurretSupport = 0;
    public float m_hpBarDistFromTurretSupport = 0;

    public GameObject m_body = null;
    public GameObject m_hpBar_front = null;
    public GameObject m_hpBar_back = null;

    private float m_maxHpBar_frontScale = 0;
    private float m_prevHpBar_frontScale = 0;

    private Transform m_hpBar_frontTrsf = null;
    private Image m_hpBar_frontImg= null;

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
    }

    public void Hit(int damage)
    {
        m_curHP -= damage;

        if(m_curHP < 0)
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
}
