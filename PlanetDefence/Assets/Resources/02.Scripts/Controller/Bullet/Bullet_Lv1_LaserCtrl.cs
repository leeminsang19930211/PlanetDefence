using UnityEngine;
using System.Collections;

public class Bullet_Lv1_LaserCtrl : BulletCtrl
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
            BulletType = Bullet.Lv1_Laser;

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

    protected override void _OnTargetByRayCast(Gunner target, Vector3 hitPos)
    {
        PlayEffect(Effect.Explosion2, hitPos);
        target.Hit(m_damage);
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

