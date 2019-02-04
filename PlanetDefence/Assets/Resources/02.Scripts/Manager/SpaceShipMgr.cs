using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMgr : MonoBehaviour
{
    public struct WrappedWaveInfo
    {
        public  GameObject spaceShip;
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

    private Dictionary<string, GameObject> m_spaceShips = new Dictionary<string, GameObject>();

    public void Instantiate()
    {
        if (m_inst == null)
        {
            GameObject container = new GameObject();
            container.name = "BattleGameObjectMgr";
            m_inst = container.AddComponent<SpaceShipMgr>() as SpaceShipMgr;
            DontDestroyOnLoad(container);
        }
    }

    public void Awake()
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

    private void SetUpSpaceShips()
    {
        AddSpaceShip("SpaceShip_Red");
        AddSpaceShip("SpaceShip_Green");
        AddSpaceShip("SpaceShip_Blue");
    }

    public void StartCreatingWaves(WavesMob[] waveInfos)
    {
        foreach(WavesMob waveInfo in waveInfos)
        {
            GameObject spaceShip = FindSpaceShipByEnum(waveInfo.eMobType);

            if(spaceShip == null)
            {
                Debug.LogError("Instantiating SpaceShip by type failed");

                continue;
            }

            StartCoroutine("CreateWave", new WrappedWaveInfo(spaceShip, waveInfo.nMobNum, waveInfo.fDelayTime, waveInfo.fFirstDelayTime));
        }
    }

    public GameObject FindSpaceShipByEnum(MobType type )
    {
        string typeStr = "";

        switch (type)
        {
            case MobType.Normal:
                typeStr = "SpaceShip_Red";
                break;
            case MobType.Kamikaze:
                typeStr = "SpaceShip_Green";
                break;
            case MobType.Pirate:
                typeStr = "SpaceShip_Blue";
                break;
        }

        GameObject spaceShip = null;

        m_spaceShips.TryGetValue(typeStr, out spaceShip);
  
        return spaceShip;
    }

    public IEnumerator CreateWave(WrappedWaveInfo wrappedWaveInfo)
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

        Transform parentTrsf = GameObject.Find("BattleStatic")?.GetComponent<Transform>();

        if (parentTrsf != null)
        {
            while (curSapceShipNum < wrappedWaveInfo.spaceShipNum)
            {
                // TEMP : 위치 임시로 해놓음
                GameObject obj = null;

                float randY = Random.Range(1, 1.5f) * 600f;
                obj = Instantiate(wrappedWaveInfo.spaceShip, new Vector3(0, 1200f, 0), Quaternion.Euler(0,0,0), parentTrsf);
                obj.GetComponent<SpaceShipCtrl>().m_fallingDists[0] = randY;

                curSapceShipNum += 1;

                yield return new WaitForSeconds(wrappedWaveInfo.delay);
            }
        }  
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

        m_spaceShips.Add(name, spaceShip);

        return true;
    }
}
