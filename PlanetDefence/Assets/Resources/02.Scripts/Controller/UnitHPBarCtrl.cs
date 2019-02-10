using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHPBarCtrl : MonoBehaviour
{
    public float m_pixelLength = 0;
    public float m_distFromPos = 0;
    public Transform m_targetTrsf = null;
    public Transform m_frontImgTrsf = null;

    private float m_maxScaleX = 0;
    private float m_prevScaleX = 0;
    private Transform m_trsf = null;
    
    public void Start()
    {
        m_trsf = GetComponent<Transform>();

        m_maxScaleX = m_frontImgTrsf.localScale.x;
        m_prevScaleX = m_maxScaleX;

        m_trsf.position = m_targetTrsf.position + m_targetTrsf.up * m_distFromPos;
    }

    public void UpdateHP(int curHP, int maxHP)
    {
        if (m_trsf == null || m_frontImgTrsf == null)
            return;

        if (maxHP == 0)
            return;

        if (curHP > maxHP)
            return;
       
        Vector3 _localScale = m_frontImgTrsf.localScale;
        _localScale.x = (curHP / (float)maxHP) * m_maxScaleX;

        m_frontImgTrsf.localScale = _localScale;

        m_frontImgTrsf.position -= (m_trsf.right * (m_prevScaleX - _localScale.x) * 0.5f * m_pixelLength);

        m_prevScaleX = _localScale.x;

    }
}
