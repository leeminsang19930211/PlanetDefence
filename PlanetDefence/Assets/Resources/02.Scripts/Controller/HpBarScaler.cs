using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarScaler : MonoBehaviour
{
    public Gunner m_refCtrl = null;

    private Transform m_transform = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (m_refCtrl == null)
            return;

        m_transform = GetComponent<Transform>();

        Vector3 localScale = m_transform.localScale;

        localScale.x = 7f * m_refCtrl.m_maxHP / 3000f;

        m_transform.localScale = localScale;
    }
}
