using System.Collections;
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
            if (value < 0)
            {
                return;
            }

            m_maxEnemyCnt = value;
            m_destroyedEnemyCnt = 0;

            UpdateEnemyCount();
        }
    }

    public int LeftEnemy
    {
        get
        {
            return m_maxEnemyCnt - m_destroyedEnemyCnt;
        }
    }

    private Text Text
    {
        get
        {
            if(m_cntText == null)
            {
                m_cntText = m_text.GetComponent<Text>();
            }

            return m_cntText;
        }
    }

    public void AddMaxEnemy(int add)
    {
        m_maxEnemyCnt += add;

        UpdateEnemyCount();
    }

    public void AddDestroyedEnemy(int add)      
    {
        if(m_destroyedEnemyCnt > m_maxEnemyCnt)
            return;

        m_destroyedEnemyCnt += add;

        UpdateEnemyCount();     
    }

    private void UpdateEnemyCount()
    {
        EndingMgr.Inst.LeftEnemies = m_maxEnemyCnt - m_destroyedEnemyCnt;

        Text.text = "× " + (m_maxEnemyCnt - m_destroyedEnemyCnt).ToString();
    }

}
