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
            if(value < 0)
            {
                return;
            }

            m_maxEnemyCnt = value;
        }
    }

    public int DestroyedEnemyCnt
    {
        set
        {
            m_destroyedEnemyCnt = value;
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
