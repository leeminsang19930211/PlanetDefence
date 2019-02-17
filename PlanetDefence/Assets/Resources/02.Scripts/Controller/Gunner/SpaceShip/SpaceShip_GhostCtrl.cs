using UnityEngine;
using System.Collections;

public class SpaceShip_GhostCtrl : SpaceShipCtrl
{
    public float m_duration_visible = 0;
    public float m_duration_invisible = 0;
    public float m_invisibleAlpha = 0;

    private bool m_visible = true;
    private SpriteRenderer m_sprite = null;

    public bool Visible { get { return m_visible; } }

    public override void Hit(int damage)
    {
        if (m_visible == false)
            return;

        base.Hit(damage);
    }

    protected override void CreateBullet()
    {
       if (m_visible == false)
            return;

            base.CreateBullet();
    }

    private IEnumerator UpdateVisible()
    {
        Color color = m_sprite.color ;
        float oriAlpha = color.a;

        while (true)
        {
            yield return new WaitForSeconds(m_duration_visible);

            color.a = m_invisibleAlpha;

            m_sprite.color= color;

            m_visible = false;

            yield return new WaitForSeconds(m_duration_invisible);

            color.a = oriAlpha;

            m_sprite.color = color;

            m_visible = true;          
        }
    }

    void Start()
    {
        m_bulletType = Bullet.Spc_Ghost;
        m_bulletPool = BulletPool.SpaceShip;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.SpaceShip;


        SpaceShipType = MobType.GhostShip;

        base.Init();

        m_sprite = GetComponent<SpriteRenderer>();

        StartCoroutine("UpdateVisible"); 
    }

    void Update()
    {
        MoveBody();
    }
}
