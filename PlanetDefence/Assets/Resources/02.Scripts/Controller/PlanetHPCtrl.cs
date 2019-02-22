using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHPCtrl : MonoBehaviour
{
    public float m_trackingSpeed = 0;

    public GameObject m_planetHP_bar = null;
    public GameObject m_planetHp_text = null;

    private int m_maxHP = 0;
    private int m_curHP = 0;
    private float m_curRatio = 0;

    private Image m_barImg = null;
    private Text m_hpTxt = null;

    public int MaxHP
    {
        set
        {
            m_maxHP = value;
        }
    }

    public int CurHP
    {
        set
        {
            if (value < 0)
                return;

            m_curHP = value;
        }
    }

    void Start()
    {
        m_barImg = m_planetHP_bar?.GetComponent<Image>();
        m_hpTxt = m_planetHp_text?.GetComponent<Text>();

        m_curRatio = 0f;
    }

    void Update()
    {
        if (m_maxHP == 0)
            return;

        if (m_curHP > m_maxHP)
            return;

        float targetRatio = (float)m_curHP / m_maxHP;

        m_curRatio = (1f - m_trackingSpeed * Time.deltaTime) * m_curRatio + m_trackingSpeed * Time.deltaTime * targetRatio;

        m_barImg.fillAmount = m_curRatio;

        m_hpTxt.text = "HP : " + m_curHP.ToString() + " / " + m_maxHP.ToString();
    }
}
