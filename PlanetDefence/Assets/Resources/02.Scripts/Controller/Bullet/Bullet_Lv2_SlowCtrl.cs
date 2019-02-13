using UnityEngine;
using System.Collections;

public class Bullet_Lv2_SlowCtrl : BulletCtrl
{
    public float m_probability = 0f;
    public Gunner.SlowMoveInfo m_slowInfo;

    protected override void _OnTarget(Gunner target, Vector3 hitPos)
    {
        SpaceShipCtrl spaceShip = target as SpaceShipCtrl;

        if(spaceShip != null)
        {
            float rand = Random.Range(0, 1);

            if (rand <= m_probability)
            {
                target.SlowMove(m_slowInfo);
            }

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
