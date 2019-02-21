using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastButtonCtrl : MonoBehaviour
{
    public GameObject m_fastButton_One = null;
    public GameObject m_fastButton_Double = null;

    private float m_curSpeedScale = 1f;

    public void RecoverToPrev()
    {
        Time.timeScale = m_curSpeedScale;
    }

    public void RecoverToOrigin()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        m_fastButton_One.SetActive(true);
        m_fastButton_Double.SetActive(false);
    }

    public void OnClick_FastButton_On()
    {
        Time.timeScale = 2f;
        m_curSpeedScale = 2f;
        m_fastButton_One.SetActive(false);
        m_fastButton_Double.SetActive(true);
    }

    public void OnClick_FastButton_Double()
    {
        Time.timeScale = 1f;
        m_curSpeedScale = 1f;
        m_fastButton_One.SetActive(true);
        m_fastButton_Double.SetActive(false);
    }
}
