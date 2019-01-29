using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCntCtrl : MonoBehaviour
{
    public float m_trackingSpeed = 0f;
    public float m_textDistFromEnd = 0f;
    public Color m_pointNormalColor = new Color();
    public Color m_pointZeroColor = new Color();

    public GameObject m_progress = null;
    public GameObject m_point = null;
    public GameObject m_text = null;

    private int m_maxEnemyCnt = 0;
    private int m_destroyedEnemyCnt = 0;
    private float m_curRatio = 1f;

    private Image m_progressImg = null;
    private Text m_cntText = null;
    private RectTransform m_progressRectTrsf  = null;
    private RectTransform m_cntTextRectTrsf = null;
    private Image m_pointImg = null;

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

            if (m_destroyedEnemyCnt == m_maxEnemyCnt)
                m_pointImg.color = m_pointZeroColor;
            else
                m_pointImg.color = m_pointNormalColor;
        }
    }

    void Start()
    {
        m_progressImg = m_progress.GetComponent<Image>();
        m_progressRectTrsf = m_progress.GetComponent<RectTransform>();
        m_cntText = m_text.GetComponent<Text>();
        m_cntTextRectTrsf = m_text.GetComponent<RectTransform>();
        m_pointImg = m_point.GetComponent<Image>();

        m_pointImg.color = m_pointNormalColor;

        m_curRatio = 1f;
    }

    void Update()
    {
        if (m_destroyedEnemyCnt > m_maxEnemyCnt)
            return;

        if (m_maxEnemyCnt == 0)
            return;



        float targetRatio = 1f - ((float)m_destroyedEnemyCnt / m_maxEnemyCnt);
      
        m_curRatio = (1f - m_trackingSpeed * Time.deltaTime) * m_curRatio + m_trackingSpeed * Time.deltaTime * targetRatio;

        m_progressImg.fillAmount = m_curRatio;

        int leftCnt = m_maxEnemyCnt - m_destroyedEnemyCnt;

        m_cntText.text = leftCnt.ToString();

        // 현재 프로그래스 바의 끝 위치를 따라가게 text의 위치를 옮긴다
        Vector3 cntTextPos = m_cntTextRectTrsf.position;
        float progressStartX = m_progressRectTrsf.position.x - m_progressRectTrsf.rect.width * m_progressRectTrsf.localScale.x * 0.5f;

        cntTextPos.x = progressStartX + m_progressRectTrsf.rect.width * m_progressRectTrsf.localScale.x * m_curRatio + m_textDistFromEnd;

        m_cntTextRectTrsf.position = cntTextPos;
    }
}
