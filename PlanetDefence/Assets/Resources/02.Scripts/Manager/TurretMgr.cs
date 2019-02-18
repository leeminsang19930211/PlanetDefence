using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMgr : MonoBehaviour
{
    private static TurretMgr m_inst = null;
    private bool m_init = false;
    private int m_focusedTurretSupportIdx = -1; // 클릭하여 현재 포커싱 된 터렛 지지대의 인덱스이다
    private List<TurretSupportCtrl> m_turretSupportCtrs = new List<TurretSupportCtrl>();
    private Dictionary<string, GameObject> m_sourceTurrets = new Dictionary<string, GameObject>();

    public static TurretMgr Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "TurretMgr";
                m_inst = container.AddComponent<TurretMgr>() as TurretMgr;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }

    public TurretCtrl FocusedTurret
    {
        get
        {
            if (m_focusedTurretSupportIdx < 0 || m_focusedTurretSupportIdx >= m_turretSupportCtrs.Count)
                return null;

            return m_turretSupportCtrs[m_focusedTurretSupportIdx].TurretCtrl;
        }
    }

    public void Init()
    {
        if (m_init)
            return;
        else
            m_init = true;

        GameObject turrets = GlobalGameObjectMgr.Inst.FindGameObject("Turrets");

        // 테스트 환경 용.씬에서 직접 프리팹을 추가해서 테스트 하는경우에는 GlobalGameObjectMgr.Inst 에 추가가 안되있다
        if (turrets == null)
        {
            turrets = GameObject.Find("Turrets");
        }

        if (turrets)
        {
            // 잠깐 TurretMgr에서 검색할수 있도록 켰다 끈다
            turrets.SetActive(true);
            SetUpTurrets();
            turrets.SetActive(false);
        }
    }

    public void Release_Clear()
    {
        m_focusedTurretSupportIdx = -1;  
    }

    public void Release_Fail()
    {
        m_focusedTurretSupportIdx = -1;

        foreach (TurretSupportCtrl turret in m_turretSupportCtrs)
        {
            if (turret.TurretCtrl)
            {
                turret.TurretCtrl.Die();
            }
        }
    }

    public TurretData[] GetSourceTurretDatas()
    {
        // TEMP
        //if(m_sourceTurrets.Count < (int)Turret.End)
        //{
        //    Debug.LogError("The sourc turrets is less than Turret.End");
        //    return null;
        //}

        TurretData[] datas = new TurretData[(int)Turret.End];

        for (int i=0; i<(int)Turret.End; ++i)
        {
            datas[i] = GetSourceTurretData((Turret)i);
        }

        return datas;
    }

    public TurretData GetSourceTurretData(Turret turret)
    {
        string key = EnumToStr(turret);

        GameObject source = null;

        if (false == m_sourceTurrets.TryGetValue(key, out source))
        {
            // TEMP
            //Debug.LogError("Finding source by " + key + " failed");
            return null;
        }

        TurretCtrl ctrl = source.GetComponent<TurretCtrl>();

        return ctrl.TurretData;
    }

    public void UpdateTurretDatas()
    {
        foreach(TurretSupportCtrl support in m_turretSupportCtrs)
        {
            if (support.TurretCtrl == null)
                continue;

            support.TurretCtrl.UpdateTurretData();
        }
    }

    public void CheckShieldToShow(int turretIdx, int exception = -1)
    {
        if (turretIdx < 0 || turretIdx >= m_turretSupportCtrs.Count)
        {
            Debug.Log("the focus idx is out of the range");
            return;
        }
       
        int startIdx = (turretIdx/5) * 5;
        PlanetArea area = IdxToArea(startIdx);

        BattleGameObjectMgr.Inst.HideShields(area);

        for (int i = startIdx; i < startIdx + 5; ++i)
        {
            if (m_turretSupportCtrs[i].TurretCtrl == null)
                continue;

            if (i == exception)
                continue;

            BattleGameObjectMgr.Inst.ShowShield(area, m_turretSupportCtrs[i].TurretCtrl.TurretType);
        }
    }

    public Gunner FindShieldTurret(int refIdx)
    {
        int startIdx = (refIdx / 5)* 5;

        for(int i =startIdx; i<startIdx +5; ++i)
        {
            if (m_turretSupportCtrs[i].TurretCtrl == null)
                continue;

            if(m_turretSupportCtrs[i].TurretCtrl.tag == "TURRET_SHIELD")
            {
                return m_turretSupportCtrs[i].TurretCtrl;
            }
        }

        return null;
    }

    public Gunner FindShieldTurret()
    {
        if (m_focusedTurretSupportIdx < 0 || m_focusedTurretSupportIdx>= m_turretSupportCtrs.Count)
            return null;

        int startIdx = (m_focusedTurretSupportIdx / 5) * 5;

        for (int i = startIdx; i < startIdx + 5; ++i)
        {
            if (m_turretSupportCtrs[i].TurretCtrl == null)
                continue;

            if (m_turretSupportCtrs[i].TurretCtrl.tag == "TURRET_SHIELD")
            {
                return m_turretSupportCtrs[i].TurretCtrl;
            }
        }

        return null;
    }


    public Gunner FindFirstTargetInFan(Vector3 from, float fanAngle)
    {     
        Vector3 toFrom = from - PlanetCtrl.Inst.Position; 

        float leftAngle = MyMath.LeftAngle360(PlanetCtrl.Inst.Up, toFrom);
        float halfAngle = fanAngle * 0.5f;

        int planetArea = -1;

        if( 0<= leftAngle && ( halfAngle > leftAngle  || 360f-halfAngle < leftAngle) )
        {
            planetArea = 0;
        }
        else if( 90f- halfAngle <= leftAngle && 90f +halfAngle > leftAngle )
        {
            planetArea = 1;
        }
        else if( 180f - halfAngle <= leftAngle && 180f +halfAngle > leftAngle)
        {
            planetArea = 2;
        }
        else if(270f - halfAngle <= leftAngle && 270f +halfAngle > leftAngle)
        {
            planetArea = 3;
        }

        if (planetArea == -1)
            return null;

        Gunner target = FindFirstTurret(planetArea);

        if (target == null) // 구역에는 있는데 터렛이 없을 경우
            return PlanetCtrl.Inst.GetPlanetHit(planetArea);

        return target;
    }

    public bool CheckTurretOnTurretSupport()
    {
        if (m_focusedTurretSupportIdx < 0 || m_focusedTurretSupportIdx >= m_turretSupportCtrs.Count)
        {
            Debug.Log("the focus idx is out of the range");
            return false;
        }

        if (m_turretSupportCtrs[m_focusedTurretSupportIdx].Focus == false)
        {
            Debug.Log("the turret support is not focused");
            return false;
        }

        return m_turretSupportCtrs[m_focusedTurretSupportIdx].TurretCtrl != null;
    }

    public bool CreateTurretOnTurretSupport(string turretName)
    {
        if(m_focusedTurretSupportIdx < 0 || m_focusedTurretSupportIdx >= m_turretSupportCtrs.Count)
        {
            Debug.Log("the focus idx is out of the range");
            return false;
        }

        if(CheckTurretOnTurretSupport() == true)
        {
            Debug.Log("the turret support has a turret");
            return false;
        }
        
        if(m_turretSupportCtrs[m_focusedTurretSupportIdx].Focus == false)
        {
            Debug.Log("the turret support is not focused");
            return false;
        }

        GameObject turret = null;
            
        if(false ==  m_sourceTurrets.TryGetValue(turretName, out turret))
        {
            Debug.Log("Finding turret in the Dictionary failed");
            return false;
        }

        Transform parentTrsf = GameObject.FindGameObjectWithTag("BATTLESTATIC")?.GetComponent<Transform>();

        if (parentTrsf == null)
        {
            Debug.Log("The BattleStatic inst is not found");
            return false;
        }

        CreateTurret(turret, parentTrsf);

        return true;
    }

    public bool CreateTurretOnTurretSupport(Turret turretName)
    {
        if (m_focusedTurretSupportIdx < 0 || m_focusedTurretSupportIdx >= m_turretSupportCtrs.Count)
        {
            Debug.Log("the focus idx is out of the range");
            return false;
        }

        if (CheckTurretOnTurretSupport() == true)
        {
            Debug.Log("the turret support has a turret");
            return false;
        }

        if (m_turretSupportCtrs[m_focusedTurretSupportIdx].Focus == false)
        {
            Debug.Log("the turret support is not focused");
            return false;
        }

        GameObject turret = null;
        string turretStr = EnumToStr(turretName);

        if (false == m_sourceTurrets.TryGetValue(turretStr, out turret))
        {
            Debug.Log("Finding turret in the Dictionary failed");
            return false;
        }

        Transform parentTrsf = GameObject.FindGameObjectWithTag("BATTLESTATIC")?.GetComponent<Transform>();

        if (parentTrsf == null)
        {
            Debug.Log("The BattleStatic inst is not found");
            return false;
        }

        CreateTurret(turret, parentTrsf);

        return true;
    }

    public bool RemoveTurretOnTurretSupport()
    {
        if (m_focusedTurretSupportIdx < 0 || m_focusedTurretSupportIdx >= m_turretSupportCtrs.Count)
        {
            Debug.Log("the focus idx is out of the range");
            return false;
        }

        if (CheckTurretOnTurretSupport() == false)
        {
            Debug.Log("the turret support has a no turret");
            return false;
        }

        if (m_turretSupportCtrs[m_focusedTurretSupportIdx].Focus == false)
        {
            Debug.Log("the turret support is not focused");
            return false;
        }

        RemoveTurret();

        return true;
    }

    public bool FocusTurretSupportByIdx(int idx)
    {
        if (idx < 0 || idx >= m_turretSupportCtrs.Count)
        {
            Debug.Log("the focus idx is out of the range");
            return false;
        }

        foreach(TurretSupportCtrl ctrl in m_turretSupportCtrs)
        {
            ctrl.Focus = false;
        }

        m_turretSupportCtrs[idx].Focus = true;

        m_focusedTurretSupportIdx = idx;

        return true;
    }

    private Gunner FindFirstTurret(int planetArea)
    {
        if (planetArea < 0 || planetArea >= 4)
            return null;

        int start = planetArea * 5;// 5 단위로 구역이 나뉨

        TurretCtrl turret = null;

        for (int i = start; i < start + 5; ++i)
        {
            if (m_turretSupportCtrs[i].TurretCtrl != null)
            {
                turret = m_turretSupportCtrs[i].TurretCtrl;
                break;
            }
        }

        return turret;
    }

    private void CreateTurret(GameObject source, Transform parentTrsf)
    {
      
        float angle = (m_focusedTurretSupportIdx / 5) * 90f; //5 단위로 위/왼쪽/아래/오른쪽으로 나뉨

        m_turretSupportCtrs[m_focusedTurretSupportIdx].TurretCtrl = Instantiate(source, m_turretSupportCtrs[m_focusedTurretSupportIdx].SetUpPos, Quaternion.Euler(0, 0, angle), parentTrsf)?.GetComponent<TurretCtrl>();
        m_turretSupportCtrs[m_focusedTurretSupportIdx].TurretCtrl.BulletPoolIdx = m_focusedTurretSupportIdx;
        m_turretSupportCtrs[m_focusedTurretSupportIdx].TurretCtrl.Clone = true;

      
    }

    private void RemoveTurret()
    {
        m_turretSupportCtrs[m_focusedTurretSupportIdx].TurretCtrl.Die();        
    }

    private void AddTurretSupports()
    {
        GameObject[] turretSupports = GameObject.FindGameObjectsWithTag("TURRET_SUPPORT");

        for (int i = 0; i < turretSupports.Length; ++i)
        {
            TurretSupportCtrl ctrl = turretSupports[i].GetComponent<TurretSupportCtrl>();

            if (ctrl == null)
            {
                Debug.LogError("Finding turretSupportsCtrl failed");
                continue;
            }

            ctrl.Idx = i;

            m_turretSupportCtrs.Add(ctrl);
        }
    }

    private bool AddTurret(string name)
    {
        GameObject turret = null;

        turret = GameObject.Find(name);
        if (turret == null)
        {
            Debug.LogError("Finding " + name + " failed");
            return false;
        }

        m_sourceTurrets.Add(name, turret);
        return true;
    }

    private void SetUpTurrets()
    {
        AddTurret("Turret_Lv1_Missile");
        AddTurret("Turret_Lv1_Laser");
        AddTurret("Turret_Lv1_Gatling");
        AddTurret("Turret_Lv2_Missile");
        AddTurret("Turret_Lv2_Laser");
        AddTurret("Turret_Lv2_Poison");
        AddTurret("Turret_Lv2_Shield");
        AddTurret("Turret_Lv2_Slow");
        AddTurret("Turret_Lv2_Pause");
        AddTurret("Turret_Lv3_Sniper");
        AddTurret("Turret_Lv3_Missile");
        AddTurret("Turret_Lv3_Laser");
        AddTurret("Turret_Lv3_Heal");
        AddTurret("Turret_Lv3_Berserker");
        AddTurret("Turret_Lv3_Poison");
        AddTurret("Turret_Lv3_Shield");
        AddTurret("Turret_Lv3_Slow");
        AddTurret("Turret_Lv3_Pause");
        AddTurret("Turret_Lv1_Fast");
        AddTurret("Turret_Lv2_Fast");
        AddTurret("Turret_Lv3_Fast");
        AddTurret("Turret_Lv2_Gatling");
        AddTurret("Turret_Lv3_Gatling");

        AddTurretSupports();

        BattleGameObjectMgr.Inst.OffTurretSupports(2);
    }

    private PlanetArea IdxToArea(int num)
    {
        int areaNum = num / 5;

        switch (areaNum)
        {
            case 0:
                return PlanetArea.Up;
            case 1:
                return PlanetArea.Left;
            case 2:
                return PlanetArea.Down;
            case 3:
                return PlanetArea.Right;
            default:
                Debug.LogError("The num to change to PlanetArea is invalid");
                break;

        }

        return PlanetArea.End;
    }

    private string EnumToStr(Turret turret)
    {
        string str = "";

        switch (turret)
        {
            case Turret.Lv1_Missile:
                str = "Turret_Lv1_Missile";
                break;
            case Turret.Lv1_Laser:
                str = "Turret_Lv1_Laser";
                break;
            case Turret.Lv1_Gatling:
                str = "Turret_Lv1_Gatling";
                break;
            case Turret.Lv1_Fast:
                str = "Turret_Lv1_Fast";
                break;
            case Turret.Lv2_Missile:
                str = "Turret_Lv2_Missile";
                break;
            case Turret.Lv2_Laser:
                str = "Turret_Lv2_Laser";
                break;
            case Turret.Lv2_Shield:
                str = "Turret_Lv2_Shield";
                break;
            case Turret.Lv2_Poison:
                str = "Turret_Lv2_Poison";
                break;
            case Turret.Lv2_Slow:
                str = "Turret_Lv2_Slow";
                break;
            case Turret.Lv2_Pause:
                str = "Turret_Lv2_Pause";
                break;
            case Turret.Lv2_Gatling:
                str = "Turret_Lv2_Gatling";
                break;
            case Turret.Lv2_Fast:
                str = "Turret_Lv2_Fast";
                break;
            case Turret.Lv3_Missile:
                str = "Turret_Lv3_Missile";
                break;
            case Turret.Lv3_Laser:
                str = "Turret_Lv3_Laser";
                break;
            case Turret.Lv3_Sniper:
                str = "Turret_Lv3_Sniper";
                break;
            case Turret.Lv3_Heal:
                str = "Turret_Lv3_Heal";
                break;
            case Turret.Lv3_Berserker:
                str = "Turret_Lv3_Berserker";
                break;
            case Turret.Lv3_Poison:
                str = "Turret_Lv3_Poison";
                break;
            case Turret.Lv3_Shield:
                str = "Turret_Lv3_Shield";
                break;
            case Turret.Lv3_Slow:
                str = "Turret_Lv3_Slow";
                break;
            case Turret.Lv3_Pause:
                str = "Turret_Lv3_Pause";
                break;
            case Turret.Lv3_Gatling:
                str = "Turret_Lv3_Gatling";
                break;
            case Turret.Lv3_Fast:
                str = "Turret_Lv3_Fast";
                break;
            default:
                Debug.LogError("The turret str from the turret enum is not mapped");
                break;
        }

        return str;
    }
}
