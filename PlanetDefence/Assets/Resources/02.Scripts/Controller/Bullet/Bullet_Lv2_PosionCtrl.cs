using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Lv2_PosionCtrl : BulletCtrl
{
    public float m_probability = 0f;
    public Gunner.DotInfo m_dotInfo;

    protected override void _OnTarget(Gunner target, Vector3 hitPos)
    {
        float rand = Random.Range(0, 1);

        if(rand <= m_probability)
        {
            target.Hit_Dot(m_dotInfo);
        }

        base._OnTarget(target, hitPos);
    }

    private void OnEnable()
    {
        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}
