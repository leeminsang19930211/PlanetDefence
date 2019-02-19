using UnityEngine;
using System.Collections;

public class Bullet_Lv3_LaserCtrl : BulletCtrl
{
    public float m_stayDuration = 0;
    public float m_hitDelay = 0;

    public override BulletData BulletData
    {
        get
        {
            BulletData_Laser bulletData = new BulletData_Laser();

            bulletData.damage = m_damage;
            bulletData.duration = m_stayDuration;

            return bulletData;
        }

        set
        {
            BulletData_Laser bulletData = (BulletData_Laser)value;

            m_damage = bulletData.damage;
            m_stayDuration = bulletData.duration;
        }
    }

    void OnEnable()
    {
        if (Clone)
        {
            BulletType = Bullet.Lv3_Laser;

            m_effect_explosion = Effect.Explosion2;



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



