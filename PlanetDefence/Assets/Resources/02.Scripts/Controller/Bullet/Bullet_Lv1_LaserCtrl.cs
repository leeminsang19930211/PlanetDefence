using UnityEngine;
using System.Collections;

public class Bullet_Lv1_LaserCtrl : BulletCtrl
{
    public float m_duration = 0;

    void OnEnable()
    {
        base.Init();
        StartCoroutine("Stay");
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
        yield return new WaitForSeconds(m_duration);

        gameObject.SetActive(false);
    }
}
