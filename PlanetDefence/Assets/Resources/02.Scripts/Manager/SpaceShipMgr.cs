using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMgr : MonoBehaviour
{
    public struct WrappedWaveInfo
    {
        public GameObject spaceShip;
        public int spaceShipNum;
        public float delay;
        public float firstDelay;

        public WrappedWaveInfo(GameObject _spaceShip, int _spaceShipNum, float _delay, float _firstDelay)
        {
            spaceShip = _spaceShip;
            spaceShipNum = _spaceShipNum;
            delay = _delay;
            firstDelay = _firstDelay;
        }
    }

    private static SpaceShipMgr m_inst = null;
    private int m_maxSpaceShipCnt = 0;
    private int m_createdSpaceShipCnt = 0;
    private Dictionary<string, GameObject> m_sourceSpaceShips = new Dictionary<string, GameObject>();

    public int MaxSpaceShipCnt
    {
        get
        {
            return m_maxSpaceShipCnt;
        }
    }

    public static SpaceShipMgr Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "SpaceShipMgr";
                m_inst = container.AddComponent<SpaceShipMgr>() as SpaceShipMgr;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }

    public void Init()
    {
        GameObject spaceShips = GlobalGameObjectMgr.Inst.FindGameObject("SpaceShips");

        // 테스트 환경 용.씬에서 직접 프리팹을 추가해서 테스트 하는경우에는 GlobalGameObjectMgr.Inst 에 추가가 안되있다
        if (spaceShips == null)
        {
            spaceShips = GameObject.Find("SpaceShips");
        }

        if (spaceShips)
        {
            // 잠깐 SpaceShipMgr에서 검색할수 있도록 켰다 끈다
            spaceShips.SetActive(true);
            SetUpSpaceShips();
            spaceShips.SetActive(false);
        }
    }

    public void Release_Clear()
    {
        m_maxSpaceShipCnt = 0;
        m_createdSpaceShipCnt = 0;

        GameObject[] ships = GameObject.FindGameObjectsWithTag("SPACESHIP_DUMMY");

        foreach(GameObject obj in ships)
        {
            obj?.GetComponent<SpaceShipCtrl>()?.Die();
        }

        ships = GameObject.FindGameObjectsWithTag("SPACESHIP_NORMAL");

        foreach (GameObject obj in ships)
        {
            obj?.GetComponent<SpaceShipCtrl>()?.Die();
        }

        StopCoroutine("CreateWave");
    }

    public void Release_Fail()
    {
        m_maxSpaceShipCnt = 0;
        m_createdSpaceShipCnt = 0;

        GameObject[] ships = GameObject.FindGameObjectsWithTag("SPACESHIP_DUMMY");

        foreach (GameObject obj in ships)
        {
            obj?.GetComponent<SpaceShipCtrl>()?.Die();
        }

        ships = GameObject.FindGameObjectsWithTag("SPACESHIP_NORMAL");

        foreach (GameObject obj in ships)
        {
            obj?.GetComponent<SpaceShipCtrl>()?.Die();
        }

        StopCoroutine("CreateWave");
    }

    public void StartCreatingWaves(WavesMob[] waveInfos)
    {
        m_maxSpaceShipCnt = 0;
        m_createdSpaceShipCnt = 0;

        foreach (WavesMob waveInfo in waveInfos)
        {
            m_maxSpaceShipCnt += waveInfo.nMobNum;

            GameObject spaceShip = FindSpaceShipByEnum(waveInfo.eMobType);

            if(spaceShip == null)
            {
                Debug.LogError("Instantiating SpaceShip by type failed");

                continue;
            }

            StartCoroutine("CreateWave", new WrappedWaveInfo(spaceShip, waveInfo.nMobNum, waveInfo.fDelayTime, waveInfo.fFirstDelayTime));
        }

        BattleGameObjectMgr.Inst.UpdateEnemyCnt(m_maxSpaceShipCnt);
    }

    public Gunner FindFirstTargetInFan(float refAngle, float fanAngle, Vector3 from, float minDist)
    {
        Gunner target = null;

        GameObject[] dummyList = GameObject.FindGameObjectsWithTag("SPACESHIP_DUMMY");

        target = FindFirstTargetInList(dummyList, refAngle, fanAngle, from,  minDist);

        if (target != null)
            return target; // 더미가 있으면 무조건 더미먼저

        GameObject[] normalList = GameObject.FindGameObjectsWithTag("SPACESHIP_NORMAL");

        target = FindFirstTargetInList(normalList, refAngle, fanAngle, from, minDist);

        return target;
    }

    public Gunner FindMinHPTargetInFan(float refAngle, float fanAngle, Vector3 from, float minDist)
    { 
        GameObject[] dummyList = GameObject.FindGameObjectsWithTag("SPACESHIP_DUMMY");

        Gunner dummyTarget = FindMinHPTargetInList(dummyList, refAngle, fanAngle, from, minDist);

        GameObject[] normalList = GameObject.FindGameObjectsWithTag("SPACESHIP_NORMAL");

        Gunner noramlTarget = FindMinHPTargetInList(normalList, refAngle, fanAngle, from, minDist);

        if (dummyTarget == null)
            return noramlTarget;
        else if (noramlTarget == null)
            return dummyTarget;

        return noramlTarget.CurHP <= dummyTarget.CurHP ? noramlTarget : dummyTarget;
    }

    private Gunner FindFirstTargetInList(GameObject[] spaceShipList, float refAngle, float fanAngle, Vector3 from, float minDist)
    {
        if (spaceShipList == null)
            return null;

        float halfAngle = fanAngle * 0.5f;

        foreach (GameObject spcShip in spaceShipList)
        {
            SpaceShipCtrl ctrl = spcShip.GetComponent<SpaceShipCtrl>();

            if ((ctrl.Position - from).magnitude < minDist)
            {
                continue;
            }

            SpaceShip_GhostCtrl ghost = ctrl as SpaceShip_GhostCtrl;

            if (ghost != null)
            {
                if (ghost.Visible == false)
                    continue;
            }

            if(refAngle == 0)
            {
                if (0 <= ctrl.AngleFromPlanetUp && (halfAngle > ctrl.AngleFromPlanetUp || 360f- halfAngle  < ctrl.AngleFromPlanetUp))
                    return ctrl;      
            }
            else
            {
                if (refAngle - halfAngle <= ctrl.AngleFromPlanetUp && refAngle + halfAngle > ctrl.AngleFromPlanetUp)
                    return ctrl;
            }
        }

        return null;
    }

    private Gunner FindMinHPTargetInList(GameObject[] spaceShipList, float refAngle, float fanAngle, Vector3 from, float minDist)
    {
        if (spaceShipList == null)
            return null;

        float halfAngle = fanAngle * 0.5f;

        float minHP = float.MaxValue;
        Gunner targetCtrl = null;


        foreach (GameObject spcShip in spaceShipList)
        {
            SpaceShipCtrl ctrl = spcShip.GetComponent<SpaceShipCtrl>();

            if ((ctrl.Position - from).magnitude < minDist)
            {
                continue;
            }

            SpaceShip_GhostCtrl ghost = ctrl as SpaceShip_GhostCtrl;

            if (ghost != null)
            {
                if (ghost.Visible == false)
                    continue;
            }

            if (refAngle == 0)
            {
                if (0 <= ctrl.AngleFromPlanetUp && (halfAngle > ctrl.AngleFromPlanetUp || 360f - halfAngle < ctrl.AngleFromPlanetUp))
                {
                    if(ctrl.CurHP < minHP)
                    {
                        targetCtrl = ctrl;
                        minHP = ctrl.CurHP;
                    }
                }
                   
            }
            else
            {
                if (refAngle - halfAngle <= ctrl.AngleFromPlanetUp && refAngle + halfAngle > ctrl.AngleFromPlanetUp)
                {
                    if (ctrl.CurHP < minHP)
                    {
                        targetCtrl = ctrl;
                        minHP = ctrl.CurHP;
                    }
                }
                    
            }
        }

        return targetCtrl;
    }

    private GameObject FindSpaceShipByEnum(MobType type )
    {
        GameObject spaceShip = null;

        m_sourceSpaceShips.TryGetValue(EnumToStr(type), out spaceShip);
  
        return spaceShip;
    }

    private IEnumerator CreateWave(WrappedWaveInfo wrappedWaveInfo)
    {
        if (wrappedWaveInfo.spaceShip == null)
            yield return null;

        if(wrappedWaveInfo.firstDelay < 0 || wrappedWaveInfo.delay< 0 || wrappedWaveInfo.spaceShipNum < 0)
        {
            Debug.LogError("The values for creating waves is invaild");
            yield return null;
        }

        yield return new WaitForSeconds(wrappedWaveInfo.firstDelay);

        int curSapceShipNum = 0;

        Transform parentTrsf = GameObject.FindGameObjectWithTag("BATTLESTATIC")?.GetComponent<Transform>();

        if (parentTrsf != null)
        {
            while (curSapceShipNum < wrappedWaveInfo.spaceShipNum)
            {
                CreateSpaceShip(wrappedWaveInfo.spaceShip, parentTrsf);

                curSapceShipNum += 1;
                
                yield return new WaitForSeconds(wrappedWaveInfo.delay);
            }
        }  
    }

    private void CreateSpaceShip(GameObject source, Transform parentTrsf)
    {
        SpaceShipCtrl ctrl = null;
        ctrl = Instantiate(source, new Vector3(0, 1000f, 0), Quaternion.Euler(0, 0, 0), parentTrsf)?.GetComponent<SpaceShipCtrl>();
        ctrl.BulletPoolIdx = m_createdSpaceShipCnt;
        ctrl.Clone = true;

        m_createdSpaceShipCnt += 1;
    }

    private bool AddSpaceShip(string name)
    {
        GameObject spaceShip = null;

        spaceShip = GameObject.Find(name);
        if(spaceShip == null)
        {
            Debug.LogError("Finding " + name + " failed");
            return false;
        }

        m_sourceSpaceShips.Add(name, spaceShip);

        return true;
    }

    private void SetUpSpaceShips()
    {
        AddSpaceShip("SpaceShip_Normal");
        AddSpaceShip("SpaceShip_Dummy");
        AddSpaceShip("SpaceShip_Kamikaze");
        AddSpaceShip("SpaceShip_Pirate");
        AddSpaceShip("SpaceShip_Little");
        AddSpaceShip("SpaceShip_Zombie");
        AddSpaceShip("SpaceShip_Ghost");
    }

    private string EnumToStr(MobType spaceShip)
    {
        string str = "";

        switch (spaceShip)
        {
            case MobType.Normal:
                str = "SpaceShip_Normal";
                break;
            case MobType.DummyShip:
                str = "SpaceShip_Dummy";
                break;
            case MobType.Kamikaze:
                str = "SpaceShip_Kamikaze";
                break;
            case MobType.Pirate:
                str = "SpaceShip_Pirate";
                break;
            case MobType.Little:
                str = "SpaceShip_Little";
                break;
            case MobType.ZombieShip:
                str = "SpaceShip_Zombie";
                break;
            case MobType.GhostShip:
                str = "SpaceShip_Ghost";
                break;

            default:
                Debug.LogError("The space ship str from the space ship enum is not mapped");
                break;
        }

        return str;
    }


}
