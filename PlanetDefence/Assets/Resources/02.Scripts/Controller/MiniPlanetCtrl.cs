﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlanetCtrl : MonoBehaviour
{
    public enum AREA_DIR
    {
        UP,
        LEFT,
        DOWN,
        RIGHT
    }

    public int m_maxFalshCnt = 0;
    public float m_flashDuration = 0;
    
    public GameObject m_miniPlanet_spot = null;
    public GameObject m_miniPlanet_up = null;
    public GameObject m_miniPlanet_left = null;
    public GameObject m_miniPlanet_down = null;
    public GameObject m_miniPlanet_right = null;
    
    private Transform m_miniPlanet_spotTrsf = null;

    void Start()
    {
        m_miniPlanet_spotTrsf = m_miniPlanet_spot?.GetComponent<Transform>();
    }

    void Update()
    {
        
    }

    public void FlashArea(AREA_DIR areaDir )
    {
        switch (areaDir)
        {
            case AREA_DIR.UP:
                StopCoroutine("Coroutine_FlashArea");
                StartCoroutine("Coroutine_FlashArea", m_miniPlanet_up);
                break;
            case AREA_DIR.LEFT:
                StopCoroutine("Coroutine_FlashArea");
                StartCoroutine("Coroutine_FlashArea", m_miniPlanet_left);
                break;
            case AREA_DIR.DOWN:
                StopCoroutine("Coroutine_FlashArea");
                StartCoroutine("Coroutine_FlashArea", m_miniPlanet_down);
                break;
            case AREA_DIR.RIGHT:
                StopCoroutine("Coroutine_FlashArea");
                StartCoroutine("Coroutine_FlashArea", m_miniPlanet_right);
                break;
        }
    }

    public void RotateToTarget(Vector3 targetDir)
    {
        float angle = MyMath.LeftAngle360(Vector3.up, targetDir);

        m_miniPlanet_spotTrsf.eulerAngles = new Vector3(0, 0, angle);

        //m_miniPlanet_spotTrsf.Rotate(Vector3.forward, angle);
    }

    private IEnumerator Coroutine_FlashArea(GameObject area)
    {
        int curFlashCnt = 0;

        while(curFlashCnt < m_maxFalshCnt)
        {
            area.SetActive(true);

            yield return new WaitForSeconds(m_flashDuration);

            area.SetActive(false);

            yield return new WaitForSeconds(m_flashDuration);

            curFlashCnt++;
        }
    }
}
