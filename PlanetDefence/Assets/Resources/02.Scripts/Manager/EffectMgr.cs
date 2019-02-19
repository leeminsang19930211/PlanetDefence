using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EffectMgr : MonoBehaviour
{
    private static EffectMgr m_inst = null;
    private int[] m_effectPoolIdx = new int[(int)Effect.End];
    private List<EffectCtrl>[] m_effectPool = new List<EffectCtrl>[(int)Effect.End];
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

    public void _OnStart()
    {
        GameObject effects = GlobalGameObjectMgr.Inst.FindGameObject("Effects");

        // 테스트 환경 용.씬에서 직접 프리팹을 추가해서 테스트 하는경우에는 GlobalGameObjectMgr.Inst 에 추가가 안되있다
        if (effects == null)
        {
            effects = GameObject.Find("Effects");
        }

        if (effects)
        {
            // 잠깐 SpaceShipMgr에서 검색할수 있도록 켰다 끈다
            effects.SetActive(true);
            SetUpEffects();
            effects.SetActive(false);
        }
    }


    public void Release_Clear()
    {
        for (int i = 0; i < (int)Effect.End; ++i)
        {
            ClearEffects((Effect)i);
        }
    }

    public void Release_Fail()
    {
        for (int i = 0; i < (int)Effect.End; ++i)
        {
            ClearEffects((Effect)i);
        }
    }

    public bool PlayEffect(Effect effect, Vector3 startPos)
    {
        if (m_effectPool[(int)effect].Count == 0)
        {
            Debug.Log("the effect pool is not allocated");
            return false;
        }

        m_effectPool[(int)effect][m_effectPoolIdx[(int)effect]].gameObject.SetActive(true);
        m_effectPool[(int)effect][m_effectPoolIdx[(int)effect]].Play(startPos);

        m_effectPoolIdx[(int)effect] += 1;

        if (m_effectPoolIdx[(int)effect] >= m_effectPool[(int)effect].Count)
        {
            m_effectPoolIdx[(int)effect] = 0;
        }

        return true;
    }

    public bool AllocateEffects(Effect effect, int count)
    {
        if(effect == Effect.End)
        {
            Debug.LogError("the effect to allocate pool is Effect.End");
            return false;
        }

        if(m_effectPool[(int)effect].Count > 0)
        {
            Debug.LogError("The effect pool is allocated already");
            return false;
        }

        GameObject source = FindSourceEffect(effect);

        if (source == null)
        {
            Debug.Log("Finding the source of effect failed");
            return false;
        }

        Transform parent = GameObject.FindGameObjectWithTag("BATTLESTATIC")?.GetComponent<Transform>();

        for (int i=0; i<count; ++i)
        {
            m_effectPool[(int)effect].Add(CreateEffect(source, parent));
        }

        m_effectPoolIdx[(int)effect] = 0;

        return true;
    }

    public bool ClearEffects(Effect effect)
    {
        if (effect == Effect.End)
        {
            Debug.LogError("the effect to allocate pool is Effect.End");
            return false;
        }

        foreach (EffectCtrl ctrl in m_effectPool[(int)effect])
        {
            RemoveEffect(ctrl.gameObject);
        }

        m_effectPool[(int)effect].Clear();
        m_effectPool[(int)effect].Capacity = 0;
        m_effectPoolIdx[(int)effect] = 0;

        return true;
    }
    
   
    private EffectCtrl CreateEffect(GameObject source, Transform parent, bool active = false)
    {
        GameObject obj = Instantiate(source, parent);

        obj.SetActive(active);

        return obj.GetComponent<EffectCtrl>();
    }

    private void RemoveEffect(GameObject effect)
    {
        Destroy(effect);
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
        AddEffect("Effect_Explosion0");
        AddEffect("Effect_Explosion1");
        AddEffect("Effect_Explosion2");
        AddEffect("Effect_ShieldHit0");
        AddEffect("Effect_Poison0");

        for (int i = 0; i < (int)Effect.End; ++i)
        {
            m_effectPool[i] = new List<EffectCtrl>();
            m_effectPoolIdx[i] = 0;
        }

    }
    private string EnumToStr(Effect effect)
    {
        string str = "";

        switch (effect)
        {
            case Effect.Explosion0:
                str = "Effect_Explosion0";
                break;
            case Effect.Explosion1:
                str = "Effect_Explosion1";
                break;
            case Effect.Explosion2:
                str = "Effect_Explosion2";
                break;
            case Effect.ShieldHit0:
                str = "Effect_ShieldHit0";
                break;
            case Effect.Poison0:
                str = "Effect_Poison0";
                break;
            default:
                Debug.LogError("The effect str from the effect enum is not mapped");
                break;
        }

        return str;
    }
}
