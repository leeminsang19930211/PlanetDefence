using UnityEngine;
using System.Collections;

public class Bullet_Lv2_LaserCtrl : BulletCtrl
{
    public float m_stayDuration = 0;
    public float m_hitDelay = 0;

    void OnEnable()
    {
        if (Clone)
        {
            RotateToTarget();
            base.Init();
            StartCoroutine("Stay");
            StartCoroutine("Hit");
        }
    }

    private void Start()
    {

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
        while (true)
        {
            RayCastTargets();

            yield return new WaitForSeconds(m_hitDelay);
        }
    }
}

