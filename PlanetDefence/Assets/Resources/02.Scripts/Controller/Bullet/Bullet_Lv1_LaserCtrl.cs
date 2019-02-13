﻿using UnityEngine;
using System.Collections;

public class Bullet_Lv1_LaserCtrl : BulletCtrl
{
    public float m_stayDuration = 0;
    public float m_hitDuration = 0;

    void OnEnable()
    {
        RotateToTarget();
        base.Init();
        StartCoroutine("Stay");
        StartCoroutine("Hit");
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        RotateToTarget();
    }

    private IEnumerator Stay()
    {
        yield return new WaitForSeconds(m_stayDuration);

        gameObject.SetActive(false);
    }

    private IEnumerator Hit()
    {
        while(true)
        {
            RayCastTargets();

            yield return new WaitForSeconds(m_hitDuration);
        }        
    }
}
