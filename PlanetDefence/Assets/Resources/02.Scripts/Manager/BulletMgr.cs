using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMgr : MonoBehaviour
{
    private static BulletMgr m_inst = null;

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

    private int[] m_curBulletIndies = new int[20];
    private List<BulletCtrl>[] m_copyBullets = new List<BulletCtrl>[20]; // 포탑 지지대의 수가 20개이다.
    private Dictionary<string, GameObject> m_sourceBullets = new Dictionary<string, GameObject>();


    public void Instantiate()
    {
        if (m_inst == null)
        {
            GameObject container = new GameObject();
            container.name = "BulletMgr";
            m_inst = container.AddComponent<BulletMgr>() as BulletMgr;
            DontDestroyOnLoad(container);
        }
    }

    public void Awake()
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

    public bool FireBullet(int idx, Vector3 startPos, Vector3 startAngle, SpaceShipCtrl target)
    {
        if (target == null)
            return false;

        if (idx < 0 || idx >= m_copyBullets.Length)
        {
            Debug.Log("the bullet idx is out of the range");
            return false;
        }

        if(m_copyBullets[idx].Count == 0)
        {
            Debug.Log("the bullets is not allocated");
            return false;
        }

        m_copyBullets[idx][m_curBulletIndies[idx]].gameObject.SetActive(true);
        m_copyBullets[idx][m_curBulletIndies[idx]].Fire(startPos, startAngle, target);
        m_curBulletIndies[idx] += 1;

        if (m_curBulletIndies[idx] >= m_copyBullets[idx].Count)
            m_curBulletIndies[idx] = 0;

        return true;
    }

    public bool AllocateBullets(Bullet bullet, int idx, int bulletCnt, bool active= false)
    {
        if (idx < 0 || idx >= m_copyBullets.Length)
        {
            Debug.Log("the bullet idx is out of the range");
            return false;
        }

        if (m_copyBullets[idx].Count > 0)
        {
            Debug.Log("the bullets at the idx is created already");
            return false;
        }

        GameObject source = FindSourceBullet(bullet);

        if(source == null)
        {
            Debug.Log("Finding the source of bullet failed");
            return false;
        }

        m_copyBullets[idx].Capacity = bulletCnt;

        Transform parentTrsf = GameObject.FindGameObjectWithTag("BATTLESTATIC")?.GetComponent<Transform>();


        for (int i=0; i<bulletCnt; ++i)
        {
            GameObject obj = null;

            obj = Instantiate(source, parentTrsf);

            obj.SetActive(active);

            m_copyBullets[idx].Add(obj.GetComponent<BulletCtrl>());
        }

        return true;
    }

    public bool ClearBullets(int idx)
    {
        if (idx < 0 || idx >= m_copyBullets.Length)
        {
            Debug.Log("the bullet idx is out of the range");
            return false;
        }

        foreach (BulletCtrl ctrl in m_copyBullets[idx])
        {
            Destroy(ctrl.gameObject);
        }

        m_copyBullets[idx].Clear();
        m_copyBullets[idx].Capacity = 0;

        return true;
    }

    private GameObject FindSourceBullet(Bullet bullet)
    {
        string bulletStr = "";

        switch (bullet)
        {
            case Bullet.Lv1_Missile:
                bulletStr = "Bullet_Lv1_Missile";
                break;
            case Bullet.Lv1_Laser:
                bulletStr = "Bullet_Lv1_Laser";
                break;
        }

        GameObject source = null;

        m_sourceBullets.TryGetValue(bulletStr, out source);

        return source;
    }

    private void SetUpBullets()
    {
        AddBullet("Bullet_Lv1_Missile");
        AddBullet("Bullet_Lv1_Laser");

       for (int i=0; i< m_copyBullets.Length; ++i)
        {
            m_copyBullets[i] = new List<BulletCtrl>();
        }
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
}
