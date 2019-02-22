using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCtrl : MonoBehaviour
{
    public int m_maxHP = 0;
    public int m_recoverHP = 0;
    public Gunner[] m_planetHits = null;

    private int m_curHP = 0;
    private Transform m_trsf = null;
    private static PlanetCtrl m_inst = null;

    public static PlanetCtrl Inst
    {
        get
        {
            if (m_inst == null)
            {
                m_inst = GameObject.Find("Planet").GetComponent<PlanetCtrl>();
            }
            return m_inst;
        }
    }

    public int CurHP
    {
        get { return m_curHP; }
    }

    public int StartHP { get; set; } = -1;

    public int MaxHP
    {
        get { return m_maxHP; }
        set { m_maxHP = value; }
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

    public void _OnStart()
    {
        m_curHP = m_maxHP;

        if(StartHP > 0)
        {
            m_curHP = StartHP;
        }

        BattleGameObjectMgr.Inst.UpdatePlanetHP(m_maxHP, m_curHP);
    }

    public void _OnNewBattle()
    {
        m_curHP = m_curHP + m_recoverHP;

        if (m_curHP > m_maxHP)
            m_curHP = m_maxHP;

        m_trsf = GetComponent<Transform>();
        BattleGameObjectMgr.Inst.UpdatePlanetHP(m_maxHP, m_curHP);
    }

    public Gunner GetPlanetHit(int idx)
    {
        if (idx < 0 || idx >= m_planetHits.Length)
            return null;

        return m_planetHits[idx];
    }

    public void RecoverHP(float ratio)
    {
        m_curHP += (int)(m_maxHP * ratio);

        if (m_curHP > m_maxHP)
            m_curHP = m_maxHP;

        UpdateHP();
    }

    public void Hit(int damage)
    {
        if (m_curHP <= 0)
            return;

        m_curHP -= damage* 200;

        if (m_curHP <= 0)
        {
            m_curHP = 0;
        }

        UpdateHP();
    }

    private void UpdateHP()
    {
        EndingMgr.Inst.LeftPlanetHP = m_curHP;

        BattleGameObjectMgr.Inst.UpdatePlanetHP(m_maxHP, m_curHP);
    }

}
