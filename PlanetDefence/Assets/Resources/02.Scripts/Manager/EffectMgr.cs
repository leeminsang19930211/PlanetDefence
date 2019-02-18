using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMgr : MonoBehaviour
{
    private static EffectMgr m_inst = null;
    private bool m_init = false;
    private List<int>[] m_effectPoolIndex = new List<int>[(int)EffectPool.End];
    private List<List<EffectCtrl>>[] m_effectPool = new List<List<EffectCtrl>>[(int)EffectPool.End];
    private Dictionary<string, GameObject> m_sourceEffects = new Dictionary<string, GameObject>();

    public static EffectMgr Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "EffectMgr";
                m_inst = container.AddComponent<EffectMgr>() as EffectMgr;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }
    
    public void Init()
    {
        if (m_init)
            return;
        else
            m_init = true;

        GameObject bullets = GlobalGameObjectMgr.Inst.FindGameObject("Effects");

        // 테스트 환경 용.씬에서 직접 프리팹을 추가해서 테스트 하는경우에는 GlobalGameObjectMgr.Inst 에 추가가 안되있다
        if (bullets == null)
        {
            bullets = GameObject.Find("Effects");
        }

        if (bullets)
        {
            // 잠깐 SpaceShipMgr에서 검색할수 있도록 켰다 끈다
            bullets.SetActive(true);
            SetUpEffects();
            bullets.SetActive(false);
        }
    }

    public void Release_Clear()
    {
        //for (int i = 0; i < (int)EffectPool.End; ++i)
        //{
        //    ClearEffectPool((EffectPool)i);
        //}
    }

    public void Release_Fail()
    {
        //for (int i = 0; i < (int)EffectPool.End; ++i)
        //{
        //    ClearEffectPool((EffectPool)i);
        //}
    }

    public bool AllocateEffectPool(EffectPool pool, int cnt)
    {
        if (m_effectPool[(int)pool].Count > 0 || m_effectPool[(int)pool].Count > 0)
        {
            //Debug.Log("The effectPool is alloceted already");
            return false;
        }

        for (int i = 0; i < cnt; ++i)
        {
            m_effectPool[(int)pool].Add(new List<EffectCtrl>());
            m_effectPoolIndex[(int)pool].Add(0);
        }
        return true;
    }

    public bool ClearEffectPool(EffectPool pool)
    {
        if (m_effectPool[(int)pool] == null)
            return false;

        for (int i = 0; i < m_effectPool[(int)pool].Count; ++i)
        {
            ClearEffects(pool, i);
        }

        m_effectPoolIndex[(int)pool].Clear();
        m_effectPool[(int)pool].Clear();
        m_effectPool[(int)pool].Capacity = 0;

        return true;
    }

    public bool AllocateEffects(Effect effect, EffectPool pool, int idx, int bulletCnt, bool active = false)
    {
        if (m_effectPool[(int)pool].Count == 0)
        {
            Debug.Log("The effectPool is not allocted");
            return false;
        }

        if (idx < 0 || idx >= m_effectPool[(int)pool].Count)
        {
            Debug.Log("the effect idx is out of the range");
            return false;
        }

        if (m_effectPool[(int)pool][idx].Count > 0)
        {
            Debug.Log("the effects at the idx is created already");
            return false;
        }

        GameObject source = FindSourceEffect(effect);

        if (source == null)
        {
            Debug.Log("Finding the source of effect failed");
            return false;
        }

        m_effectPool[(int)pool][idx].Capacity = bulletCnt;

        Transform parentTrsf = GameObject.FindGameObjectWithTag("BATTLESTATIC")?.GetComponent<Transform>();

        for (int i = 0; i < bulletCnt; ++i)
        {
            GameObject obj = null;

            obj = Instantiate(source, parentTrsf);

            obj.SetActive(active);

            m_effectPool[(int)pool][idx].Add(obj.GetComponent<EffectCtrl>());
        }

        m_effectPoolIndex[(int)pool][idx] = 0;

        return true;
    }

    public bool ClearEffects(EffectPool pool, int idx)
    {
        if (idx < 0 || idx >= m_effectPool[(int)pool].Count)
        {
            Debug.Log("the bullet idx is out of the range");
            return false;
        }

        foreach (EffectCtrl ctrl in m_effectPool[(int)pool][idx])
        {
            Destroy(ctrl.gameObject);
        }

        m_effectPool[(int)pool][idx].Clear();
        m_effectPool[(int)pool][idx].Capacity = 0;
        m_effectPoolIndex[(int)pool][idx] = 0;

        return true;
    }

    public bool PlayEffect(EffectPool pool, int idx, Vector3 startPos)
    {
        if (idx < 0 || idx >= m_effectPool[(int)pool].Count)
        {
            Debug.Log("the effect idx is out of the range");
            return false;
        }

        if (m_effectPool[(int)pool][idx].Count == 0)
        {
            Debug.Log("the effect is not allocated");
            return false;
        }

        m_effectPool[(int)pool][idx][m_effectPoolIndex[(int)pool][idx]].gameObject.SetActive(true);
        m_effectPool[(int)pool][idx][m_effectPoolIndex[(int)pool][idx]].Play(startPos);
        m_effectPoolIndex[(int)pool][idx] += 1;

        if (m_effectPoolIndex[(int)pool][idx] >= m_effectPool[(int)pool][idx].Count)
            m_effectPoolIndex[(int)pool][idx] = 0;

        return true;
    }

    private GameObject FindSourceEffect(Effect effect)
    {
        GameObject source = null;

        m_sourceEffects.TryGetValue(EnumToStr(effect), out source);

        return source;
    }


    private bool AddEffect(string name)
    {
        GameObject effect = null;

        effect = GameObject.Find(name);
        if (effect == null)
        {
            Debug.LogError("Finding " + name + " failed");
            return false;
        }

        m_sourceEffects.Add(name, effect);

        return true;
    }

    private void SetUpEffects()
    {
        AddEffect("Explosion_Bullet0");

        for (int i = 0; i < (int)EffectPool.End; ++i)
        {
            m_effectPool[i] = new List<List<EffectCtrl>>();
            m_effectPoolIndex[i] = new List<int>();
        }

    }
    private string EnumToStr(Effect effect)
    {
        string str = "";

        switch (effect)
        {
            case Effect.Explosion_Bullet0:
                str = "Explosion_Bullet0";
                break;
            default:
                Debug.LogError("The effect str from the effect enum is not mapped");
                break;
        }

        return str;
    }

}
