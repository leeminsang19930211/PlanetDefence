using UnityEngine;
using System.Collections;

public class SpaceShip_BattleCtrl : SpaceShipCtrl
{
    public int m_childrenToCreate = 0; // 죽었을때 생성할 자식 의 수
    public float m_children_MinLength = 0; // 자식을 생성할 위치의 최소범위 
    public float m_children_MaxLength = 0; // 자식을 생성할 위치의 최대범위

    protected override void _OnZeroHP()
    {
        for(int i=0; i< m_childrenToCreate; ++i)
        {
            Vector3 randPos = m_trsf.position;

            if (Random.Range(-1f, 1f) >= 0)
            {
                randPos.x += Random.Range(m_children_MinLength, m_children_MaxLength);
            }
            else
            {
                randPos.x += Random.Range(m_children_MinLength, m_children_MaxLength) * -1f;
            }

            if (Random.Range(-1f, 1f) >= 0)
            {
                randPos.y += Random.Range(m_children_MinLength, m_children_MaxLength);
            }
            else
            {
                randPos.y += Random.Range(m_children_MinLength, m_children_MaxLength) * -1f;
            }

            SpaceShipMgr.Inst.CreateSpaceShip(MobType.Little, randPos, Quaternion.Euler(m_trsf.eulerAngles), STATE.STAYING);
        }

        SpaceShipMgr.Inst.AddSpaceShipCount(m_childrenToCreate);

        base._OnZeroHP();    
    }

    void Start()
    {
        m_bulletType = Bullet.Spc_Battle;
        m_bulletPool = BulletPool.SpaceShip;

        SpaceShipType = MobType.BattleShip;

        m_blasterSound = AudioManager.eSpaceshipSFX.BattleSFX;

        base.Init();
    }

    void Update()
    {
        MoveBody();
    }
}
