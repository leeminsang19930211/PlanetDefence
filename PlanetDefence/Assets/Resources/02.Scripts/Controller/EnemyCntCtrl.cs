﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCntCtrl : MonoBehaviour
{
    public GameObject m_text = null;

    private int m_maxEnemyCnt = 0;
    private int m_destroyedEnemyCnt = 0;

    private Text m_cntText = null;

    public int MaxEnemyCnt
    {
        set
        {
            if(value < 0)
            {
                return;
            }

            m_maxEnemyCnt = value;
            m_destroyedEnemyCnt = 0;
        }
    }

    public void AddDestroyedEnemy(int add)      
    {
        m_destroyedEnemyCnt += add;

        if(m_destroyedEnemyCnt == m_maxEnemyCnt)
        {
            BattleGameObjectMgr.Inst.PopUpResult(true);
            m_destroyedEnemyCnt = 0;
        }
    }


    void Start()
    {
        m_cntText = m_text.GetComponent<Text>();
    }

    void Update()
    {
        m_cntText.text = "× " + (m_maxEnemyCnt - m_destroyedEnemyCnt).ToString();
    }
}
