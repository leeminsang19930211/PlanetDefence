using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMgr : MonoBehaviour
{
    private static BulletMgr m_inst = null;
    private List<int>[] m_bulletPoolIndex = new List<int>[(int)BulletPool.End];
    private List<List<BulletCtrl>>[] m_bulletPool = new List<List<BulletCtrl>>[(int)BulletPool.End];
    private Dictionary<string, GameObject> m_sourceBullets = new Dictionary<string, GameObject>();

    public static BulletMgr Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "BulletMgr";
                m_inst = container.AddComponent<BulletMgr>() as BulletMgr;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }

    public void Init()
    {
        GameObject bullets = GlobalGameObjectMgr.Inst.FindGameObject("Bullets");

        // 테스트 환경 용.씬에서 직접 프리팹을 추가해서 테스트 하는경우에는 GlobalGameObjectMgr.Inst 에 추가가 안되있다
        if (bullets == null)
        {
            bullets = GameObject.Find("Bullets");
        }

        if (bullets)
        {
            // 잠깐 SpaceShipMgr에서 검색할수 있도록 켰다 끈다
            bullets.SetActive(true);
            SetUpBullets();
            bullets.SetActive(false);
        }
    }

    public void Release_Clear()
    {
        for (int i = 0; i < (int)BulletPool.End; ++i)
        {
            ClearBulletPool((BulletPool)i);
        }
    }

    public void Release_Fail()
    {
        for(int i=0; i<(int)BulletPool.End; ++i)
        {
            ClearBulletPool((BulletPool)i);
        } 
    }

    public bool AllocateBulletPool(BulletPool pool, int cnt )
    {
        if(m_bulletPool[(int)pool].Count > 0 || m_bulletPool[(int)pool].Count > 0)
        {
            Debug.Log("The bulletPool is alloceted already");
            return false;
        }

        for(int i=0; i<cnt; ++i)
        {
            m_bulletPool[(int)pool].Add(new List<BulletCtrl>());
            m_bulletPoolIndex[(int)pool].Add(0);
        }
        return true;
    }

    public bool ClearBulletPool(BulletPool pool)
    {
        if (m_bulletPool[(int)pool] == null)
            return false;

        for(int i=0; i<m_bulletPool[(int)pool].Count; ++i)
        {
            ClearBullets(pool, i);
           
        }

        m_bulletPoolIndex[(int)pool].Clear();
        m_bulletPool[(int)pool].Clear();
        m_bulletPool[(int)pool].Capacity = 0;

        return true;
    }

    public bool AllocateBullets(Bullet bullet, BulletPool pool, int idx, int bulletCnt, bool active = false)
    {
        if(m_bulletPool[(int)pool].Count == 0)
        {
            Debug.Log("The bulletPool is not allocted");
            return false;
        }

        if (idx < 0 || idx >= m_bulletPool[(int)pool].Count)
        {
            Debug.Log("the bullet idx is out of the range");
            return false;
        }

        if (m_bulletPool[(int)pool][idx].Count > 0)
        {
            Debug.Log("the bullets at the idx is created already");
            return false;
        }

        GameObject source = FindSourceBullet(bullet);

        if (source == null)
        {
            Debug.Log("Finding the source of bullet failed");
            return false;
        }

        m_bulletPool[(int)pool][idx].Capacity = bulletCnt;

        Transform parentTrsf = GameObject.FindGameObjectWithTag("BATTLESTATIC")?.GetComponent<Transform>();

        for (int i = 0; i < bulletCnt; ++i)
        {
            GameObject obj = null;

            obj = Instantiate(source, parentTrsf);

            obj.SetActive(active);

            m_bulletPool[(int)pool][idx].Add(obj.GetComponent<BulletCtrl>());
        }

        m_bulletPoolIndex[(int)pool][idx] = 0;

        return true;
    }

    public bool ClearBullets(BulletPool pool, int idx)
    {
        if (idx < 0 || idx >= m_bulletPool[(int)pool].Count)
        {
            Debug.Log("the bullet idx is out of the range");
            return false;
        }

        foreach (BulletCtrl ctrl in m_bulletPool[(int)pool][idx])
        {
            Destroy(ctrl.gameObject);
        }

        m_bulletPool[(int)pool][idx].Clear();
        m_bulletPool[(int)pool][idx].Capacity = 0;
        m_bulletPoolIndex[(int)pool][idx] = 0;

        return true;
    }

    public bool FireBullet(BulletPool pool, int idx, Vector3 startPos, Vector3 startAngle, Gunner shooter, Gunner target)
    {
        if (target == null)
            return false;

        if (idx < 0 || idx >= m_bulletPool[(int)pool].Count)
        {
            Debug.Log("the bullet idx is out of the range");
            return false;
        }

        if (m_bulletPool[(int)pool][idx].Count == 0)
        {
            Debug.Log("the bullets is not allocated");
            return false;
        }

        m_bulletPool[(int)pool][idx][m_bulletPoolIndex[(int)pool][idx]].gameObject.SetActive(true);
        m_bulletPool[(int)pool][idx][m_bulletPoolIndex[(int)pool][idx]].Fire(startPos, startAngle, shooter, target);
        m_bulletPoolIndex[(int)pool][idx] += 1;

        if (m_bulletPoolIndex[(int)pool][idx] >= m_bulletPool[(int)pool][idx].Count)
            m_bulletPoolIndex[(int)pool][idx] = 0;

        return true;
    }

    private GameObject FindSourceBullet(Bullet bullet)
    {
        GameObject source = null;

        m_sourceBullets.TryGetValue(EnumToStr(bullet), out source);

        return source;
    }

    private bool AddBullet(string name)
    {
        GameObject bullet = null;

        bullet = GameObject.Find(name);
        if (bullet == null)
        {
            Debug.LogError("Finding " + name + " failed");
            return false;
        }

        m_sourceBullets.Add(name, bullet);

        return true;
    }

    private void SetUpBullets()
    {
        AddBullet("Bullet_Lv1_Missile");
        AddBullet("Bullet_Lv1_Laser");
        AddBullet("Bullet_Lv1_Gatling");
        AddBullet("Bullet_Lv2_Posion");
        AddBullet("Bullet_Lv2_Slow");
        AddBullet("Bullet_Lv2_Pause");
        AddBullet("Bullet_Lv3_Sniper");
        AddBullet("Bullet_Lv3_Berserker");
        AddBullet("Bullet_Spc_Normal");
        AddBullet("Bullet_Spc_Pirate");
        AddBullet("Bullet_Spc_Little");
        AddBullet("Bullet_Spc_Zombie");
        AddBullet("Bullet_Spc_Ghost");

        for (int i=0; i <(int)BulletPool.End; ++i)
        {
            m_bulletPool[i] = new List<List<BulletCtrl>>();
            m_bulletPoolIndex[i] = new List<int>();
        }
    }

    private string EnumToStr(Bullet bullet)
    {
        string str = "";

        switch (bullet)
        {
            case Bullet.Lv1_Missile:
                str = "Bullet_Lv1_Missile";
                break;
            case Bullet.Lv1_Laser:
                str = "Bullet_Lv1_Laser";
                break;
            case Bullet.Lv1_Gatling:
                str = "Bullet_Lv1_Gatling";
                break;
            case Bullet.Lv2_Posion:
                str = "Bullet_Lv2_Posion";
                break;
            case Bullet.Lv2_Slow:
                str = "Bullet_Lv2_Slow";
                break;
            case Bullet.Lv2_Pause:
                str = "Bullet_Lv2_Pause";
                break;
            case Bullet.Lv3_Sniper:
                str = "Bullet_Lv3_Sniper";
                break;
            case Bullet.Lv3_Berserker:
                str = "Bullet_Lv3_Berserker";
                break;
            case Bullet.Spc_Normal:
                str = "Bullet_Spc_Normal";
                break;
            case Bullet.Spc_Pirate:
                str = "Bullet_Spc_Pirate";
                break;
            case Bullet.Spc_Little:
                str = "Bullet_Spc_Little";
                break;
            case Bullet.Spc_Zombie:
                str = "Bullet_Spc_Zombie";
                break;
            case Bullet.Spc_Ghost:
                str = "Bullet_Spc_Ghost";
                break;
            default:
                Debug.LogError("The bullet str from the bullet enum is not mapped");
                break;
        }

        return str;
    }

 
}
