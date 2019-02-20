using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GlobalGameObjectMgr : MonoBehaviour
{
    //public CardInfo SelectedCard = null;
    public WavesMob[] waveInfos = null;
    public bool bSelected = false;

    private static GlobalGameObjectMgr m_inst = null;

    private Dictionary<string, GameObject> m_gameObjects = new Dictionary<string, GameObject>();

    public int MaxDay { get; set; } = 3; // Test용
    public int CurDay { get; set; } = 1;
    public bool Battle { get; set; } = false; // Battle 씬에서 전투가 끝났는지 아닌지 여부를 판단하기 위한 값

    public static GlobalGameObjectMgr Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "GlobalGameObjectMgr";
                m_inst = container.AddComponent<GlobalGameObjectMgr>() as GlobalGameObjectMgr;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }

    public int LeftDays
    {
        get
        {
            int leftDays = MaxDay - CurDay;

            if (leftDays < 0)
                return 0;

            return leftDays;
        }

    }


    public void IncreaseDay()
    {
        CurDay += 1;

        if (CurDay > MaxDay)
            CurDay = MaxDay;

      
    }

    public bool SaveData(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        if (fileStream == null)
        {
            Debug.LogError("File open for saving global data failed");
            return false;
        }

        BinaryFormatter binFormatter = new BinaryFormatter();

        binFormatter.Serialize(fileStream, CurDay);
 
        fileStream.Close();

        return true;
    }

    public bool LoadData(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open);

        if (fileStream == null)
        {
            Debug.LogError("File open for loading player data failed");
            return false;
        }

        BinaryFormatter binFormatter = new BinaryFormatter();

        CurDay = (int)binFormatter.Deserialize(fileStream);
        
        fileStream.Close();

        return true;
    }

    // GameObjectMgr에 등록된 모든 오브젝트는 씬이 변해도 파괴되지 않는다.
    public bool RegisterGameObject(string key, GameObject gameobj, bool active)
    {
        if (m_gameObjects.ContainsKey(key))
            return false;

        gameobj.SetActive(active);

        DontDestroyOnLoad(gameobj);

        m_gameObjects.Add(key, gameobj);

        return true;
    }

    public GameObject FindGameObject(string key)
    {
        GameObject obj = null;

        m_gameObjects.TryGetValue(key,out obj);

        return obj;
    }

    public bool SetGameObectActive(string key, bool active)
    {
        GameObject obj = null;

        m_gameObjects.TryGetValue(key, out obj);

        if (obj == null)
            return false;

        obj.SetActive(active);

        return true;
    }

    public bool DontDestroyOnLoad(string key)
    {
        GameObject obj = null;

        m_gameObjects.TryGetValue(key, out obj);

        if (obj == null)
            return false;

        DontDestroyOnLoad(obj);

        return true;
    }

    public bool MoveGameObjectToScene(string key, string sceneName)
    {
        GameObject obj = null;

        m_gameObjects.TryGetValue(key, out obj);

        if (obj == null)
            return false;

        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (scene == null)
            return false;

        SceneManager.MoveGameObjectToScene(obj, scene);

        return true;
    }
}
